using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario que muestra todos los grupos donde participa el usuario actual
    /// junto con sus miembros y gastos en un control TreeView.
    /// </summary>
    public partial class MostrarForm : Form
    {
        /// <summary>
        /// Usuario actualmente logueado, usado para filtrar grupos.
        /// </summary>
        private readonly User currentUser;

        /// <summary>
        /// Constructor del formulario MostrarForm.
        /// Inicializa los componentes y carga los grupos del usuario.
        /// </summary>
        /// <param name="user">Usuario actualmente logueado. No puede ser null.</param>
        public MostrarForm(User user)
        {
            InitializeComponent();
            currentUser = user ?? throw new ArgumentNullException(nameof(user));

            // Carga los grupos en los que participa el usuario
            CargarGruposConDetalles();
        }

        /// <summary>
        /// Carga los grupos del usuario y sus detalles en el TreeView.
        /// Incluye miembros oficiales y participantes de gastos.
        /// </summary>
        private void CargarGruposConDetalles()
        {
            try
            {
                // Limpiar nodos existentes
                treeViewGrupos.Nodes.Clear();

                // Filtrar grupos donde el usuario está como miembro o participa en algún gasto
                var gruposDelUsuario = DataManager.Instance.Groups
                    .Where(grupo =>
                        (grupo.Members != null &&
                         grupo.Members.Contains(currentUser.Email, StringComparer.OrdinalIgnoreCase))
                        ||
                        DataManager.Instance.Expenses.Any(exp =>
                            exp.GroupId == grupo.GroupId &&
                            exp.InvolvedUsersEmails != null &&
                            exp.InvolvedUsersEmails.Contains(currentUser.Email, StringComparer.OrdinalIgnoreCase))
                    )
                    .ToList();

                // Crear nodos para cada grupo
                foreach (var grupo in gruposDelUsuario)
                {
                    var nodoGrupo = new TreeNode($"{grupo.GroupName} (ID: {grupo.GroupId})");

                    // Añadir nodos hijos: Miembros y Gastos
                    nodoGrupo.Nodes.Add(CrearNodoMiembros(grupo));
                    nodoGrupo.Nodes.Add(CrearNodoGastos(grupo));

                    treeViewGrupos.Nodes.Add(nodoGrupo);
                }

                // Expandir todos los nodos por defecto
                treeViewGrupos.ExpandAll();
            }
            catch (Exception ex)
            {
                // Mostrar mensaje amigable al usuario
                MessageBox.Show($"Error al cargar grupos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Crea un nodo TreeNode con todos los miembros de un grupo.
        /// Incluye miembros oficiales y participantes de gastos no oficiales.
        /// </summary>
        /// <param name="grupo">Grupo cuyos miembros se van a mostrar.</param>
        /// <returns>TreeNode con la lista de miembros del grupo.</returns>
        private static TreeNode CrearNodoMiembros(Group grupo)
        {
            var miembrosOficiales = grupo.Members ?? new List<string>();

            // Obtener miembros a partir de los gastos
            var miembrosDeGastos = DataManager.Instance.Expenses
                .Where(exp => exp.GroupId == grupo.GroupId && exp.InvolvedUsersEmails != null)
                .SelectMany(exp => exp.InvolvedUsersEmails)
                .Where(email => !string.IsNullOrWhiteSpace(email))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Unir miembros oficiales y de gastos, evitando duplicados
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
                // Mostrar nombre del usuario si existe, sino mostrar solo el email
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

        /// <summary>
        /// Crea un nodo TreeNode con todos los gastos asociados a un grupo.
        /// </summary>
        /// <param name="grupo">Grupo cuyos gastos se van a mostrar.</param>
        /// <returns>TreeNode con la lista de gastos del grupo.</returns>
        private static TreeNode CrearNodoGastos(Group grupo)
        {
            var nodoGastos = new TreeNode("Gastos");

            var gastosDelGrupo = DataManager.Instance.Expenses
                .Where(exp => exp.GroupId == grupo.GroupId)
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
