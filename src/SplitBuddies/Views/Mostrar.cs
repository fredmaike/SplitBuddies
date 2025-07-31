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
        public MostrarForm()
        {
            InitializeComponent();
            // Carga todos los grupos y sus detalles cuando se inicia el formulario
            CargarGruposConDetalles();
        }

        // Método principal que carga todos los grupos existentes y sus detalles (miembros y gastos)
        private void CargarGruposConDetalles()
        {
            try
            {
                // Limpia el TreeView antes de cargar nuevos datos
                treeViewGrupos.Nodes.Clear();

                // Recorre todos los grupos registrados
                foreach (var grupo in DataManager.Instance.Groups)
                {
                    // Crea el nodo principal del grupo
                    var nodoGrupo = new TreeNode($"{grupo.GroupName} (ID: {grupo.GroupId})");

                    // Agrega nodo de miembros al grupo
                    var nodoMiembros = CrearNodoMiembros(grupo);

                    // Agrega nodo de gastos al grupo
                    var nodoGastos = CrearNodoGastos(grupo);

                    // Inserta los nodos al nodo del grupo
                    nodoGrupo.Nodes.Add(nodoMiembros);
                    nodoGrupo.Nodes.Add(nodoGastos);

                    // Agrega el grupo completo al TreeView
                    treeViewGrupos.Nodes.Add(nodoGrupo);
                }

                // Expande todos los nodos para mejor visibilidad
                treeViewGrupos.ExpandAll();
            }
            catch (Exception ex)
            {
                // Muestra mensaje de error si ocurre alguna excepción
                MessageBox.Show($"Error al cargar grupos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método que construye el nodo de miembros para un grupo dado
        private static TreeNode CrearNodoMiembros(Group grupo)
        {
            // Obtiene los miembros oficiales definidos en el grupo
            var miembrosOficiales = grupo.Members ?? new List<string>();

            // Extrae los correos electrónicos de usuarios que participaron en gastos
            var miembrosDeGastos = DataManager.Instance.Expenses
                .Where(g => g.GroupId == grupo.GroupId && g.InvolvedUsersEmails != null)
                .SelectMany(g => g.InvolvedUsersEmails)
                .Where(email => !string.IsNullOrWhiteSpace(email))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Une los miembros oficiales con los participantes en gastos, eliminando duplicados
            var todosLosEmails = miembrosOficiales
                .Union(miembrosDeGastos, StringComparer.OrdinalIgnoreCase)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Crea nodo padre "Miembros"
            var nodoMiembros = new TreeNode("Miembros");

            if (todosLosEmails.Count == 0)
            {
                // Si no hay miembros, se muestra mensaje
                nodoMiembros.Nodes.Add("No hay miembros");
            }
            else
            {
                // Agrega cada miembro como subnodo
                foreach (var email in todosLosEmails)
                {
                    // Busca al usuario por su correo para mostrar su nombre si está registrado
                    var usuario = DataManager.Instance.Users
                        .FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                    string nombreMostrado = usuario != null ? $"{usuario.Name} ({email})" : email;

                    nodoMiembros.Nodes.Add(nombreMostrado);
                }
            }

            return nodoMiembros;
        }

        // Método que construye el nodo de gastos para un grupo dado
        private static TreeNode CrearNodoGastos(Group grupo)
        {
            // Crea nodo padre "Gastos"
            var nodoGastos = new TreeNode("Gastos");

            // Filtra los gastos que pertenecen al grupo actual
            var gastosDelGrupo = DataManager.Instance.Expenses
                .Where(g => g.GroupId == grupo.GroupId)
                .ToList();

            if (gastosDelGrupo.Count == 0)
            {
                // Si no hay gastos, se muestra mensaje
                nodoGastos.Nodes.Add("No hay gastos");
            }
            else
            {
                // Agrega cada gasto como subnodo con su descripción
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
