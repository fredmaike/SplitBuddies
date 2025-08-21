using System;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Controllers;
using SplitBuddies.Models;
using SplitBuddies.Data;
using System.Collections.Generic;
using System.IO;

namespace SplitBuddies.Views
{
    public partial class ExpenseForm : Form
    {
        private readonly ExpenseController expenseController = new ExpenseController();
        private readonly GroupController groupController;
        private readonly User currentUser;

        public ExpenseForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            InitializeComponent();

            // Controlador de grupos trabaja sobre la lista viva del DataManager
            var dm = DataManager.Instance;
            dm.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            dm.LoadUsers();
            dm.LoadGroups();
            dm.LoadExpenses();
            try { dm.LoadInvitations(); } catch { /* opcional */ }

            groupController = new GroupController(dm.Groups);

            // Eventos
            cmbGroups.SelectedIndexChanged += CmbGroups_SelectedIndexChanged;
            btnAddExpense.Click += btnAddExpense_Click;

            // Cargar combos iniciales
            LoadGroups();
        }

        // -------- utilidades internas --------

        private sealed class UserItem
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public override string ToString() => string.IsNullOrWhiteSpace(Name) ? Email : $"{Name} ({Email})";
        }

        private void LoadGroups()
        {
            // Solo grupos a los que pertenece el usuario actual
            var groups = groupController.GetGroupsForUser(currentUser.Email) ?? new List<Group>();

            cmbGroups.DataSource = null;
            cmbGroups.DisplayMember = nameof(Group.GroupName);
            cmbGroups.ValueMember   = nameof(Group.GroupId);
            cmbGroups.DataSource    = groups;

            if (groups.Count > 0)
                cmbGroups.SelectedIndex = 0; // dispara CmbGroups_SelectedIndexChanged
        }

        private void CmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedItem is not Group selectedGroup)
            {
                cmbPaidBy.DataSource = null;
                clbIncludedMembers.Items.Clear();
                return;
            }

            // Mapear miembros del grupo (emails) a usuarios (para mostrar nombre)
            var dm = DataManager.Instance;
            var memberEmails = (selectedGroup.Members ?? new List<string>()).Where(m => !string.IsNullOrWhiteSpace(m)).Distinct(StringComparer.OrdinalIgnoreCase).ToList();

            // Construir lista de UserItem solo con miembros del grupo
            var usersInGroup = memberEmails
                .Select(e => {
                    var u = dm.Users.FirstOrDefault(x => x != null && x.Email != null && x.Email.Equals(e, StringComparison.OrdinalIgnoreCase));
                    return new UserItem { Email = e, Name = u?.Name ?? e };
                })
                .ToList();

            // Pagador = solo miembros del grupo
            cmbPaidBy.DataSource = null;
            cmbPaidBy.DisplayMember = nameof(UserItem.Name);
            cmbPaidBy.ValueMember   = nameof(UserItem.Email);
            cmbPaidBy.DataSource    = usersInGroup;

            // Intentar seleccionar por defecto al usuario actual si es miembro
            var idx = usersInGroup.FindIndex(u => u.Email.Equals(currentUser.Email, StringComparison.OrdinalIgnoreCase));
            if (idx >= 0) cmbPaidBy.SelectedIndex = idx;

            // Participantes = checklist con todos los miembros del grupo, marcados por defecto
            clbIncludedMembers.Items.Clear();
            foreach (var u in usersInGroup)
                clbIncludedMembers.Items.Add(u, true);
        }

        // -------- alta de gasto --------
        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedItem is not Group selectedGroup)
            {
                MessageBox.Show("Seleccione un grupo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPaidBy.SelectedItem is not UserItem payerItem)
            {
                MessageBox.Show("Seleccione quién pagó.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string expenseName = (txtExpenseName?.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(expenseName))
            {
                MessageBox.Show("Ingrese un nombre para el gasto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse((txtAmount?.Text ?? "").Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Ingrese un monto válido (> 0).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Participantes seleccionados
            var involvedEmails = clbIncludedMembers.CheckedItems
                .Cast<UserItem>()
                .Select(u => u.Email)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (involvedEmails.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un participante.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar que pagador y participantes pertenezcan al grupo
            var groupMembers = (selectedGroup.Members ?? new List<string>()).Distinct(StringComparer.OrdinalIgnoreCase).ToHashSet(StringComparer.OrdinalIgnoreCase);

            if (!groupMembers.Contains(payerItem.Email))
            {
                MessageBox.Show("El pagador seleccionado no pertenece al grupo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (involvedEmails.Any(e => !groupMembers.Contains(e)))
            {
                MessageBox.Show("Existen participantes que no pertenecen al grupo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Fecha: usa dtpDate si existe; si no, ahora
            DateTime when = DateTime.Now;
            var dtp = this.Controls.Find("dtpDate", true).FirstOrDefault() as DateTimePicker;
            if (dtp != null) when = dtp.Value;

            try
            {
                // Crear gasto vía controlador
                var expense = expenseController.AddExpense(
                    name: expenseName,
                    description: (txtDescription?.Text ?? "").Trim(),
                    payerEmail: payerItem.Email,
                    involvedEmails: involvedEmails,
                    amount: amount,
                    date: when,
                    groupId: selectedGroup.GroupId
                );

                // Persistir: aseguramos que esté en la lista global y actualizamos el grupo
                var dm = DataManager.Instance;

                // Si el controlador ya lo añadió a dm.Expenses, esto no duplica por Id; si no, lo añadimos.
                if (!dm.Expenses.Any(e => e.Id == expense.Id))
                    dm.Expenses.Add(expense);

                // Asegurar que el grupo tenga la lista de IDs de gastos y agregar si no está
                selectedGroup.Expenses ??= new List<int>();
                if (!selectedGroup.Expenses.Contains(expense.Id))
                    selectedGroup.Expenses.Add(expense.Id);

                dm.SaveExpenses();
                dm.SaveGroups();

                MessageBox.Show($"Gasto '{expense.Name}' agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFormPreservingGroup();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar gasto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // -------- helpers de limpieza --------
        private void ClearFormPreservingGroup()
        {
            txtExpenseName.Clear();
            txtDescription.Clear();
            txtAmount.Clear();

            // Mantener el grupo seleccionado y recargar miembros/pagador (por si cambiaron datos)
            var prev = cmbGroups.SelectedItem as Group;
            LoadGroups();
            if (prev != null)
            {
                var list = cmbGroups.DataSource as List<Group>;
                var idx = list?.FindIndex(g => g.GroupId == prev.GroupId) ?? -1;
                if (idx >= 0) cmbGroups.SelectedIndex = idx;
            }
        }
    }
}
