using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public partial class MostrarForm : Form
    {
        private readonly User currentUser; // ✅ usuario logueado

        public MostrarForm(User user)
        {
            InitializeComponent();
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            // Carga solo los grupos en los que participa el usuario
            CargarGruposConDetalles();
        }

        // Método principal que carga los grupos del usuario logueado y sus detalles
        private void CargarGruposConDetalles()
        {
            try
            {
                treeViewGrupos.Nodes.Clear();

                // ✅ Filtrar solo grupos donde participa el usuario actual
                var gruposDelUsuario = DataManager.Instance.Groups
                    .Where(grupo =>
                        (grupo.Members != null &&
                         grupo.Members.Contains(currentUser.Email, StringComparer.OrdinalIgnoreCase))
                        ||
                        DataManager.Instance.Expenses.Any(g =>
                            g.GroupId == grupo.GroupId &&
                            g.InvolvedUsersEmails != null &&
                            g.InvolvedUsersEmails.Contains(currentUser.Email, StringComparer.OrdinalIgnoreCase))
                    )
                    .ToList();

                foreach (var grupo in gruposDelUsuario)
                {
                    var nodoGrupo = new TreeNode($"{grupo.GroupName} (ID: {grupo.GroupId})");

                    var nodoMiembros = CrearNodoMiembros(grupo);
                    var nodoGastos = CrearNodoGastos(grupo);

                    nodoGrupo.Nodes.Add(nodoMiembros);
                    nodoGrupo.Nodes.Add(nodoGastos);

                    treeViewGrupos.Nodes.Add(nodoGrupo);
                }

                treeViewGrupos.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar grupos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ Los métodos CrearNodoMiembros y CrearNodoGastos quedan igual
        private static TreeNode CrearNodoMiembros(Group grupo)
        {
            var miembrosOficiales = grupo.Members ?? new List<string>();

            var miembrosDeGastos = DataManager.Instance.Expenses
                .Where(g => g.GroupId == grupo.GroupId && g.InvolvedUsersEmails != null)
                .SelectMany(g => g.InvolvedUsersEmails)
                .Where(email => !string.IsNullOrWhiteSpace(email))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var todosLosEmails = miembrosOficiales
                .Union(miembrosDeGastos, StringComparer.OrdinalIgnoreCase)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var nodoMiembros = new TreeNode("Miembros");

            if (todosLosEmails.Count == 0)
            {
                nodoMiembros.Nodes.Add("No hay miembros");
            }
            else
            {
                foreach (var email in todosLosEmails)
                {
                    var usuario = DataManager.Instance.Users
                        .FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                    string nombreMostrado = usuario != null ? $"{usuario.Name} ({email})" : email;

                    nodoMiembros.Nodes.Add(nombreMostrado);
                }
            }

            return nodoMiembros;
        }

        private static TreeNode CrearNodoGastos(Group grupo)
        {
            var nodoGastos = new TreeNode("Gastos");

            var gastosDelGrupo = DataManager.Instance.Expenses
                .Where(g => g.GroupId == grupo.GroupId)
                .ToList();

            if (gastosDelGrupo.Count == 0)
            {
                nodoGastos.Nodes.Add("No hay gastos");
            }
            else
            {
                foreach (var gasto in gastosDelGrupo)
                {
                    string textoGasto = $"{gasto.Name} - {gasto.Amount:C} - {gasto.Description}";
                    nodoGastos.Nodes.Add(new TreeNode(textoGasto));
                }
            }

            return nodoGastos;
        }
    }
}
