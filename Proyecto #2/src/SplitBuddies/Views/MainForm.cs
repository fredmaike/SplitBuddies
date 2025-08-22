using System;
using System.IO;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;

namespace SplitBuddies.Views
{
    public partial class MainForm : Form
    {
        // Usuario actualmente autenticado en la aplicación
        private User currentUser;

        // Constructor que recibe el usuario actual y configura el formulario
        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            Load += MainForm_Load; // Evento que se ejecuta al cargar el formulario
        }

        // ===== Helpers =====
        private void EnsureDataLoaded()
        {
            var dm = DataManager.Instance;

            // BasePath por si se abrió la app desde otro lugar
            dm.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dm.BasePath))
                Directory.CreateDirectory(dm.BasePath);

            // Cargar datos (idempotente)
            dm.LoadUsers();
            dm.LoadGroups();
            dm.LoadExpenses();
            try { dm.LoadInvitations(); } catch { /* si no existe, lista vacía */ }
        }

        private void RefreshWelcome()
        {
            var email = currentUser?.Email ?? "(sin email)";
            var name = currentUser?.Name ?? "(Usuario)";
            lblWelcome.Text = $"Bienvenido, {name}!";
            this.Text = $"SplitBuddies — {name} <{email}>";
        }

        // ===== Eventos =====
        // Evento que se ejecuta cuando el formulario se carga
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Asegurar datos y sesión
            EnsureDataLoaded();
            if (!AppSession.IsAuthenticated && !string.IsNullOrWhiteSpace(currentUser?.Email))
                AppSession.SignIn(currentUser.Email);

            RefreshWelcome();

            // Si tienes un botón Dashboard en el diseñador, puedes enlazarlo así:
          
        }

        // Evento para abrir el formulario de gestión de grupos
        private void btnGroups_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            using (var groupForm = new GroupForm(currentUser))
            {
                groupForm.ShowDialog();
            }
        }

        // Evento para abrir el formulario de gestión de gastos
        private void btnExpenses_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            using (var expenseForm = new ExpenseForm(currentUser))
            {
                expenseForm.ShowDialog();
            }
        }

        // Evento para guardar todos los datos (usuarios, grupos, gastos, invitaciones) en archivos JSON
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dm = DataManager.Instance;
                dm.SaveUsers();        // usa usuarios.json
                dm.SaveGroups();       // usa grupos.json
                dm.SaveExpenses();     // usa gastos.json
                try { dm.SaveInvitations(); } catch { /* por si aún no usas invitaciones */ }

                MessageBox.Show("Datos guardados.", "Guardado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento para mostrar información combinada de grupos y gastos
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return; 
            using (var mostrarForm = new MostrarForm(currentUser)) 
            {
                mostrarForm.ShowDialog();
            }
        }

        // Evento para cerrar sesión y volver al formulario de login
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Cerrar sesión
            AppSession.SignOut();

            this.Hide(); // Ocultar el formulario actual
            using (var loginForm = new LoginForm())
            {
                var result = loginForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Login exitoso: actualizar usuario actual, sesión y UI
                    currentUser = loginForm.LoggedInUser ?? currentUser;
                    if (currentUser != null && !string.IsNullOrWhiteSpace(currentUser.Email))
                        AppSession.SignIn(currentUser.Email);

                    EnsureDataLoaded();
                    RefreshWelcome();
                    this.Show();
                }
                else
                {
                    // Cancelado: cerrar la aplicación
                    Application.Exit();
                }
            }
        }

        // Evento para abrir un formulario para editar grupos (se asume que existe EditGroupsForm)
        private void btnEditGroups_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return; 
            using (var editForm = new EditGroupsForm(currentUser)) 
            {
                editForm.ShowDialog();
            }
        }
        private void BtnInvitations_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            using (var invitationsForm = new InvitationList(currentUser))
            {
                invitationsForm.ShowDialog();
            }
        }

    }
}
