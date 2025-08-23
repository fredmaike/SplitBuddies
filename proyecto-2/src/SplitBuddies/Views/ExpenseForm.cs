using System;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario para registrar gastos dentro de un grupo.
    /// Permite seleccionar el grupo, el pagador, los miembros involucrados,
    /// ingresar el monto, descripción y nombre del gasto.
    /// </summary>
    public partial class ExpenseForm : Form
    {
        /// <summary>
        /// Usuario actualmente autenticado.
        /// </summary>
        private readonly User _currentUser;

        /// <summary>
        /// Inicializa una nueva instancia del formulario de gastos.
        /// </summary>
        /// <param name="user">Usuario autenticado que agregará gastos.</param>
        /// <exception cref="ArgumentNullException">Si el parámetro <paramref name="user"/> es null.</exception>
        public ExpenseForm(User user)
        {
            InitializeComponent();
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            CargarGruposDelUsuario();
        }

        /// <summary>
        /// Carga los grupos a los que pertenece el usuario actual en el ComboBox.
        /// </summary>
        private void CargarGruposDelUsuario()
        {
            var grupos = DataManager.Instance.Groups
                .Where(g => g.Members.Contains(_currentUser.Email, StringComparer.OrdinalIgnoreCase))
                .ToList();

            cmbGroups.DataSource = grupos;
            cmbGroups.DisplayMember = nameof(Group.GroupName);
            cmbGroups.SelectedIndex = 0;
        }

        /// <summary>
        /// Evento disparado al cambiar el grupo seleccionado.
        /// Actualiza el ComboBox de pagador y la lista de miembros involucrados.
        /// </summary>
        private void CmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedItem is not Group grupoSeleccionado) return;

            cmbPaidBy.DataSource = grupoSeleccionado.Members.ToList();
            clbIncludedMembers.Items.Clear();
            foreach (var miembro in grupoSeleccionado.Members)
            {
                clbIncludedMembers.Items.Add(miembro, true); // todos seleccionados por defecto
            }
        }

        /// <summary>
        /// Evento disparado al hacer clic en el botón "Agregar gasto".
        /// Valida los datos, crea y guarda el gasto, y limpia los campos del formulario.
        /// </summary>
        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            if (!ValidarEntrada(out Group grupoSeleccionado, out decimal monto, out var miembrosInvolucrados))
                return;

            var nuevoGasto = CrearGasto(grupoSeleccionado, monto, miembrosInvolucrados);
            GuardarGasto(nuevoGasto);

            LimpiarCampos();

            MessageBox.Show("Gasto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Valida los campos del formulario antes de crear un gasto.
        /// </summary>
        /// <param name="grupoSeleccionado">Grupo seleccionado en el ComboBox.</param>
        /// <param name="monto">Monto ingresado en el formulario.</param>
        /// <param name="miembrosInvolucrados">Lista de miembros involucrados seleccionados.</param>
        /// <returns>true si los datos son válidos; false en caso contrario.</returns>
        private bool ValidarEntrada(out Group grupoSeleccionado, out decimal monto, out System.Collections.Generic.List<string> miembrosInvolucrados)
        {
            grupoSeleccionado = cmbGroups.SelectedItem as Group;
            monto = 0;
            miembrosInvolucrados = clbIncludedMembers.CheckedItems.Cast<string>().ToList();

            if (grupoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un grupo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text.Trim(), out monto) || monto <= 0)
            {
                MessageBox.Show("Monto inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (miembrosInvolucrados.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un miembro involucrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Crea un objeto <see cref="Expense"/> con los datos del formulario.
        /// </summary>
        /// <param name="grupo">Grupo al que pertenece el gasto.</param>
        /// <param name="monto">Monto del gasto.</param>
        /// <param name="miembros">Miembros involucrados en el gasto.</param>
        /// <returns>Instancia de <see cref="Expense"/> lista para guardar.</returns>
        private Expense CrearGasto(Group grupo, decimal monto, System.Collections.Generic.List<string> miembros)
        {
            return new Expense
            {
                Id = DataManager.Instance.GetNextExpenseId(),
                GroupId = grupo.GroupId,
                Name = txtExpenseName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                PaidByEmail = cmbPaidBy.SelectedItem?.ToString() ?? "",
                Amount = monto,
                Date = DateTime.Now,
                InvolvedUsersEmails = miembros
            };
        }

        /// <summary>
        /// Agrega el gasto al DataManager y persiste en archivo JSON.
        /// </summary>
        /// <param name="gasto">Gasto a guardar.</param>
        private static void GuardarGasto(Expense gasto)
        {
            DataManager.Instance.Expenses.Add(gasto);
            DataManager.Instance.SaveExpenses();
        }

        /// <summary>
        /// Limpia los campos del formulario después de agregar un gasto.
        /// </summary>
        private void LimpiarCampos()
        {
            txtExpenseName.Clear();
            txtDescription.Clear();
            txtAmount.Clear();
        }
    }
}
