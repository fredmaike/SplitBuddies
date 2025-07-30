using System;
using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class FormAgregarUsuario : Form
    {
        // Controles del formulario para ingresar datos de usuario
        private TextBox txtNombre;       // Campo para el nombre del usuario
        private TextBox txtEmail;        // Campo para el email del usuario
        private TextBox txtPassword;     // Campo para la contraseña (oculta texto)
        private ComboBox cmbTipo;        // Combo para seleccionar tipo de usuario (Regular, Premium)
        private Button btnGuardar;       // Botón para guardar el usuario
        private Button btnCancelar;      // Botón para cancelar la operación

        // Método que inicializa y configura los controles y layout del formulario
        private void InitializeComponent()
        {
            // Crear instancias de los controles
            this.txtNombre = new TextBox();
            this.txtEmail = new TextBox();
            this.txtPassword = new TextBox();
            this.cmbTipo = new ComboBox();
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();

            // Etiqueta para el campo Nombre
            Label lblNombre = new Label() { Text = "Nombre:", Location = new Point(20, 25) };
            this.txtNombre.Location = new Point(100, 22);  // Posición del textbox
            this.txtNombre.Width = 180;                      // Ancho del textbox

            // Etiqueta para el campo Email
            Label lblEmail = new Label() { Text = "Email:", Location = new Point(20, 65) };
            this.txtEmail.Location = new Point(100, 62);
            this.txtEmail.Width = 180;

            // Etiqueta para el campo Contraseña
            Label lblPassword = new Label() { Text = "Contraseña:", Location = new Point(20, 105) };
            this.txtPassword.Location = new Point(100, 102);
            this.txtPassword.Width = 180;
            this.txtPassword.UseSystemPasswordChar = true;  // Oculta el texto para proteger la contraseña

            // Etiqueta para el combo Tipo de usuario
            Label lblTipo = new Label() { Text = "Tipo:", Location = new Point(20, 145) };
            this.cmbTipo.Location = new Point(100, 142);
            this.cmbTipo.Width = 180;
            this.cmbTipo.Items.AddRange(new string[] { "Regular", "Premium" }); // Opciones disponibles
            this.cmbTipo.SelectedIndex = 0; // Selección por defecto en "Regular"

            // Botón Guardar para enviar la información
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Location = new Point(50, 190);
            this.btnGuardar.Click += new EventHandler(this.btnGuardar_Click); // Evento click para guardar

            // Botón Cancelar para cerrar sin guardar
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new Point(160, 190);
            this.btnCancelar.DialogResult = DialogResult.Cancel; // Cierra el formulario

            // Configuración general del formulario
            this.ClientSize = new Size(320, 240);

            // Añadir controles al formulario para que se muestren
            this.Controls.Add(lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(lblTipo);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);

            // Título de la ventana
            this.Text = "Registrar Usuario";
        }

      
    }
}
