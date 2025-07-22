using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SplitBuddies.Models;
using SplitBuddies.Data;

namespace SplitBuddies.Views
{
    public class RegisterForm : Form
    {
        private TextBox txtName;
        private TextBox txtEmail;
        private Button btnRegister;

        public RegisterForm()
        {
            this.Text = "Registrar Usuario";
            this.Size = new Size(300, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblName = new Label();
            lblName.Text = "Nombre:";
            lblName.Location = new Point(10, 20);
            this.Controls.Add(lblName);

            txtName = new TextBox();
            txtName.Location = new Point(10, 45);
            txtName.Width = 250;
            this.Controls.Add(txtName);

            Label lblEmail = new Label();
            lblEmail.Text = "Correo:";
            lblEmail.Location = new Point(10, 80);
            this.Controls.Add(lblEmail);

            txtEmail = new TextBox();
            txtEmail.Location = new Point(10, 105);
            txtEmail.Width = 250;
            this.Controls.Add(txtEmail);

            btnRegister = new Button();
            btnRegister.Text = "Registrar";
            btnRegister.Location = new Point(10, 150);
            btnRegister.Click += BtnRegister_Click;
            this.Controls.Add(btnRegister);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

            List<User> usuarios = DataLoader.LoadUsers();
            usuarios.Add(new User { Name = name, Email = email });
            DataLoader.SaveUsers(usuarios);

            MessageBox.Show($"Usuario '{name}' agregado correctamente.");
            this.Close();
        }
    }
}
