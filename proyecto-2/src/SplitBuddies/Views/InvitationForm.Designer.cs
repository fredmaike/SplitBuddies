namespace SplitBuddies.Views
{
    /// <summary>
    /// Contiene la definición visual del formulario para enviar invitaciones.
    /// </summary>
    partial class InvitationForm
    {
        /// <summary>
        /// Contenedor para los componentes del formulario.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles del formulario
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSend;

        /// <summary>
        /// Libera los recursos utilizados por el formulario.
        /// </summary>
        /// <param name="disposing">true si se deben liberar recursos administrados; false de lo contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Inicializa y configura los componentes visuales del formulario.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // -----------------------------
            // Label para correo electrónico
            // -----------------------------
            this.lblEmail.Location = new System.Drawing.Point(20, 20);
            this.lblEmail.Size = new System.Drawing.Size(80, 20);
            this.lblEmail.Text = "Email:";

            // -----------------------------
            // Caja de texto para email
            // -----------------------------
            this.txtEmail.Location = new System.Drawing.Point(20, 45);
            this.txtEmail.Size = new System.Drawing.Size(200, 23);

            // -----------------------------
            // Botón Enviar
            // -----------------------------
            this.btnSend.Location = new System.Drawing.Point(20, 80);
            this.btnSend.Size = new System.Drawing.Size(100, 30);
            this.btnSend.Text = "Enviar";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // -----------------------------
            // Configuración general del formulario
            // -----------------------------
            this.ClientSize = new System.Drawing.Size(250, 130);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnSend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
