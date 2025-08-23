using System;
using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Contiene la definición visual del formulario de inicio de sesión.
    /// Se encarga únicamente de la disposición y configuración de los controles.
    /// </summary>
    partial class LoginForm
    {
        /// <summary>
        /// Contenedor de componentes del formulario.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // ===== Controles del formulario =====

        // Etiquetas
        private Label lblEmail;
        private Label lblPassword;

        // Cajas de texto
        private TextBox txtEmail;
        private TextBox txtPassword;

        // Botones
        private Button btnLogin;
        private Button btnRegister;

        /// <summary>
        /// Libera los recursos utilizados por el formulario.
        /// </summary>
        /// <param name="disposing">Indica si se deben liberar recursos administrados.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Inicializa y configura todos los controles del formulario.
        /// Divide la configuración en métodos especializados por tipo de control.
        /// </summary>
        private void InitializeComponent()
        {
            InitializeLabels();
            InitializeTextBoxes();
            InitializeButtons();
            InitializeForm();
        }

        #region Métodos de Inicialización de Controles

        /// <summary>
        /// Inicializa y configura las etiquetas del formulario.
        /// </summary>
        private void InitializeLabels()
        {
            lblEmail = new Label
            {
                AutoSize = true,
                Location = new Point(30, 30),
                Name = "lblEmail",
                Size = new Size(39, 15),
                Text = "Email:"
            };

            lblPassword = new Label
            {
                AutoSize = true,
                Location = new Point(30, 70),
                Name = "lblPassword",
                Size = new Size(70, 15),
                Text = "Contraseña:"
            };

            // Agregar etiquetas al formulario
            this.Controls.Add(lblEmail);
            this.Controls.Add(lblPassword);
        }

        /// <summary>
        /// Inicializa y configura las cajas de texto del formulario.
        /// </summary>
        private void InitializeTextBoxes()
        {
            txtEmail = new TextBox
            {
                Location = new Point(100, 27),
                Name = "txtEmail",
                Size = new Size(200, 23)
            };

            txtPassword = new TextBox
            {
                Location = new Point(100, 67),
                Name = "txtPassword",
                Size = new Size(200, 23),
                UseSystemPasswordChar = true // Oculta el texto para la contraseña
            };

            // Agregar cajas de texto al formulario
            this.Controls.Add(txtEmail);
            this.Controls.Add(txtPassword);
        }

        /// <summary>
        /// Inicializa y configura los botones del formulario.
        /// </summary>
        private void InitializeButtons()
        {
            btnLogin = new Button
            {
                Location = new Point(100, 110),
                Name = "btnLogin",
                Size = new Size(90, 30),
                Text = "Iniciar sesión",
                UseVisualStyleBackColor = true
            };
            btnLogin.Click += new EventHandler(this.btnLogin_Click);

            btnRegister = new Button
            {
                Location = new Point(210, 110),
                Name = "btnRegister",
                Size = new Size(90, 30),
                Text = "Registrar",
                UseVisualStyleBackColor = true
            };
            btnRegister.Click += new EventHandler(this.btnRegister_Click);

            // Agregar botones al formulario
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnRegister);
        }

        /// <summary>
        /// Configura las propiedades generales del formulario (tamaño, título y layout).
        /// </summary>
        private void InitializeForm()
        {
            this.ClientSize = new Size(350, 170);
            this.Name = "LoginForm";
            this.Text = "Inicio de sesión";

            // Finaliza la inicialización del layout
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
