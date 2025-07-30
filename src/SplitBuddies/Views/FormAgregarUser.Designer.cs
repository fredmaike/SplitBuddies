using System;
using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario para registrar un nuevo usuario.
    /// Contiene campos de entrada para nombre, correo electrónico, tipo de cuenta y contraseña.
    /// </summary>
    partial class FormAgregarUser
    {
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Label lblPassword;       // Nueva etiqueta para contraseña
        private TextBox txtPassword;     // Nuevo TextBox para contraseña
        private Button btnGuardar;
        private Button btnCancelar;

        /// <summary>
        /// Inicializa todos los controles del formulario.
        /// Este método es generado por el diseñador, pero puede personalizarse.
        /// </summary>
        private void InitializeComponent()
        {
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();

            SuspendLayout();

            // 
            // lblNombre
            // Etiqueta para el campo "Nombre"
            lblNombre.Location = new Point(49, 142);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(100, 23);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre";

            // 
            // txtNombre
            // Caja de texto para ingresar el nombre del usuario
            txtNombre.Location = new Point(160, 140);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(150, 23);
            txtNombre.TabIndex = 1;

            // 
            // lblEmail
            // Etiqueta para el campo "Email"
            lblEmail.Location = new Point(49, 180);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";

            // 
            // txtEmail
            // Caja de texto para ingresar el correo electrónico del usuario
            txtEmail.Location = new Point(160, 180);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(150, 23);
            txtEmail.TabIndex = 3;

            // 
            // lblTipo
            // Etiqueta para el campo "Tipo"
            lblTipo.Location = new Point(49, 220);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(100, 23);
            lblTipo.TabIndex = 4;
            lblTipo.Text = "Tipo";

            // 
            // cmbTipo
            // ComboBox con los tipos de usuario disponibles
            cmbTipo.Items.AddRange(new object[] { "Normal", "Premium" });
            cmbTipo.Location = new Point(160, 220);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(121, 23);
            cmbTipo.TabIndex = 5;
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.SelectedIndex = 0;

            // 
            // lblPassword
            // Etiqueta para el campo "Contraseña"
            lblPassword.Location = new Point(49, 260);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Contraseña";

            // 
            // txtPassword
            // Caja de texto para ingresar la contraseña, oculta el texto
            txtPassword.Location = new Point(160, 260);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(150, 23);
            txtPassword.TabIndex = 7;
            txtPassword.UseSystemPasswordChar = true;

            // 
            // btnGuardar
            // Botón para guardar el nuevo usuario
            btnGuardar.Location = new Point(80, 300);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;

            // 
            // btnCancelar
            // Botón para cancelar la operación y cerrar el formulario
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(200, 300);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";

            // 
            // FormAgregarUser
            // Propiedades del formulario
            ClientSize = new Size(400, 350);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblTipo);
            Controls.Add(cmbTipo);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Name = "FormAgregarUser";
            Text = "Registrar Usuario";
            Load += FormAgregarUser_Load;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
