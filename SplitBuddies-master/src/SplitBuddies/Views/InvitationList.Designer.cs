using System.Windows.Forms;
using System.Drawing;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Contiene la definición visual del formulario para listar invitaciones de grupos.
    /// Incluye un DataGridView con las invitaciones y botones para aceptarlas o rechazarlas.
    /// </summary>
    partial class InvitationList
    {
        /// <summary>
        /// DataGridView para mostrar la lista de invitaciones.
        /// </summary>
        private DataGridView dgvInvitations;

        /// <summary>
        /// Botón para aceptar la invitación seleccionada.
        /// </summary>
        private Button btnAcceptInvitation;

        /// <summary>
        /// Botón para rechazar la invitación seleccionada.
        /// </summary>
        private Button btnRejectInvitation;

        /// <summary>
        /// Inicializa los componentes visuales del formulario.
        /// </summary>
        private void InitializeComponent()
        {
            dgvInvitations = new DataGridView();
            btnAcceptInvitation = new Button();
            btnRejectInvitation = new Button();

            SuspendLayout();

            InitializeDataGridView();
            InitializeButtons();
            InitializeForm();

            ResumeLayout(false);
        }

        #region Inicialización de Controles

        /// <summary>
        /// Configura el DataGridView y define las columnas visibles y ocultas.
        /// </summary>
        private void InitializeDataGridView()
        {
            dgvInvitations.Location = new Point(20, 20);
            dgvInvitations.Size = new Size(500, 200);
            dgvInvitations.AutoGenerateColumns = false;
            dgvInvitations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInvitations.MultiSelect = false;

            dgvInvitations.Columns.AddRange(
                new DataGridViewColumn[]
                {
                    new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "InvitationId",
                        Name = "InvitationId",
                        Visible = false
                    },
                    new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "GroupId",
                        Name = "GroupId",
                        Visible = false
                    },
                    new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "GroupName",
                        Name = "Grupo",
                        Width = 200
                    },
                    new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "InviterEmail",
                        Name = "QuienInvita",
                        Width = 200
                    },
                    new DataGridViewTextBoxColumn
                    {
                        DataPropertyName = "Status",
                        Name = "Estado",
                        Width = 80
                    }
                }
            );
        }

        /// <summary>
        /// Configura los botones Aceptar y Rechazar, incluyendo ubicación, tamaño y eventos.
        /// </summary>
        private void InitializeButtons()
        {
            // Botón Aceptar invitación
            btnAcceptInvitation.Location = new Point(20, 230);
            btnAcceptInvitation.Size = new Size(100, 30);
            btnAcceptInvitation.Text = "Aceptar";
            btnAcceptInvitation.Click += btnAccept_Click;

            // Botón Rechazar invitación
            btnRejectInvitation.Location = new Point(140, 230);
            btnRejectInvitation.Size = new Size(100, 30);
            btnRejectInvitation.Text = "Rechazar";
            btnRejectInvitation.Click += btnReject_Click;
        }

        /// <summary>
        /// Configura las propiedades generales del formulario y agrega los controles.
        /// </summary>
        private void InitializeForm()
        {
            ClientSize = new Size(550, 280);
            Controls.Add(dgvInvitations);
            Controls.Add(btnAcceptInvitation);
            Controls.Add(btnRejectInvitation);
            Text = "Invitaciones Pendientes";
        }

        #endregion
    }
}
