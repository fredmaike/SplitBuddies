using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class RegisterForm : Form
    {
        // Propiedad para obtener el usuario registrado (aunque actualmente no se usa)
        public User RegisteredUser { get; }

        public RegisterForm()
        {
            InitializeComponent();

            // Establece la ruta base de los datos si aún no está definida
            if (string.IsNullOrWhiteSpace(DataManager.Instance.BasePath))
            {
                DataManager.Instance.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            }

            // Carga los usuarios existentes al abrir el formulario
            DataManager.Instance.LoadUsers();
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Registrar".
        /// Valida los campos, verifica si el correo ya existe, agrega al nuevo usuario y guarda los datos.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Obtener los valores ingresados por el usuario
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validar que todos los campos estén completos
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            // Recargar la lista de usuarios para asegurarse de tener los datos más recientes
            DataManager.Instance.LoadUsers();

            // Verificar si el correo ya está registrado
            if (DataManager.Instance.Users.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Este correo ya está registrado.");
                return;
            }

            // Agregar nuevo usuario a la lista
            DataManager.Instance.Users.Add(new User
            {
                Name = name,
                Email = email,
                Password = password
            });

            // Asegurar que el directorio de datos exista antes de guardar
            if (!Directory.Exists(DataManager.Instance.BasePath))
            {
                Directory.CreateDirectory(DataManager.Instance.BasePath);
            }

            // Guardar los usuarios en el archivo JSON
            DataManager.Instance.SaveUsers("usuarios.json");

            // Notificar al usuario que el registro fue exitoso
            MessageBox.Show("Usuario registrado correctamente.");

            // Establecer el resultado como OK para indicar éxito al formulario que lo llamó
            this.DialogResult = DialogResult.OK;

            // Cerrar el formulario
            Close();
        }
    }
}
