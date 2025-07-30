using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class MostrarForm : Form
    {
        public MostrarForm()
        {
            InitializeComponent();
            CargarGruposConDetalles();
        }

        private void CargarGruposConDetalles()
        {
            try
            {
                treeViewGrupos.Nodes.Clear();

                foreach (var grupo in DataManager.Instance.Groups)
                {
                    var nodoGrupo = new TreeNode($"{grupo.GroupName} (ID: {grupo.GroupId})");

                    // Sección de Miembros
                   
                    var miembrosOficiales = grupo.Members ?? new List<string>();

               
                    var miembrosDeGastos = DataManager.Instance.Expenses
                        .Where(g => g.GroupId == grupo.GroupId && g.InvolvedUsersEmails != null)
                        .SelectMany(g => g.InvolvedUsersEmails)
                        .Distinct()
                        .ToList();

           
                    var miembrosTotales = miembrosOficiales.Union(miembrosDeGastos).Distinct().ToList();

                    var nodoMiembros = new TreeNode("Miembros");
                    if (miembrosTotales.Count == 0)
                    {
                        nodoMiembros.Nodes.Add("No hay miembros");
                    }
                    else
                    {
                        foreach (var emailMiembro in miembrosTotales)
                        {
                            var usuario = DataManager.Instance.Users
                                .FirstOrDefault(u => u.Email.Equals(emailMiembro, StringComparison.OrdinalIgnoreCase));
                            string nombre = usuario != null ? usuario.Name : emailMiembro;
                            nodoMiembros.Nodes.Add(nombre);
                        }
                    }
                    nodoGrupo.Nodes.Add(nodoMiembros);


                    // Sección de Gastos
                    var nodoGastos = new TreeNode("Gastos");

                    // Buscar gastos de este grupo filtrando en Expenses por GroupId
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

                    nodoGrupo.Nodes.Add(nodoGastos);

                    treeViewGrupos.Nodes.Add(nodoGrupo);
                }

                treeViewGrupos.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar grupos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}