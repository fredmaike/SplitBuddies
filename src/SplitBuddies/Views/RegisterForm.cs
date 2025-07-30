using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class RegisterForm : Form
    {
        public User RegisteredUser { get; }

        public RegisterForm()
        {
            InitializeComponent();

            // Solo se debe establecer la ruta base si aún no se ha establecido.
            if (string.IsNullOrWhiteSpace(DataManager.Instance.BasePath))
            {
                DataManager.Instance.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            }

            DataManager.Instance.LoadUsers(); // Cargar usuarios al abrir el formulario
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

            // Cargar usuarios actuales
            DataManager.Instance.LoadUsers();

            // 2. Validar que el email no exista
            if (DataManager.Instance.Users.Exists(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Este correo ya está registrado.");
                return;
            }

            // 3. Crear nuevo usuario y agregarlo a la lista
            DataManager.Instance.Users.Add(new User
            {
                Name = name,
                Email = email,
                Password = password
            });

            // 4. Crear carpeta si no existe (solo la primera vez)
            if (!Directory.Exists(DataManager.Instance.BasePath))
            {
                Directory.CreateDirectory(DataManager.Instance.BasePath);
            }

            // 5. Guardar usuarios en el archivo JSON
            DataManager.Instance.SaveUsers("usuarios.json");

            // 6. Confirmar al usuario que se guardó
            MessageBox.Show("Usuario registrado correctamente.");
            Close();
        }
    }
}
