using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Parte del formulario principal MainForm (diseñador).
    /// Contiene la inicialización de los controles visuales del formulario.
    /// </summary>
    partial class MainForm
    {
        /// <summary>
        /// Contenedor de componentes utilizado para liberar recursos.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // -----------------------------
        // Controles principales del formulario
        // -----------------------------
        private Label lblWelcome;       // Label de bienvenida del usuario
        private Button btnGroups;       // Botón para gestionar grupos
        private Button btnExpenses;     // Botón para gestionar gastos
        private Button btnSave;         // Botón para guardar cambios
        private Button btnMostrar;      // Botón para mostrar grupos y gastos
        private Button btnLogout;       // Botón para cerrar sesión
        private Button btnEditGroups;   // Botón para modificar grupos y gastos
        private Button btnInvitations;  // Botón para ver invitaciones pendientes

        // Labels informativos del estado del usuario
        private Label lblStatus;        // Estado general del usuario
        private Label lblDebts;         // Deudas del usuario hacia otros
        private Label lblGroups;        // Grupos a los que pertenece el usuario

        /// <summary>
        /// Inicializa y configura todos los controles del formulario.
        /// </summary>
        private void InitializeComponent()
        {
            // Inicializar controles
            lblWelcome = new Label();
            btnGroups = new Button();
            btnExpenses = new Button();
            btnSave = new Button();
            btnMostrar = new Button();
            btnLogout = new Button();
            btnEditGroups = new Button();
            btnInvitations = new Button();

            lblStatus = new Label();
            lblDebts = new Label();
            lblGroups = new Label();

            SuspendLayout();

            // -----------------------------
            // Configuración del label de bienvenida
            // -----------------------------
            lblWelcome.Location = new Point(20, 20);
            lblWelcome.Size = new Size(200, 23);
            lblWelcome.Text = "Bienvenido";

            // -----------------------------
            // Configuración de botones principales
            // -----------------------------
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

            btnLogout.Location = new Point(20, 100);
            btnLogout.Size = new Size(100, 30);
            btnLogout.Text = "Cerrar Sesión";
            btnLogout.Click += btnLogout_Click;

            btnMostrar.Location = new Point(140, 100);
            btnMostrar.Size = new Size(220, 30);
            btnMostrar.Text = "Mostrar Grupos y Gastos";
            btnMostrar.Click += btnMostrar_Click;

            btnEditGroups.Location = new Point(20, 140);
            btnEditGroups.Size = new Size(360, 30);
            btnEditGroups.Text = "Modificar Grupos y Gastos";
            btnEditGroups.Click += btnEditGroups_Click;

            btnInvitations.Location = new Point(20, 180);
            btnInvitations.Size = new Size(360, 30);
            btnInvitations.Text = "Ver Invitaciones Pendientes";
            btnInvitations.Click += BtnInvitations_Click;

            // -----------------------------
            // Configuración de labels informativos del usuario
            // -----------------------------
            lblStatus.Location = new Point(20, 230);
            lblStatus.Size = new Size(360, 23);
            lblStatus.Text = "Estatus: ";

            lblDebts.Location = new Point(20, 260);
            lblDebts.Size = new Size(360, 23);
            lblDebts.Text = "Debe a: ";

            lblGroups.Location = new Point(20, 290);
            lblGroups.Size = new Size(360, 23);
            lblGroups.Text = "Grupo(s): ";

            // -----------------------------
            // Configuración general del formulario
            // -----------------------------
            ClientSize = new Size(400, 330);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "SplitBuddies - Main";

            // Añadir todos los controles al formulario
            Controls.Add(lblWelcome);
            Controls.Add(btnGroups);
            Controls.Add(btnExpenses);
            Controls.Add(btnSave);
            Controls.Add(btnMostrar);
            Controls.Add(btnLogout);
            Controls.Add(btnEditGroups);
            Controls.Add(btnInvitations);

            Controls.Add(lblStatus);
            Controls.Add(lblDebts);
            Controls.Add(lblGroups);

            ResumeLayout(false);
        }
    }
}
