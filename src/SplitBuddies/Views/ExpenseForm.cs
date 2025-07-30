using System;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Controllers;
using SplitBuddies.Models;
using SplitBuddies.Data;
using System.Collections.Generic;

namespace SplitBuddies.Views
{
    public partial class ExpenseForm : Form
    {
        private readonly ExpenseController expenseController = new ExpenseController();
        private readonly GroupController groupController;
        private readonly User currentUser;

        // Constructor del formulario, recibe al usuario actual
        public ExpenseForm(User user)
        {
            currentUser = user;
            InitializeComponent();

            groupController = new GroupController(DataManager.Instance.Groups);

            // Evento que maneja cuando cambia el grupo seleccionado
            cmbGroups.SelectedIndexChanged += CmbGroups_SelectedIndexChanged;

            // Cargar los grupos del usuario al iniciar
            LoadGroups();
        }

        // Carga los grupos del usuario en el combo cmbGroups
        private void LoadGroups()
        {
            var groups = groupController.GetGroupsForUser(currentUser.Email);

            cmbGroups.DataSource = null;
            cmbGroups.DataSource = groups;
            cmbGroups.DisplayMember = "GroupName";
            cmbGroups.ValueMember = "GroupId";
        }

        // Clase auxiliar para mostrar usuarios en combos y listas
        private sealed class UserItem
        {
            public string Name { get; set; }
            public string Email { get; set; }

            public override string ToString() => string.IsNullOrWhiteSpace(Name) ? Email : Name;
        }

        // Al seleccionar un grupo, carga los usuarios disponibles
        private void CmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedItem is Group selectedGroup)
            {
                // Mostrar nombre del grupo (para depuración)
                MessageBox.Show($"Grupo seleccionado: {selectedGroup.GroupName}");

                // Cargar todos los usuarios registrados (¿debería filtrarse por grupo?)
                var users = DataManager.Instance.Users
                    .Select(u => new UserItem { Name = u.Name, Email = u.Email })
                    .ToList();

                MessageBox.Show($"Usuarios encontrados: {users.Count}");

                // Asignar al combo de pagador
                cmbPaidBy.DataSource = null;
                cmbPaidBy.DataSource = users;
                cmbPaidBy.DisplayMember = "Name";
                cmbPaidBy.ValueMember = "Email";

                // Mostrar los usuarios en la lista de miembros incluidos
                clbIncludedMembers.Items.Clear();
                foreach (var userItem in users)
                {
                    clbIncludedMembers.Items.Add(userItem, true); // Marcar todos como incluidos por defecto
                }
            }
            else
            {
                // Limpiar si no hay grupo seleccionado
                cmbPaidBy.DataSource = null;
                clbIncludedMembers.Items.Clear();
            }
        }

        // Validación y registro del gasto al hacer clic en "Agregar"
        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedItem is not Group selectedGroup)
            {
                MessageBox.Show("Seleccione un grupo.");
                return;
            }

            if (cmbPaidBy.SelectedItem == null)
            {
                MessageBox.Show("Seleccione quién pagó.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtExpenseName.Text))
            {
                MessageBox.Show("Ingrese un nombre para el gasto.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            var involvedEmails = clbIncludedMembers.CheckedItems
                .Cast<UserItem>()
                .Select(u => u.Email)
                .ToList();

            if (involvedEmails.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un miembro incluido.");
                return;
            }

            string payerEmail = (cmbPaidBy.SelectedItem as UserItem)?.Email;

            try
            {
                var expense = expenseController.AddExpense(
                    txtExpenseName.Text.Trim(),
                    txtDescription.Text.Trim(),
                    payerEmail,
                    involvedEmails,
                    amount,
                    DateTime.Now,
                    selectedGroup.GroupId
                );

                MessageBox.Show($"Gasto '{expense.Name}' agregado exitosamente.");
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar gasto: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            txtExpenseName.Clear();
            txtDescription.Clear();
            txtAmount.Clear();
            if (cmbGroups.Items.Count > 0) cmbGroups.SelectedIndex = 0;
        }
    }
}