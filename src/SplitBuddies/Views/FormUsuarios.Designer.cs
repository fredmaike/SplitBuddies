using System;
using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class FormAgregarUsuario : Form
    {
        private TextBox txtNombre;
        private TextBox txtEmail;
        private ComboBox cmbTipo;
        private Button btnGuardar;
        private Button btnCancelar;

        private void InitializeComponent()
        {
            this.txtNombre = new TextBox();
            this.txtEmail = new TextBox();
            this.cmbTipo = new ComboBox();
            this.btnGuardar = new Button();
            this.btnCancelar = new Button();

            Label lblNombre = new Label() { Text = "Nombre:", Location = new Point(20, 25) };
            this.txtNombre.Location = new Point(100, 22);
            this.txtNombre.Width = 180;

            Label lblEmail = new Label() { Text = "Email:", Location = new Point(20, 65) };
            this.txtEmail.Location = new Point(100, 62);
            this.txtEmail.Width = 180;

            Label lblTipo = new Label() { Text = "Tipo:", Location = new Point(20, 105) };
            this.cmbTipo.Location = new Point(100, 102);
            this.cmbTipo.Width = 180;
            this.cmbTipo.Items.AddRange(new string[] { "Regular", "Premium" });
            this.cmbTipo.SelectedIndex = 0;

            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Location = new Point(50, 150);
            this.btnGuardar.Click += new EventHandler(this.btnGuardar_Click);

            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new Point(160, 150);
            this.btnCancelar.DialogResult = DialogResult.Cancel;

            this.ClientSize = new Size(320, 200);
            this.Controls.Add(lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(lblTipo);
            this.Controls.Add(this.cmbTipo);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Text = "Registrar Usuario";
        }
    }
}