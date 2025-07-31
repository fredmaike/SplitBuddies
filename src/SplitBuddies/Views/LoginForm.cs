using SplitBuddies.Controllers;
using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario para que los usuarios puedan iniciar sesión o registrarse.
    /// </summary>
    public partial class LoginForm : Form
    {
        // Controlador que maneja la lógica relacionada con usuarios (carga, login).
        private UserController userController = new UserController();

        // Usuario que ha iniciado sesión correctamente, disponible para otras partes de la app.
        public User LoggedInUser { get; private set; }

        /// <summary>
        /// Constructor del formulario de login.
        /// Inicializa los controles, define la ruta para los datos y carga la lista de usuarios.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();

            // Establece la ruta base para la carga/guardado de datos (archivos JSON).
            DataManager.Instance.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            // Carga los usuarios almacenados en la base de datos para tenerlos disponibles.
            userController.LoadUsers();
        }

        /// <summary>
        /// Manejador del evento click para el botón "Iniciar sesión".
        /// Obtiene las credenciales, intenta autenticar y muestra resultados.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Recopila el email y contraseña ingresados por el usuario, eliminando espacios en blanco.
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Solicita al controlador que intente autenticar con las credenciales dadas.
                LoggedInUser = userController.Login(email, password);

                // Si la autenticación es exitosa, muestra mensaje de bienvenida.
                MessageBox.Show($"Bienvenido {LoggedInUser.Name}!", "Login exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Indica que el diálogo fue exitoso para que el formulario padre sepa el resultado.
                DialogResult = DialogResult.OK;

                // Cierra el formulario de login para continuar con la aplicación.
                Close();
            }
            catch (Exception ex)
            {
                // Si la autenticación falla o hay un error, muestra un mensaje descriptivo al usuario.
                MessageBox.Show(ex.Message, "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Manejador del evento click para el botón "Registrar".
        /// Abre el formulario para registro de nuevo usuario y recarga usuarios si hubo registro exitoso.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Instancia y muestra el formulario modal para registrar un nuevo usuario.
            var registerForm = new RegisterForm();
            var result = registerForm.ShowDialog();

            // Si el usuario se registró correctamente (resultado OK), recarga la lista de usuarios.
            if (result == DialogResult.OK)
            {
                userController.LoadUsers();
            }
        }
    }
}
