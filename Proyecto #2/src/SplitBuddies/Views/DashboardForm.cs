using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Controllers;
using SplitBuddies.Models;
using SplitBuddies.Utils;

namespace SplitBuddies.Views
{
    public partial class DashboardForm : Form
    {
        private readonly string _currentUserEmail;
        private List<User> usuarios;
        private List<Group> grupos;
        private List<Expense> gastos;

        // El diseñador usa este constructor sin parámetros:
        public DashboardForm() : this(AppSession.CurrentUserEmail ?? string.Empty) { }

        // Constructor principal (filtra por usuario)
        public DashboardForm(string currentUserEmail)
        {
            _currentUserEmail = currentUserEmail ?? string.Empty;
            InitializeComponent();
            CargarDatos();
            InicializarInterfaz();
        }

        private void CargarDatos()
        {
            var dm = SplitBuddies.Data.DataManager.Instance;
            dm.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            dm.LoadUsers();
            dm.LoadGroups();
            dm.LoadExpenses();
            dm.LoadInvitations();

            usuarios = dm.Users;

            // Filtrar grupos por usuario (si no hay email, mostrar todos para no romper el diseñador)
            if (!string.IsNullOrWhiteSpace(_currentUserEmail))
                grupos = dm.Groups.Where(g => (g.Members ?? new List<string>()).Contains(_currentUserEmail, StringComparer.OrdinalIgnoreCase)).ToList();
            else
                grupos = dm.Groups.ToList();

            var groupIds = new HashSet<int>(grupos.Select(gr => gr.GroupId));
            gastos = dm.Expenses.Where(e => groupIds.Contains(e.GroupId)).ToList();
        }

        private void InicializarInterfaz()
        {
            this.Text = "SplitBuddies - Dashboard";
            this.Size = new Size(1000, 600);

            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            // -------- Usuarios --------
            var tabUsuarios = new TabPage("Usuarios");
            var listUsuarios = new ListBox { Dock = DockStyle.Fill };
            foreach (var u in usuarios)
                listUsuarios.Items.Add($"{u.Name} ({u.Email})");
            tabUsuarios.Controls.Add(listUsuarios);
            tabControl.TabPages.Add(tabUsuarios);

            // -------- Grupos --------
            var tabGrupos = new TabPage("Grupos");
            var listGrupos = new ListView
            {
                View = View.Details,
                Dock = DockStyle.Fill
            };
            listGrupos.Columns.Add("Nombre", 200);
            listGrupos.Columns.Add("Miembros", 600);

            foreach (var g in grupos)
            {
                string miembros = string.Join(", ", g.Members ?? new List<string>());
                var item = new ListViewItem(new[] { g.GroupName, miembros }) { Tag = g };
                listGrupos.Items.Add(item);
            }
            tabGrupos.Controls.Add(listGrupos);
            tabControl.TabPages.Add(tabGrupos);

            // -------- Gastos --------
            var tabGastos = new TabPage("Gastos");
            var gridGastos = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true,
                DataSource = gastos
            };
            tabGastos.Controls.Add(gridGastos);
            tabControl.TabPages.Add(tabGastos);

            // -------- Invitaciones --------
            var dm = SplitBuddies.Data.DataManager.Instance;
            var tabInv = new TabPage("Invitaciones");
            var panelInv = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true };
            tabInv.Controls.Add(panelInv);

            var pendientes = dm.Invitations
                .Where(i => string.Equals(i.Status, "Pending", StringComparison.OrdinalIgnoreCase))
                .Where(i => string.IsNullOrWhiteSpace(_currentUserEmail) || string.Equals(i.InviteeEmail, _currentUserEmail, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var inv in pendientes)
            {
                var grp = dm.Groups.FirstOrDefault(g => g.GroupId == inv.GroupId);

                var card = new Panel { Width = 900, Height = 80, BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(8) };

                var lbl = new Label
                {
                    AutoSize = true,
                    Text = $"Invitación al grupo: {(grp?.GroupName ?? "(desconocido)")} — Para: {inv.InviteeEmail} — De: {inv.InviterEmail ?? "N/D"}"
                };

                var btnAceptar = new Button { Text = "Aceptar", Width = 100, Height = 30, Margin = new Padding(8, 0, 8, 0) };
                var btnRechazar = new Button { Text = "Rechazar", Width = 100, Height = 30 };

                btnAceptar.Click += (s, e) =>
                {
                    if (grp != null && !string.IsNullOrWhiteSpace(inv.InviteeEmail))
                    {
                        grp.Members ??= new List<string>();
                        if (!grp.Members.Contains(inv.InviteeEmail, StringComparer.OrdinalIgnoreCase))
                            grp.Members.Add(inv.InviteeEmail);
                    }

                    inv.Status = "Accepted";
                    dm.SaveGroups();
                    dm.SaveInvitations();

                    MessageBox.Show("Invitación aceptada.");
                    CargarDatos();
                    InicializarInterfaz();
                };

                btnRechazar.Click += (s, e) =>
                {
                    inv.Status = "Rejected";
                    dm.SaveInvitations();
                    MessageBox.Show("Invitación rechazada.");
                    CargarDatos();
                    InicializarInterfaz();
                };

                var buttons = new FlowLayoutPanel { Dock = DockStyle.Right, Width = 240 };
                buttons.Controls.Add(btnAceptar);
                buttons.Controls.Add(btnRechazar);

                card.Controls.Add(lbl);
                card.Controls.Add(buttons);
                panelInv.Controls.Add(card);
            }

            tabControl.TabPages.Add(tabInv);

            // Montar
            this.Controls.Clear();
            this.Controls.Add(tabControl);
        }
    }
}



