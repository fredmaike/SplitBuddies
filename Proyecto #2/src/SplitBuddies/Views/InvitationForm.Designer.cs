namespace SplitBuddies.Views
{
    partial class InvitationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSend;

        private void InitializeComponent()
        {
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // lblEmail
            this.lblEmail.Location = new System.Drawing.Point(20, 20);
            this.lblEmail.Size = new System.Drawing.Size(80, 20);
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(20, 45);
            this.txtEmail.Size = new System.Drawing.Size(200, 23);

            // btnSend
            this.btnSend.Location = new System.Drawing.Point(20, 80);
            this.btnSend.Size = new System.Drawing.Size(100, 30);
            this.btnSend.Text = "Enviar";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // InvitationForm
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
