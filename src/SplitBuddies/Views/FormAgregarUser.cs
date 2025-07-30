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
            cmbTipo.SelectedIndex = 0;
            usuarios = JsonDataService.LoadUsers();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            string tipo = cmbTipo.SelectedItem.ToString();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor completa todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool existe = usuarios.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (existe)
            {
                MessageBox.Show("El email ya está registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var nuevoUsuario = UsuarioFactory.CrearUsuario(tipo, nombre, email, password);

            usuarios.Add(nuevoUsuario);

            JsonDataService.SaveUsers(usuarios);

            MessageBox.Show("Usuario creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}