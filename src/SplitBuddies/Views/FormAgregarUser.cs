using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SplitBuddies.Models;
using SplitBuddies.Services;
using SplitBuddies.Utils;

namespace SplitBuddies.Views
{
    public partial class FormAgregarUser : Form
    {
        private List<User> usuarios;

        public FormAgregarUser()
        {
            InitializeComponent();
            this.Load += FormAgregarUser_Load;
        }

        private void FormAgregarUser_Load(object sender, EventArgs e)
        {
            // Cargar usuarios existentes al iniciar
            usuarios = JsonDataService.LoadUsers();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener valores de los campos
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validar campos obligatorios
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor completa todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verificar que el email no esté registrado
            bool existe = usuarios.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (existe)
            {
                MessageBox.Show("El email ya está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear usuario sin tipo (la fábrica sin parámetro tipo)
            var nuevoUsuario = UsuarioFactory.CrearUsuario(nombre, email, password);

            usuarios.Add(nuevoUsuario);

            // Guardar la lista actualizada
            JsonDataService.SaveUsers(usuarios);

            MessageBox.Show("Usuario creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }
    }
}
