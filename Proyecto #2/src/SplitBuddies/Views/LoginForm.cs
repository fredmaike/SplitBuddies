using SplitBuddies.Controllers;
using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario de inicio de sesión de la aplicación.
    /// Maneja validaciones, autenticación de usuario y carga de datos iniciales.
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Controlador para la gestión de usuarios.
        /// </summary>
        private readonly UserController userController = new UserController();

        /// <summary>
        /// Usuario autenticado actualmente.
        /// </summary>
        public User LoggedInUser { get; private set; }

        /// <summary>
        /// Constructor del formulario. Inicializa controles y carga datos.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            // Permite que el Enter active el botón de login
            this.AcceptButton = this.Controls.Find("btnLogin", true).FirstOrDefault() as Button;

            EnsureDataDirectory(); // Verifica carpeta Data y setea BasePath
            userController.LoadUsers();

            txtPassword.UseSystemPasswordChar = true;
        }

        #region Inicialización de Datos

        /// <summary>
        /// Verifica que exista la carpeta "Data" y establece el BasePath de DataManager.
        /// </summary>
        private static void EnsureDataDirectory()
        {
            var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);

            DataManager.Instance.BasePath = dataDir;
        }

        #endregion

        #region Evento de Login

        /// <summary>
        /// Maneja el clic en el botón de login.
        /// Valida campos, autentica usuario y carga datos de la sesión.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = (txtEmail?.Text ?? string.Empty).Trim();
                string password = (txtPassword?.Text ?? string.Empty).Trim();

                // Validaciones de campos
                ValidateFields(email, password);

                var dm = DataManager.Instance;
                dm.LoadUsers(); // Asegura que la lista esté actualizada

                var user = FindUserByEmail(dm, email); // Busca usuario existente
                ValidatePassword(user, password);       // Verifica contraseña

                // Autenticación en sesión
                AppSession.SignIn(user.Email);
                LoggedInUser = user;

                // Cargar datos adicionales
                dm.LoadGroups();
                dm.LoadExpenses();
                try { dm.LoadInvitations(); } catch { /* Ignorado si no existe archivo de invitaciones */ }

                // Mensaje de bienvenida
                MessageBox.Show($"¡Bienvenido {user.Name}!", "Login exitoso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ax)
            {
                ShowWarning(ax.Message);
            }
            catch (UnauthorizedAccessException ux)
            {
                ShowWarning(ux.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Validaciones

        /// <summary>
        /// Valida los campos de correo y contraseña.
        /// </summary>
        private static void ValidateFields(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Ingrese su correo.");
            if (!IsValidEmail(email))
                throw new ArgumentException("El formato del correo no es válido.");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Ingrese su contraseña.");
        }

        /// <summary>
        /// Busca un usuario por correo electrónico dentro del DataManager.
        /// </summary>
        private static User FindUserByEmail(DataManager dm, string email)
        {
            var user = dm.Users?.FirstOrDefault(u =>
                u != null &&
                !string.IsNullOrWhiteSpace(u.Email) &&
                string.Equals(u.Email.Trim(), email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                throw new UnauthorizedAccessException("Usuario no encontrado.");

            return user;
        }

        /// <summary>
        /// Valida que la contraseña ingresada coincida con la almacenada.
        /// </summary>
        private static void ValidatePassword(User user, string typedPassword)
        {
            string stored = (user.Password ?? string.Empty).Trim();
            if (!string.Equals(stored, typedPassword.Trim(), StringComparison.Ordinal))
                throw new UnauthorizedAccessException("Contraseña incorrecta.");
        }

        /// <summary>
        /// Valida si un correo electrónico tiene formato válido.
        /// </summary>
        private static bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            }
            catch { return false; }
        }

        /// <summary>
        /// Muestra un mensaje de advertencia y limpia la contraseña.
        /// </summary>
        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Advertencia",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            txtPassword.Clear();
            txtPassword.Focus();
        }

        #endregion

        #region Registro de Nuevo Usuario

        /// <summary>
        /// Maneja el clic en el botón de registro.
        /// Abre el formulario de registro y actualiza la lista de usuarios.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            using var registerForm = new RegisterForm();
            if (registerForm.ShowDialog() != DialogResult.OK) return;

            userController.LoadUsers();

            // Si se pasó un email desde el formulario de registro, colocarlo en el login
            if (!string.IsNullOrWhiteSpace(registerForm?.Tag as string))
            {
                txtEmail.Text = registerForm.Tag.ToString();
                txtPassword.Focus();
            }
        }

        #endregion
    }
}
