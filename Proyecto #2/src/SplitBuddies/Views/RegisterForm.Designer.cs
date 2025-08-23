using System;
using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        // ==========================
        // Controles del formulario
        // ==========================
        private Label lblTitle;
        private Label lblName;
        private TextBox txtName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Button btnRegister;
        private Button btnCancel;

        /// <summary>
        /// Libera los recursos utilizados por el formulario.
        /// </summary>
        /// <param name="disposing">Indica si se deben liberar recursos administrados.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Inicializa y configura todos los controles del formulario de registro.
        /// </summary>
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

            // ==========================
            // Variables para layout
            // ==========================
            const int labelWidth = 120;
            const int controlWidth = 180;
            const int labelX = 20;
            const int controlX = 140;
            int y = 25;
            const int spacing = 32;

            // ==========================
            // Controles del formulario
            // ==========================

            // Título del formulario
            lblTitle = CrearLabel(
                "Registro de usuario",
                new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold),
                new Size(310, 32),
                new Point(20, 10),
                ContentAlignment.MiddleCenter
            );

            // Nombre
            lblName = CrearLabel("Nombre:", labelWidth, new Point(labelX, y += 40));
            txtName = CrearTextBox(new Point(controlX, y), controlWidth);

            // Correo electrónico
            lblEmail = CrearLabel("Correo electrónico:", labelWidth, new Point(labelX, y += spacing));
            txtEmail = CrearTextBox(new Point(controlX, y), controlWidth);

            // Contraseña
            lblPassword = CrearLabel("Contraseña:", labelWidth, new Point(labelX, y += spacing));
            txtPassword = CrearTextBox(new Point(controlX, y), controlWidth, true);

            // Confirmar contraseña
            lblConfirmPassword = CrearLabel("Confirmar contraseña:", labelWidth, new Point(labelX, y += spacing));
            txtConfirmPassword = CrearTextBox(new Point(controlX, y), controlWidth, true);

            // Botón Registrar
            btnRegister = CrearBoton("Registrar", new Size(110, 32), new Point(70, y += spacing + 18), btnRegister_Click);

            // Botón Cancelar
            btnCancel = CrearBoton("Cancelar", new Size(110, 32), new Point(180, y));
            btnCancel.DialogResult = DialogResult.Cancel;

            // Agregar todos los controles al formulario
            Controls.AddRange(new Control[]
            {
                lblTitle,
                lblName, txtName,
                lblEmail, txtEmail,
                lblPassword, txtPassword,
                lblConfirmPassword, txtConfirmPassword,
                btnRegister, btnCancel
            });
        }

        #region Helpers

        /// <summary>
        /// Crea un Label simple con texto, ancho y ubicación.
        /// </summary>
        private static Label CrearLabel(string texto, int ancho, Point location)
        {
            return new Label
            {
                Text = texto,
                Size = new Size(ancho, 23),
                Location = location
            };
        }

        /// <summary>
        /// Crea un Label con fuente personalizada, tamaño, ubicación y alineación.
        /// </summary>
        private static Label CrearLabel(string texto, Font font, Size size, Point location, ContentAlignment align)
        {
            return new Label
            {
                Text = texto,
                Font = font,
                Size = size,
                Location = location,
                TextAlign = align
            };
        }

        /// <summary>
        /// Crea un TextBox en una ubicación específica, opcionalmente con carácter de contraseña.
        /// </summary>
        private static TextBox CrearTextBox(Point location, int ancho, bool passwordChar = false)
        {
            return new TextBox
            {
                Size = new Size(ancho, 23),
                Location = location,
                PasswordChar = passwordChar ? '●' : '\0'
            };
        }

        /// <summary>
        /// Crea un botón con texto, tamaño, ubicación y un manejador opcional para el evento Click.
        /// </summary>
        private static Button CrearBoton(string texto, Size size, Point location, EventHandler clickHandler = null)
        {
            var button = new Button
            {
                Text = texto,
                Size = size,
                Location = location
            };
            if (clickHandler != null)
                button.Click += clickHandler;

            return button;
        }

        #endregion

        #endregion
    }
}
