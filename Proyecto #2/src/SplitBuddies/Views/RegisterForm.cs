using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class RegisterForm : Form
    {
        // Propiedad para almacenar el usuario registrado
        public User RegisteredUser { get; }

        public RegisterForm()
        {
            InitializeComponent();

            // Establece la ruta base de los datos si aún no está configurada
            if (string.IsNullOrWhiteSpace(DataManager.Instance.BasePath))
            {
                DataManager.Instance.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            }

            // Carga la lista de usuarios existentes al iniciar el formulario
            DataManager.Instance.LoadUsers();
        }

        // Evento click del botón Registrar
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Obtener valores de los campos de texto y quitar espacios en blanco
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validar que ningún campo esté vacío
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            // Volver a cargar los usuarios para tener la lista actualizada
            DataManager.Instance.LoadUsers();

            // Validar que el email no exista ya registrado 
            if (DataManager.Instance.Users.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Este correo ya está registrado.");
                return;
            }

            // Crear el nuevo usuario y añadirlo a la lista
            DataManager.Instance.Users.Add(new User
            {
                Name = name,
                Email = email,
                Password = password
            });

            // Crear la carpeta de datos si no existe
            if (!Directory.Exists(DataManager.Instance.BasePath))
            {
                Directory.CreateDirectory(DataManager.Instance.BasePath);
            }

            // Guardar la lista de usuarios en el archivo JSON
            DataManager.Instance.SaveUsers("usuarios.json");

            // Confirmar al usuario que se registró correctamente
            MessageBox.Show("Usuario registrado correctamente.");

            // Cerrar el formulario
            Close();
        }
    }
}
