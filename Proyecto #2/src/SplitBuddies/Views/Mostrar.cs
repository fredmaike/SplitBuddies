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
            CargarGruposConDetalles();
        }

        // Método principal que carga los grupos y sus detalles en el TreeView
        private void CargarGruposConDetalles()
        {
            try
            {
                // Limpiar nodos previos para evitar duplicados
                treeViewGrupos.Nodes.Clear();

                // Recorrer cada grupo registrado en DataManager
                foreach (var grupo in DataManager.Instance.Groups)
                {
                    // Crear nodo principal para el grupo, mostrando nombre e ID
                    var nodoGrupo = new TreeNode($"{grupo.GroupName} (ID: {grupo.GroupId})");

                    // Crear subnodos de miembros y gastos usando métodos auxiliares
                    var nodoMiembros = CrearNodoMiembros(grupo);
                    var nodoGastos = CrearNodoGastos(grupo);

                    // Agregar los subnodos al nodo principal del grupo
                    nodoGrupo.Nodes.Add(nodoMiembros);
                    nodoGrupo.Nodes.Add(nodoGastos);

                    // Agregar el nodo del grupo al TreeView
                    treeViewGrupos.Nodes.Add(nodoGrupo);
                }

                // Expandir todos los nodos para mostrar detalles
                treeViewGrupos.ExpandAll();
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error si ocurre alguna excepción
                MessageBox.Show($"Error al cargar grupos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método estático que crea el nodo de miembros para un grupo dado
        private static TreeNode CrearNodoMiembros(Group grupo)
        {
            // Obtener miembros oficiales del grupo (lista de emails)
            var miembrosOficiales = grupo.Members ?? new List<string>();

            // Obtener miembros que han participado en gastos del grupo, para incluirlos aunque no estén en la lista oficial
            var miembrosDeGastos = DataManager.Instance.Expenses
                .Where(g => g.GroupId == grupo.GroupId && g.InvolvedUsersEmails != null)
                .SelectMany(g => g.InvolvedUsersEmails)
                .Distinct()
                .ToList();

            // Unir ambas listas y eliminar duplicados
            var miembrosTotales = miembrosOficiales.Union(miembrosDeGastos).Distinct().ToList();

            var nodoMiembros = new TreeNode("Miembros");

            if (miembrosTotales.Count == 0)
            {
                // Si no hay miembros, agregar nodo indicativo
                nodoMiembros.Nodes.Add("No hay miembros");
            }
            else
            {
                // Para cada email, buscar el nombre del usuario y agregarlo
                foreach (var emailMiembro in miembrosTotales)
                {
                    var usuario = DataManager.Instance.Users
                        .FirstOrDefault(u => u.Email.Equals(emailMiembro, StringComparison.OrdinalIgnoreCase));
                    string nombre = usuario != null ? usuario.Name : emailMiembro;
                    nodoMiembros.Nodes.Add(nombre);
                }
            }
            return nodoMiembros;
        }

        // Método estático que crea el nodo de gastos para un grupo dado
        private static TreeNode CrearNodoGastos(Group grupo)
        {
            var nodoGastos = new TreeNode("Gastos");

            // Filtrar los gastos correspondientes al grupo
            var gastosDelGrupo = DataManager.Instance.Expenses
                .Where(g => g.GroupId == grupo.GroupId)
                .ToList();

            if (gastosDelGrupo.Count == 0)
            {
                // Si no hay gastos, mostrar mensaje indicativo
                nodoGastos.Nodes.Add("No hay gastos");
            }
            else
            {
                // Para cada gasto, mostrar nombre, monto y descripción
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
