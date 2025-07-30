using SplitBuddies.Models;
using SplitBuddies.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class RegisterForm : Form
    {
        public User RegisteredUser { get; private set; }

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            var usuarios = DataStorage.LoadUsers();

            if (usuarios.Exists(u => u.Email == email))
            {
                MessageBox.Show("Este correo ya está registrado.");
                return;
            }

            RegisteredUser = new User
            {
                Name = name,
                Email = email,
                Password = password
            };

            usuarios.Add(RegisteredUser);
            DataStorage.SaveUsers(usuarios);

            MessageBox.Show("Usuario registrado correctamente.");
            Close();
        }
    }
}
