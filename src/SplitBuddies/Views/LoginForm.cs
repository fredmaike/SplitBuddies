using SplitBuddies.Controllers;
using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class LoginForm : Form
    {
        private UserController userController = new UserController();

        public User LoggedInUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();

            DataManager.Instance.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            userController.LoadUsers();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                LoggedInUser = userController.Login(email, password);

                MessageBox.Show($"Bienvenido {LoggedInUser.Name}!", "Login exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }
    }
}