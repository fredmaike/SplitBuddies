using SplitBuddies.Models;


namespace SplitBuddies.Views
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            // Validación básica de email
            if (!email.Contains("@"))
            {
                MessageBox.Show("Correo inválido.");
                return;
            }

            // Crear objeto de usuario
            User newUser = new User
            {
                Name = name,
                Email = email
            };

            // Guardar usando el controlador
            bool success = UserController.Register(newUser);

            if (success)
            {
                MessageBox.Show("Usuario registrado con éxito.");
                this.Close(); // o navegar a otra vista
            }
            else
            {
                MessageBox.Show("El usuario ya existe.");
            }
        }
    }
}