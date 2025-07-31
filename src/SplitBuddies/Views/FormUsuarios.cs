using SplitBuddies.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario para agregar manualmente un nuevo usuario al sistema.
    /// </summary>
    public partial class FormAgregarUsuario : Form
    {
        // Usuario que se agregará mediante este formulario.
        public User UsuarioAgregado { get; private set; }

        /// <summary>
        /// Constructor que inicializa el formulario.
        /// </summary>
        public FormAgregarUsuario()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que se dispara cuando el usuario hace clic en el botón "Guardar".
        /// Realiza validaciones, guarda el usuario y cierra el formulario.
        /// </summary>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Se recogen los datos ingresados por el usuario.
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validación: todos los campos deben estar completos.
            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor complete todos los campos, incluida la contraseña.");
                return;
            }

            // Carga la lista actual de usuarios desde el archivo.
            var usuarios = SplitBuddies.Data.DataStorage.LoadUsers();

            // Verifica que el correo ingresado no esté registrado.
            if (usuarios.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("El correo ya está registrado.");
                return;
            }

            // Se crea un nuevo objeto User con los datos ingresados.
            UsuarioAgregado = new User
            {
                Name = nombre,
                Email = email,
                Password = password,
            };

            // Se añade el nuevo usuario a la lista.
            usuarios.Add(UsuarioAgregado);

            try
            {
                // Guarda la lista de usuarios actualizada en el archivo.
                SplitBuddies.Data.DataStorage.SaveUsers(usuarios);

                // Notifica al usuario que el guardado fue exitoso.
                MessageBox.Show("Usuario agregado correctamente.");

                // Devuelve el resultado al formulario llamador y lo cierra.
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                // Si ocurre un error al guardar, se muestra un mensaje de error.
                MessageBox.Show($"Error guardando usuario: {ex.Message}");
            }
        }
    }
}
