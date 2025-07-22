using System;
using System.Drawing;
using System.Windows.Forms;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public class LoginForm : Form
    {
        private TextBox txtEmail;
        private Button btnLogin;

        public LoginForm()
        {
            this.Text = "Iniciar Sesión";
            this.Size = new Size(300, 200);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblEmail = new Label();
            lblEmail.Text = "Correo electrónico:";
            lblEmail.Location = new Point(10, 20);
            lblEmail.Size = new Size(120, 20);
            this.Controls.Add(lblEmail);

            txtEmail = new TextBox();
            txtEmail.Location = new Point(10, 45);
            txtEmail.Width = 250;
            this.Controls.Add(txtEmail);

            btnLogin = new Button();
            btnLogin.Text = "Iniciar sesión";
            btnLogin.Location = new Point(10, 80);
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Ingrese su correo.");
            }
            else
            {
                User usuario = new User { Name = email, Email = email };
                this.Hide();
                DashboardForm dashboard = new DashboardForm(usuario);
                dashboard.FormClosed += (s, args) => this.Show();
                dashboard.Show();
            }
        }
    }
}
