
namespace SplitBuddies.Views
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRegister = new Button();
            txtEmail = new TextBox();
            txtName = new TextBox();
            Nombre = new Label();
            Correo = new Label();
            SuspendLayout();
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(208, 322);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(125, 29);
            btnRegister.TabIndex = 0;
            btnRegister.Text = "Agregar";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click_1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(170, 248);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(191, 27);
            txtEmail.TabIndex = 1;
            // 
            // txtName
            // 
            txtName.Location = new Point(170, 146);
            txtName.Name = "txtName";
            txtName.Size = new Size(191, 27);
            txtName.TabIndex = 2;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.Location = new Point(231, 97);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(64, 20);
            Nombre.TabIndex = 3;
            Nombre.Text = "Nombre";
            Nombre.Click += label1_Click;
            // 
            // Correo
            // 
            Correo.AutoSize = true;
            Correo.Location = new Point(241, 213);
            Correo.Name = "Correo";
            Correo.Size = new Size(54, 20);
            Correo.TabIndex = 4;
            Correo.Text = "Correo";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(519, 450);
            Controls.Add(Correo);
            Controls.Add(Nombre);
            Controls.Add(txtName);
            Controls.Add(txtEmail);
            Controls.Add(btnRegister);
            Name = "RegisterForm";
            Text = "RegisterForm";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }



        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Implementación inicial vacía para evitar el error CS0103.  
        }
        #endregion

        private Button btnRegister;
        private TextBox txtEmail;
        private TextBox txtName;
        private Label Nombre;
        private Label Correo;
    }
}