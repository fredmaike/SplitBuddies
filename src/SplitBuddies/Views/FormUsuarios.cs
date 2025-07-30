using SplitBuddies.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class FormAgregarUsuario : Form
    {
        public User UsuarioAgregado { get; private set; }

        public FormAgregarUsuario()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();  
            string tipo = cmbTipo.SelectedItem?.ToString() ?? "Regular";

            // Validar que no falte ningún campo
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor complete todos los campos, incluida la contraseña.");
                return;
            }

            var usuarios = SplitBuddies.Data.DataStorage.LoadUsers();

            // Validar que el correo no exista ya
            if (usuarios.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("El correo ya está registrado.");
                return;
            }

            UsuarioAgregado = new User
            {
                Name = nombre,
                Email = email,
                Password = password,  // Guardamos la contraseña ingresada
                Type = tipo
            };

            usuarios.Add(UsuarioAgregado);

            try
            {
                SplitBuddies.Data.DataStorage.SaveUsers(usuarios);
                MessageBox.Show("Usuario agregado correctamente.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error guardando usuario: {ex.Message}");
            }
        }
    }
}
