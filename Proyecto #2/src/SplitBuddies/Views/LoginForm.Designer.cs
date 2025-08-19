namespace SplitBuddies.Views
{
    partial class LoginForm
    {
        // Contenedor para componentes del formulario, usado para liberar recursos
        private System.ComponentModel.IContainer components = null;

        // Etiqueta para el campo Email
        private System.Windows.Forms.Label lblEmail;
        // Caja de texto para que el usuario ingrese su email
        private System.Windows.Forms.TextBox txtEmail;

        // Etiqueta para el campo Contraseña
        private System.Windows.Forms.Label lblPassword;
        // Caja de texto para que el usuario ingrese su contraseña 
        private System.Windows.Forms.TextBox txtPassword;

        // Botón para iniciar sesión
        private System.Windows.Forms.Button btnLogin;
        // Botón para abrir el formulario de registro
        private System.Windows.Forms.Button btnRegister;

        // Método para liberar los recursos usados por el formulario
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Método que inicializa y configura los controles del formulario
        private void InitializeComponent()
        {
            // Crear controles
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // Configuración de la etiqueta Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(30, 30);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.Text = "Email:";

            // Configuración de la caja de texto Email
            this.txtEmail.Location = new System.Drawing.Point(100, 27);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 23);

            // Configuración de la etiqueta Contraseña
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(30, 70);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(70, 15);
            this.lblPassword.Text = "Contraseña:";

            // Configuración de la caja de texto Contraseña
            this.txtPassword.Location = new System.Drawing.Point(100, 67);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 23);
            this.txtPassword.UseSystemPasswordChar = true;  

            // Configuración del botón Iniciar sesión
            this.btnLogin.Location = new System.Drawing.Point(100, 110);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(90, 30);
            this.btnLogin.Text = "Iniciar sesión";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // Configuración del botón Registrar
            this.btnRegister.Location = new System.Drawing.Point(210, 110);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(90, 30);
            this.btnRegister.Text = "Registrar";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // Configuración general del formulario LoginForm
            this.ClientSize = new System.Drawing.Size(350, 170);
            // Añadir controles al formulario
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnRegister);

            this.Name = "LoginForm";
            this.Text = "Inicio de sesión";

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
