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
    public partial class LoginForm : Form
    {
        private readonly UserController userController = new UserController();
        public User LoggedInUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();

            if (this.Controls.Find("btnLogin", true).FirstOrDefault() is Button bLogin)
                this.AcceptButton = bLogin;

            // Asegurar BasePath y carpeta Data
            var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);
            DataManager.Instance.BasePath = dataDir;

            // Cargar usuarios desde DataManager.BasePath
            userController.LoadUsers();

            if (this.Controls.Find("txtPassword", true).FirstOrDefault() is TextBox pwd)
                pwd.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var txtEmail = this.Controls.Find("txtEmail", true).FirstOrDefault() as TextBox;
                var txtPassword = this.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;

                string email = (txtEmail?.Text ?? string.Empty).Trim();
                string password = (txtPassword?.Text ?? string.Empty).Trim();

                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentException("Ingrese su correo.");
                if (!IsValidEmail(email))
                    throw new ArgumentException("El formato del correo no es válido.");
                if (string.IsNullOrWhiteSpace(password))
                    throw new ArgumentException("Ingrese su contraseña.");

                var dm = DataManager.Instance;
                if (string.IsNullOrWhiteSpace(dm.BasePath))
                    dm.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

                dm.LoadUsers(); // idempotente

                var user = dm.Users?.FirstOrDefault(u =>
                    u != null &&
                    !string.IsNullOrWhiteSpace(u.Email) &&
                    string.Equals(u.Email.Trim(), email, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                    throw new UnauthorizedAccessException("Usuario no encontrado.");

                var stored = (user.Password ?? string.Empty).Trim();
                var typed  = password.Trim();

                if (!string.Equals(stored, typed, StringComparison.Ordinal))
                    throw new UnauthorizedAccessException("Contraseña incorrecta.");

                AppSession.SignIn(user.Email);
                LoggedInUser = user;

                dm.LoadGroups();
                dm.LoadExpenses();
                try { dm.LoadInvitations(); } catch { }

                MessageBox.Show($"¡Bienvenido {user.Name}!", "Login exitoso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ax)
            {
                MessageBox.Show(ax.Message, "Campos inválidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                var pwd = this.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;
                pwd?.Clear(); pwd?.Focus();
            }
            catch (UnauthorizedAccessException ux)
            {
                MessageBox.Show(ux.Message, "Credenciales",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                var pwd = this.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;
                pwd?.Clear(); pwd?.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool IsValidEmail(string email)
        {
            try { return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase); }
            catch { return false; }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (var registerForm = new RegisterForm())
            {
                var result = registerForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    userController.LoadUsers();
                    if (!string.IsNullOrWhiteSpace(registerForm?.Tag as string))
                    {
                        var txtEmail = this.Controls.Find("txtEmail", true).FirstOrDefault() as TextBox;
                        txtEmail!.Text = registerForm.Tag.ToString();
                        var pwd = this.Controls.Find("txtPassword", true).FirstOrDefault() as TextBox;
                        pwd?.Focus();
                    }
                }
            }
        }
    }
}
