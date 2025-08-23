using Newtonsoft.Json;
using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class EditGroupsForm : Form
    {
        private List<Group> grupos;
        private List<Expense> todosLosGastos;
        private readonly string jsonGroupsPath = "Data/grupos.json";
        private readonly string jsonExpensesPath = "Data/gastos.json";
        private readonly User currentUser;

        // Evento para notificar cambios en los gastos
        public event Action ExpensesUpdated;

        public EditGroupsForm(User user)
        {
            InitializeComponent();
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            CargarGrupos();
            CargarGastos();
        }

        #region Carga de Datos

        /// <summary>
        /// Carga los grupos a los que pertenece el usuario actual.
        /// </summary>
        private void CargarGrupos()
        {
            try
            {
                grupos = File.Exists(jsonGroupsPath)
                    ? JsonConvert.DeserializeObject<List<Group>>(File.ReadAllText(jsonGroupsPath))?
                        .Where(g => g.Members != null && g.Members.Contains(currentUser.Email, StringComparer.OrdinalIgnoreCase))
                        .ToList() ?? new List<Group>()
                    : new List<Group>();

                ActualizarListaGrupos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar grupos: {ex.Message}");
                grupos = new List<Group>();
            }
        }

        /// <summary>
        /// Carga todos los gastos desde el archivo JSON.
        /// </summary>
        private void CargarGastos()
        {
            try
            {
                todosLosGastos = File.Exists(jsonExpensesPath)
                    ? JsonConvert.DeserializeObject<List<Expense>>(File.ReadAllText(jsonExpensesPath)) ?? new List<Expense>()
                    : new List<Expense>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar gastos: {ex.Message}");
                todosLosGastos = new List<Expense>();
            }
        }

        #endregion

        #region Actualización de UI

        /// <summary>
        /// Refresca la lista de grupos en el ListBox.
        /// </summary>
        private void ActualizarListaGrupos()
        {
            listBoxGroups.Items.Clear();
            foreach (var grupo in grupos)
                listBoxGroups.Items.Add(grupo.GroupName);
        }

        /// <summary>
        /// Maneja la selección de grupo y actualiza los controles relacionados.
        /// </summary>
        private void listBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxGroups.SelectedIndex;
            if (index < 0 || index >= grupos.Count) return;

            var grupo = grupos[index];
            txtGroupName.Text = grupo.GroupName;
            txtMembers.Text = string.Join(", ", grupo.Members);

            ActualizarListaGastos(grupo);
            ActualizarMiembrosGasto(grupo);
            LimpiarCamposGasto();
        }

        /// <summary>
        /// Actualiza el ListBox de gastos del grupo seleccionado.
        /// </summary>
        private void ActualizarListaGastos(Group grupo)
        {
            listBoxExpenses.Items.Clear();
            foreach (var gasto in todosLosGastos.Where(g => g.GroupId == grupo.GroupId))
                listBoxExpenses.Items.Add(gasto);
        }

        /// <summary>
        /// Actualiza los miembros disponibles para los gastos.
        /// </summary>
        private void ActualizarMiembrosGasto(Group grupo)
        {
            clbMembersPaid.Items.Clear();
            foreach (var miembro in grupo.Members)
                clbMembersPaid.Items.Add(miembro);
        }

        /// <summary>
        /// Limpia los campos de entrada de gastos.
        /// </summary>
        private void LimpiarCamposGasto()
        {
            txtExpenseName.Text = "";
            txtAmount.Text = "";
            txtDescription.Text = "";
        }

        #endregion

        #region Guardar Cambios

        /// <summary>
        /// Guarda los cambios realizados en un grupo.
        /// </summary>
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            int index = listBoxGroups.SelectedIndex;
            if (index < 0) return;

            var grupo = grupos[index];
            grupo.GroupName = txtGroupName.Text.Trim();
            grupo.Members = txtMembers.Text.Split(',')
                                           .Select(m => m.Trim())
                                           .Where(m => !string.IsNullOrWhiteSpace(m))
                                           .ToList();

            GuardarGrupoEnArchivo(grupo);
            MessageBox.Show("Grupo actualizado correctamente.");
            CargarGrupos();
        }

        /// <summary>
        /// Actualiza la información del grupo en el archivo JSON global.
        /// </summary>
        private void GuardarGrupoEnArchivo(Group grupo)
        {
            try
            {
                var todosLosGrupos = File.Exists(jsonGroupsPath)
                    ? JsonConvert.DeserializeObject<List<Group>>(File.ReadAllText(jsonGroupsPath)) ?? new List<Group>()
                    : new List<Group>();

                var grupoGlobal = todosLosGrupos.FirstOrDefault(g => g.GroupId == grupo.GroupId);
                if (grupoGlobal != null)
                {
                    grupoGlobal.GroupName = grupo.GroupName;
                    grupoGlobal.Members = grupo.Members;
                }

                File.WriteAllText(jsonGroupsPath, JsonConvert.SerializeObject(todosLosGrupos, Formatting.Indented));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar grupo: {ex.Message}");
            }
        }

        #endregion

        #region Selección de Gasto

        private void listBoxExpenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxExpenses.SelectedItem is not Expense gasto) return;

            txtExpenseName.Text = gasto.Name;
            txtAmount.Text = gasto.Amount.ToString();
            txtDescription.Text = gasto.Description ?? "";

            var grupo = grupos[listBoxGroups.SelectedIndex];
            ActualizarCheckedListMiembros(gasto, grupo);
        }

        private void ActualizarCheckedListMiembros(Expense gasto, Group grupo)
        {
            clbMembersPaid.Items.Clear();
            var involved = gasto.InvolvedUsersEmails ?? new List<string>();

            foreach (var miembro in grupo.Members)
            {
                clbMembersPaid.Items.Add(miembro, involved.Contains(miembro));
            }
        }

        #endregion

        #region Editar y Borrar Gastos

        private void btnEditExpense_Click(object sender, EventArgs e)
        {
            if (listBoxGroups.SelectedItem == null || listBoxExpenses.SelectedItem is not Expense gastoSeleccionado) return;

            var grupo = DataManager.Instance.Groups.FirstOrDefault(g =>
                g.GroupName.Equals(listBoxGroups.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase) &&
                g.Members.Contains(currentUser.Email, StringComparer.OrdinalIgnoreCase));

            if (grupo == null) return;

            EditarGasto(gastoSeleccionado);
        }

        private void EditarGasto(Expense gasto)
        {
            try
            {
                var involved = clbMembersPaid.CheckedItems.Cast<string>().ToList();
                var gastoGlobal = DataManager.Instance.Expenses.FirstOrDefault(x => x.Id == gasto.Id);

                if (gastoGlobal != null)
                {
                    gastoGlobal.Name = txtExpenseName.Text.Trim();
                    gastoGlobal.Amount = decimal.Parse(txtAmount.Text);
                    gastoGlobal.Description = txtDescription.Text.Trim();
                    gastoGlobal.InvolvedUsersEmails = involved;
                    gastoGlobal.Date = DateTime.Now;
                }

                DataManager.Instance.SaveExpenses();
                listBoxGroups_SelectedIndexChanged(null, null);
                MessageBox.Show("Gasto editado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar gasto: {ex.Message}");
            }
        }

        private void btnDeleteExpense_Click(object sender, EventArgs e)
        {
            if (listBoxExpenses.SelectedItem is not Expense gasto) return;

            try
            {
                todosLosGastos.Remove(gasto);
                File.WriteAllText(jsonExpensesPath, JsonConvert.SerializeObject(todosLosGastos, Formatting.Indented));

                ExpensesUpdated?.Invoke();
                listBoxGroups_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al borrar gasto: {ex.Message}");
            }
        }

        #endregion
    }
}
