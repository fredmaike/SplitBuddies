using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario principal de la aplicación SplitBuddies.
    /// Permite al usuario gestionar grupos, gastos, ver invitaciones y guardar datos.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Usuario actualmente logueado.
        /// </summary>
        private User currentUser;

        /// <summary>
        /// Constructor del formulario principal.
        /// </summary>
        /// <param name="user">Usuario autenticado que abre la sesión.</param>
        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user ?? throw new ArgumentNullException(nameof(user));

            // Suscribirse al evento Load para inicializar la UI
            Load += MainForm_Load;
        }

        #region Static Helpers

        /// <summary>
        /// Asegura que los datos esenciales estén cargados desde la carpeta "Data".
        /// </summary>
        private static void EnsureDataLoaded()
        {
            var dm = DataManager.Instance;

            // Configurar ruta base de datos
            dm.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dm.BasePath))
                Directory.CreateDirectory(dm.BasePath);

            // Cargar datos principales
            dm.LoadUsers();
            dm.LoadGroups();
            dm.LoadExpenses();

            // Intentar cargar invitaciones, ignorando si el archivo aún no existe
            try
            {
                dm.LoadInvitations();
            }
            catch (FileNotFoundException)
            {
                // Ignorado: archivo de invitaciones no existe aún
            }
            catch (Exception ex)
            {
                // Log opcional de otros errores
                Console.WriteLine("Error al cargar invitaciones: " + ex.Message);
            }
        }

        #endregion

        #region UI Helpers

        /// <summary>
        /// Actualiza la etiqueta de bienvenida y el título de la ventana con el usuario actual.
        /// </summary>
        private void RefreshWelcome()
        {
            var email = currentUser?.Email ?? "(sin email)";
            var name = currentUser?.Name ?? "(Usuario)";
            lblWelcome.Text = $"Bienvenido, {name}!";
            this.Text = $"SplitBuddies — {name} <{email}>";
        }

        /// <summary>
        /// Actualiza la información de estado, grupos y deudas del usuario en el formulario.
        /// </summary>
        private void RefreshUserInfo()
        {
            if (currentUser == null) return;

            lblStatus.Text = "Estatus: Activo";

            // ----- Grupos del usuario -----
            var userGroups = DataManager.Instance.Groups
                .Where(g => g.Members != null && g.Members.Contains(currentUser.Email))
                .Select(g => g.GroupName)
                .ToList();

            lblGroups.Text = userGroups.Count > 0
                ? $"Grupo(s): {string.Join(", ", userGroups)}"
                : "Grupo(s): Ninguno";

            // ----- Deudas del usuario -----
            var debtsWithAmount = DataManager.Instance.Expenses
                .SelectMany(exp =>
                {
                    var participants = exp.InvolvedUsersEmails ?? new System.Collections.Generic.List<string>();
                    if (!participants.Contains(exp.PaidByEmail))
                        participants.Add(exp.PaidByEmail);

                    return participants
                        .Where(u => u != exp.PaidByEmail)
                        .Select(u => new { Debtor = u, Creditor = exp.PaidByEmail, Amount = exp.Amount / participants.Count });
                })
                .Where(d => d.Debtor == currentUser.Email)
                .GroupBy(d => d.Creditor)
                .Select(g => $"{g.Key} (${g.Sum(x => x.Amount):F2})")
                .ToList();

            lblDebts.Text = debtsWithAmount.Count > 0
                ? $"Debe a: {string.Join(", ", debtsWithAmount)}"
                : "Debe a: Ninguno";
        }

        #endregion

        #region Eventos del formulario

        /// <summary>
        /// Evento Load del formulario: asegura que los datos estén cargados y actualiza la UI.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            EnsureDataLoaded();

            if (!AppSession.IsAuthenticated && !string.IsNullOrWhiteSpace(currentUser?.Email))
                AppSession.SignIn(currentUser.Email);

            RefreshWelcome();
            RefreshUserInfo();
        }

        /// <summary>
        /// Muestra el formulario de gestión de grupos.
        /// </summary>
        private void btnGroups_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            using var groupForm = new GroupForm(currentUser);
            groupForm.ShowDialog();
            RefreshUserInfo();
        }

        /// <summary>
        /// Muestra el formulario de gestión de gastos.
        /// </summary>
        private void btnExpenses_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            using var expenseForm = new ExpenseForm(currentUser);
            expenseForm.ShowDialog();
            RefreshUserInfo();
        }

        /// <summary>
        /// Guarda todos los datos (usuarios, grupos, gastos e invitaciones) de manera segura.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dm = DataManager.Instance;
                dm.SaveUsers();
                dm.SaveGroups();
                dm.SaveExpenses();

                // Guardar invitaciones, ignorando fallos
                try { dm.SaveInvitations(); }
                catch (Exception ex)
                {
                    Console.WriteLine("No se pudo guardar invitaciones: " + ex.Message);
                }

                MessageBox.Show("Datos guardados.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Muestra el formulario MostrarForm con los grupos y gastos del usuario.
        /// </summary>
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            using var mostrarForm = new MostrarForm(currentUser);
            mostrarForm.ShowDialog();
        }

        /// <summary>
        /// Cierra la sesión actual y abre el formulario de login.
        /// </summary>
        private void btnLogout_Click(object sender, EventArgs e)
        {
            AppSession.SignOut();
            this.Hide();

            using var loginForm = new LoginForm();
            var result = loginForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                currentUser = loginForm.LoggedInUser ?? currentUser;
                if (currentUser != null && !string.IsNullOrWhiteSpace(currentUser.Email))
                    AppSession.SignIn(currentUser.Email);

                EnsureDataLoaded();
                RefreshWelcome();
                RefreshUserInfo();
                this.Show();
            }
            else
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Abre el formulario para editar grupos y gastos.
        /// </summary>
        private void btnEditGroups_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            using var editForm = new EditGroupsForm(currentUser);
            editForm.ShowDialog();
            RefreshUserInfo();
        }

        /// <summary>
        /// Muestra el listado de invitaciones pendientes del usuario.
        /// </summary>
        private void BtnInvitations_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            using var invitationsForm = new InvitationList(currentUser);
            invitationsForm.ShowDialog();
            RefreshUserInfo();
        }

        #endregion
    }
}
