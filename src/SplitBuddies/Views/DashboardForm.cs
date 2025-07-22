
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Controllers;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public partial class DashboardForm : Form
    {
        private List<User> usuarios;
        private List<Group> grupos;
        private List<Expense> gastos;

        public DashboardForm()
        {
            InitializeComponent();
            CargarDatos();
            InicializarInterfaz();
        }

        private void CargarDatos()
        {
            usuarios = UserController.CargarUsuarios("Data/usuarios.json");
            grupos = GroupController.CargarGrupos("Data/grupos.json");
            gastos = ExpenseController.CargarGastos("Data/gastos.json");
        }

        private void InicializarInterfaz()
        {
            this.Text = "SplitBuddies - Dashboard";
            this.Size = new Size(1000, 600);

            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            // Usuarios
            TabPage tabUsuarios = new TabPage("Usuarios");
            ListBox listUsuarios = new ListBox { Dock = DockStyle.Fill };
            foreach (var u in usuarios)
                listUsuarios.Items.Add($"{u.Name} ({u.Email})");
            tabUsuarios.Controls.Add(listUsuarios);
            tabControl.TabPages.Add(tabUsuarios);

            // Grupos
            TabPage tabGrupos = new TabPage("Grupos");
            ListView listGrupos = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill
            };
            listGrupos.Columns.Add("Nombre", 200);
            listGrupos.Columns.Add("Miembros", 600);
            foreach (var g in grupos)
            {
                string miembros = string.Join(", ", g.Miembros);
                listGrupos.Items.Add(new ListViewItem(new[] { g.Nombre, miembros }));
            }
            tabGrupos.Controls.Add(listGrupos);
            tabControl.TabPages.Add(tabGrupos);

            // Gastos
            TabPage tabGastos = new TabPage("Gastos");
            DataGridView gridGastos = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                DataSource = gastos
            };
            tabGastos.Controls.Add(gridGastos);
            tabControl.TabPages.Add(tabGastos);

            this.Controls.Add(tabControl);
        }
    }
}
