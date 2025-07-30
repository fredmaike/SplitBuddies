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

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            btnGroups = new Button();
            btnExpenses = new Button();
            btnSave = new Button();
            btnMostrar = new Button();
            btnLogout = new Button();
            btnEditGroups = new Button();

            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.Location = new Point(20, 20);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(200, 23);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Bienvenido";
            // 
            // btnGroups
            // 
            btnGroups.Location = new Point(20, 60);
            btnGroups.Name = "btnGroups";
            btnGroups.Size = new Size(100, 30);
            btnGroups.TabIndex = 1;
            btnGroups.Text = "Grupos";
            btnGroups.Click += btnGroups_Click;
            // 
            // btnExpenses
            // 
            btnExpenses.Location = new Point(140, 60);
            btnExpenses.Name = "btnExpenses";
            btnExpenses.Size = new Size(100, 30);
            btnExpenses.TabIndex = 2;
            btnExpenses.Text = "Gastos";
            btnExpenses.Click += btnExpenses_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(260, 60);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 30);
            btnSave.TabIndex = 3;
            btnSave.Text = "Guardar";
            btnSave.Click += btnSave_Click;
            // 
            // btnMostrar
            // 
            btnMostrar.Location = new Point(140, 100);
            btnMostrar.Name = "btnMostrar";
            btnMostrar.Size = new Size(220, 30);
            btnMostrar.TabIndex = 4;
            btnMostrar.Text = "Mostrar Grupos y Gastos";
            btnMostrar.Click += btnMostrar_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(20, 100);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(100, 30);
            btnLogout.TabIndex = 5;
            btnLogout.Text = "Cerrar Sesión";
            btnLogout.Click += btnLogout_Click;

            // 
            // btnEditGroups
            // 
            btnEditGroups.Location = new Point(20, 140);
            btnEditGroups.Name = "btnEditGroups";
            btnEditGroups.Size = new Size(360, 30);
            btnEditGroups.TabIndex = 6;
            btnEditGroups.Text = "Modificar Grupos";
            btnEditGroups.Click += btnEditGroups_Click;

            // 
            // MainForm
            // 
            ClientSize = new Size(400, 190); 
            Controls.Add(lblWelcome);
            Controls.Add(btnGroups);
            Controls.Add(btnExpenses);
            Controls.Add(btnSave);
            Controls.Add(btnMostrar);
            Controls.Add(btnLogout);
            Controls.Add(btnEditGroups); 
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "SplitBuddies - Main";
            ResumeLayout(false);
        }
    }
}

