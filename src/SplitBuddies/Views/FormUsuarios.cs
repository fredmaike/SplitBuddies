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
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            var usuarios = SplitBuddies.Data.DataStorage.LoadUsers();

            UsuarioAgregado = new User
            {
                Name = txtNombre.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Password = "default",
                Type = cmbTipo.SelectedItem?.ToString() ?? "Regular"
            };

            usuarios.Add(UsuarioAgregado);
            SplitBuddies.Data.DataStorage.SaveUsers(usuarios);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
