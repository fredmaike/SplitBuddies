using System;
using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class RegisterForm
    {
        // Contenedor para los componentes del formulario, para gestionar recursos
        private System.ComponentModel.IContainer components = null;

        // Etiqueta del título del formulario
        private Label lblTitle;
        // Etiqueta y caja de texto para ingresar el nombre
        private Label lblName;
        private TextBox txtName;
        // Etiqueta y caja de texto para ingresar el correo electrónico
        private Label lblEmail;
        private TextBox txtEmail;
        // Etiqueta y caja de texto para ingresar la contraseña
        private Label lblPassword;
        private TextBox txtPassword;
        // Etiqueta y caja de texto para confirmar la contraseña
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        // Botón para registrar el usuario
        private Button btnRegister;
        // Botón para cancelar el registro y cerrar el formulario
        private Button btnCancel;

        // Método para liberar los recursos que use el formulario
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose(); 
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // Configuración general del formulario
            this.Text = "Register";
            this.ClientSize = new Size(350, 340);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Variables para el layout de etiquetas y controles
            int labelWidth = 120;
            int controlWidth = 180;
            int labelX = 20;
            int controlX = 140;
            int y = 25;
            int spacing = 32;

            // Label título del formulario
            lblTitle = new Label();
            lblTitle.Text = "Registro de usuario";
            lblTitle.Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
            lblTitle.Size = new Size(310, 32);
            lblTitle.Location = new Point(20, 10);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Label y TextBox para Nombre
            lblName = new Label();
            lblName.Text = "Nombre:";
            lblName.Size = new Size(labelWidth, 23);
            lblName.Location = new Point(labelX, y += 40);

            txtName = new TextBox();
            txtName.Size = new Size(controlWidth, 23);
            txtName.Location = new Point(controlX, y);

            // Label y TextBox para Email
            lblEmail = new Label();
            lblEmail.Text = "Correo electrónico:";
            lblEmail.Size = new Size(labelWidth, 23);
            lblEmail.Location = new Point(labelX, y += spacing);

            txtEmail = new TextBox();
            txtEmail.Size = new Size(controlWidth, 23);
            txtEmail.Location = new Point(controlX, y);

            // Label y TextBox para Contraseña
            lblPassword = new Label();
            lblPassword.Text = "Contraseña:";
            lblPassword.Size = new Size(labelWidth, 23);
            lblPassword.Location = new Point(labelX, y += spacing);

            txtPassword = new TextBox();
            txtPassword.Size = new Size(controlWidth, 23);
            txtPassword.Location = new Point(controlX, y);
            txtPassword.PasswordChar = '●'; // Oculta la contraseña ingresada

            // Label y TextBox para Confirmar Contraseña
            lblConfirmPassword = new Label();
            lblConfirmPassword.Text = "Confirmar contraseña:";
            lblConfirmPassword.Size = new Size(labelWidth, 23);
            lblConfirmPassword.Location = new Point(labelX, y += spacing);

            txtConfirmPassword = new TextBox();
            txtConfirmPassword.Size = new Size(controlWidth, 23);
            txtConfirmPassword.Location = new Point(controlX, y);
            txtConfirmPassword.PasswordChar = '●'; // Oculta la contraseña ingresada

            // Botón para registrar el usuario
            btnRegister = new Button();
            btnRegister.Text = "Registrar";
            btnRegister.Size = new Size(110, 32);
            btnRegister.Location = new Point(70, y += spacing + 18);
            btnRegister.Click += btnRegister_Click; // Asigna el evento click

            // Botón para cancelar el registro
            btnCancel = new Button();
            btnCancel.Text = "Cancelar";
            btnCancel.Size = new Size(110, 32);
            btnCancel.Location = new Point(180, y);
            btnCancel.DialogResult = DialogResult.Cancel; // Cierra el formulario

            // Añade todos los controles al formulario
            Controls.Add(lblTitle);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(btnRegister);
            Controls.Add(btnCancel);
        }
    }
    #endregion
}
