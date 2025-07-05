using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

       

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Por favor, ingrese su correo.");
                return;
            }

            User user = UserController.Login(email);

            if (user != null)
            {
                MessageBox.Show($"Bienvenido {user.Name}");
                this.Hide();
                DashboardForm dashboard = new DashboardForm(user);
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Usuario no encontrado.");
            }
        }
    }
}
