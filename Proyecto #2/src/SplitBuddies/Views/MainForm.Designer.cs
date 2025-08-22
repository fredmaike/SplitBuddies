using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblWelcome;
        private Button btnGroups;
        private Button btnExpenses;
        private Button btnSave;
        private Button btnMostrar;
        private Button btnLogout;
        private Button btnEditGroups;
        private Button btnInvitations; 

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            btnGroups = new Button();
            btnExpenses = new Button();
            btnSave = new Button();
            btnMostrar = new Button();
            btnLogout = new Button();
            btnEditGroups = new Button();
            btnInvitations = new Button(); 

            SuspendLayout();

            // Label de bienvenida
            lblWelcome.Location = new Point(20, 20);
            lblWelcome.Size = new Size(200, 23);
            lblWelcome.Text = "Bienvenido";

            // Botones existentes
            btnGroups.Location = new Point(20, 60);
            btnGroups.Size = new Size(100, 30);
            btnGroups.Text = "Grupos";
            btnGroups.Click += btnGroups_Click;

            btnExpenses.Location = new Point(140, 60);
            btnExpenses.Size = new Size(100, 30);
            btnExpenses.Text = "Gastos";
            btnExpenses.Click += btnExpenses_Click;

            btnSave.Location = new Point(260, 60);
            btnSave.Size = new Size(100, 30);
            btnSave.Text = "Guardar";
            btnSave.Click += btnSave_Click;

            btnMostrar.Location = new Point(140, 100);
            btnMostrar.Size = new Size(220, 30);
            btnMostrar.Text = "Mostrar Grupos y Gastos";
            btnMostrar.Click += btnMostrar_Click;

            btnLogout.Location = new Point(20, 100);
            btnLogout.Size = new Size(100, 30);
            btnLogout.Text = "Cerrar Sesión";
            btnLogout.Click += btnLogout_Click;

            btnEditGroups.Location = new Point(20, 140);
            btnEditGroups.Size = new Size(360, 30);
            btnEditGroups.Text = "Modificar Grupos";
            btnEditGroups.Click += btnEditGroups_Click;

            // Nuevo botón para ver invitaciones pendientes
            btnInvitations.Location = new Point(20, 180);
            btnInvitations.Size = new Size(360, 30);
            btnInvitations.Text = "Ver Invitaciones Pendientes";
            btnInvitations.Click += BtnInvitations_Click; 

            // Configuración general del formulario
            ClientSize = new Size(400, 230); 
            Controls.Add(lblWelcome);
            Controls.Add(btnGroups);
            Controls.Add(btnExpenses);
            Controls.Add(btnSave);
            Controls.Add(btnMostrar);
            Controls.Add(btnLogout);
            Controls.Add(btnEditGroups);
            Controls.Add(btnInvitations); // Agregar al formulario

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "SplitBuddies - Main";

            ResumeLayout(false);
        }
    }
}
