using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario para el registro de nuevos usuarios en la aplicación SplitBuddies.
    /// Permite validar campos, verificar duplicados y almacenar usuarios en el DataManager.
    /// </summary>
    public partial class RegisterForm : Form
    {
        /// <summary>
        /// Usuario que ha sido registrado exitosamente.
        /// </summary>
        public User RegisteredUser { get; private set; }

        /// <summary>
        /// Constructor del formulario de registro.
        /// Inicializa componentes y asegura que la carpeta de datos exista.
        /// </summary>
        public RegisterForm()
        {
            InitializeComponent();
            EnsureDataDirectory();      // Asegura que exista la carpeta Data
            DataManager.Instance.LoadUsers();  // Carga usuarios existentes
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Registrar".
        /// Valida los campos, verifica si el correo ya existe, agrega al nuevo usuario y guarda los datos.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Obtener valores ingresados por el usuario
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Validar campos obligatorios
            if (!ValidateFields(name, email, password))
                return;

            // Refrescar lista de usuarios para asegurar datos actualizados
            DataManager.Instance.LoadUsers();

            // Verificar si el correo ya está registrado
            if (UserExists(email))
            {
                MessageBox.Show(
                    "Este correo ya está registrado.",
                    "Registro inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Crear el nuevo usuario y agregarlo a la lista
            RegisteredUser = new User
            {
                Name = name,
                Email = email,
                Password = password
            };
            DataManager.Instance.Users.Add(RegisteredUser);

            // Asegurar la carpeta de datos antes de guardar
            EnsureDataDirectory();
            DataManager.Instance.SaveUsers();

            // Informar al usuario del registro exitoso
            MessageBox.Show(
                "Usuario registrado correctamente.",
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            DialogResult = DialogResult.OK;
            Close();
        }

        #region Helpers

        /// <summary>
        /// Valida que los campos obligatorios no estén vacíos.
        /// </summary>
        /// <param name="name">Nombre ingresado</param>
        /// <param name="email">Correo electrónico ingresado</param>
        /// <param name="password">Contraseña ingresada</param>
        /// <returns>True si todos los campos son válidos, false si falta alguno</returns>
        private static bool ValidateFields(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Ingrese su nombre.", "Campo obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Ingrese su correo electrónico.", "Campo obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Ingrese su contraseña.", "Campo obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Verifica si ya existe un usuario con el correo especificado.
        /// </summary>
        /// <param name="email">Correo electrónico a verificar</param>
        /// <returns>True si el usuario existe, false si no</returns>
        private static bool UserExists(string email)
        {
            return DataManager.Instance.Users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Asegura que la carpeta de datos exista y asigna la ruta a DataManager.
        /// </summary>
        private static void EnsureDataDirectory()
        {
            var path = DataManager.Instance.BasePath;

            if (string.IsNullOrWhiteSpace(path))
            {
                DataManager.Instance.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
                path = DataManager.Instance.BasePath;
            }

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        #endregion
    }
}
