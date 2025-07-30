using System;
using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class FormAgregarUser
    {
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblTipo;
        private ComboBox cmbTipo;
        private Button btnGuardar;
        private Button btnCancelar;

        private void InitializeComponent()
        {
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblNombre
            // 
            lblNombre.Location = new Point(49, 142);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(100, 23);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(160, 140);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(150, 23);
            txtNombre.TabIndex = 1;
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(49, 180);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(160, 180);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(150, 23);
            txtEmail.TabIndex = 3;
            // 
            // lblTipo
            // 
            lblTipo.Location = new Point(49, 220);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(100, 23);
            lblTipo.TabIndex = 4;
            lblTipo.Text = "Tipo";
            // 
            // cmbTipo
            // 
            cmbTipo.Items.AddRange(new object[] { "Normal", "Premium" });
            cmbTipo.Location = new Point(160, 220);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(121, 23);
            cmbTipo.TabIndex = 5;
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.SelectedIndex = 0;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(80, 270);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.TabIndex = 6;
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(200, 270);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.TabIndex = 7;
            btnCancelar.Text = "Cancelar";
            // 
            // FormAgregarUser
            // 
            ClientSize = new Size(400, 350);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblTipo);
            Controls.Add(cmbTipo);
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
