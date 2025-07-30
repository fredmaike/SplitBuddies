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
        // Controladores para manejar la lógica de gastos y grupos
        private readonly ExpenseController expenseController = new ExpenseController();
        private readonly GroupController groupController;
        private readonly User currentUser;

        // Constructor que recibe el usuario actual y configura el formulario
        public ExpenseForm(User user)
        {
            currentUser = user;
            InitializeComponent();

            groupController = new GroupController(DataManager.Instance.Groups);

            // Asignar el evento para cambio de selección de grupo
            cmbGroups.SelectedIndexChanged += CmbGroups_SelectedIndexChanged;

            // Cargar los grupos a los que pertenece el usuario
            LoadGroups();
        }

        // Carga los grupos filtrados por el usuario actual en el combo de grupos
        private void LoadGroups()
        {
            var groups = groupController.GetGroupsForUser(currentUser.Email);

            cmbGroups.DataSource = null;
            cmbGroups.DataSource = groups;
            cmbGroups.DisplayMember = "GroupName";  // Mostrar nombre del grupo
            cmbGroups.ValueMember = "GroupId";      // Valor interno es el ID del grupo
        }

        // Clase auxiliar para mostrar usuarios en combos y listas con nombre y email
        private sealed class UserItem
        {
            public string Name { get; set; }
            public string Email { get; set; }

            // Muestra nombre si existe, si no, email
            public override string ToString() => string.IsNullOrWhiteSpace(Name) ? Email : Name;
        }

        // Evento al seleccionar un grupo: carga usuarios y llena controles de pagador y miembros
        private void CmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cmbGroups.SelectedItem is Group))
            {
                // Cargar todos los usuarios (puede filtrarse si se desea)
                var users = DataManager.Instance.Users
                    .Select(u => new UserItem { Name = u.Name, Email = u.Email })
                    .ToList();

                // Asignar usuarios al combo de pagador
                cmbPaidBy.DataSource = null;
                cmbPaidBy.DataSource = users;
                cmbPaidBy.DisplayMember = "Name";
                cmbPaidBy.ValueMember = "Email";

                // Mostrar todos los usuarios en lista de miembros incluidos, marcados por defecto
                clbIncludedMembers.Items.Clear();
                foreach (var userItem in users)
                {
                    clbIncludedMembers.Items.Add(userItem, true);
                }
            }
            else
            {
                // Si no hay grupo seleccionado, limpiar controles relacionados
                cmbPaidBy.DataSource = null;
                clbIncludedMembers.Items.Clear();
            }
        }

        // Evento para validar y registrar un nuevo gasto al presionar el botón
        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            // Validaciones básicas de selección y datos ingresados
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

            // Obtener emails de los miembros seleccionados
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
                // Crear el gasto mediante el controlador
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

                // Limpiar formulario para nuevo ingreso
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar gasto: {ex.Message}");
            }
        }

        // Limpia los campos del formulario y selecciona el primer grupo si existe
        private void ClearForm()
        {
            txtExpenseName.Clear();
            txtDescription.Clear();
            txtAmount.Clear();
            if (cmbGroups.Items.Count > 0)
                cmbGroups.SelectedIndex = 0;
        }
    }
}
