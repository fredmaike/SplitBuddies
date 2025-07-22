using System;
using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Representa el formulario principal del panel de control de SplitBuddies.
    /// </summary>
    public partial class DashboardForm : Form
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Inicializa una nueva instancia del formulario DashboardForm.
        /// </summary>
        public DashboardForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Libera los recursos utilizados por el formulario.
        /// </summary>
        /// <param name="disposing">True si se deben liberar recursos administrados.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Método que inicializa y configura los componentes del formulario.
        /// </summary>
        private void InitializeComponent()
        {
            lblWelcomeMessage = new Label();
            SuspendLayout();
            // 
            // lblWelcomeMessage
            // 
            lblWelcomeMessage.AutoSize = true;
            lblWelcomeMessage.Location = new Point(290, 127);
            lblWelcomeMessage.Name = "lblWelcomeMessage";
            lblWelcomeMessage.Size = new Size(183, 15);
            lblWelcomeMessage.TabIndex = 0;
            lblWelcomeMessage.Text = "¡Haz clic para darte la bienvenida!";
            lblWelcomeMessage.Click += lblWelcomeMessage_Click;
            // 
            // DashboardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(958, 487);
            Controls.Add(lblWelcomeMessage);
            Margin = new Padding(3, 2, 3, 2);
            Name = "DashboardForm";
            Text = "Panel de Control - SplitBuddies";
            Load += DashboardForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        /// <summary>
        /// Evento que se ejecuta al hacer clic sobre el mensaje de bienvenida.
        /// </summary>
        private void lblWelcomeMessage_Click(object sender, EventArgs e)
        {
            MessageBox.Show("¡Bienvenido a SplitBuddies!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Evento que se ejecuta al cargar el formulario.
        /// </summary>
        private void DashboardForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Aquí puedes colocar cualquier lógica necesaria al cargar el formulario
                Console.WriteLine("Formulario Dashboard cargado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar el formulario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Label lblWelcomeMessage;
    }
}
