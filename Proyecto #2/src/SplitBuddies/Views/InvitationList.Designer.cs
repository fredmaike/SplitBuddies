using System.Windows.Forms;
using System.Drawing;

namespace SplitBuddies.Views
{
    partial class InvitationList
    {
        private DataGridView dgvInvitations;
        private Button btnAccept;
        private Button btnReject;

        private void InitializeComponent()
        {
            dgvInvitations = new DataGridView();
            btnAccept = new Button();
            btnReject = new Button();

            SuspendLayout();

            // DataGridView
            dgvInvitations.Location = new Point(20, 20);
            dgvInvitations.Size = new Size(500, 200);
            dgvInvitations.AutoGenerateColumns = false; // Usamos columnas manuales

            // Columnas
            var colInvitationId = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InvitationId",
                Name = "InvitationId",
                Visible = false // Oculta esta columna
            };
            var colGroupId = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GroupId",
                Name = "GroupId",
                Visible = false // Oculta esta columna
            };
            var colGroupName = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "GroupName",
                Name = "Grupo",
                Width = 200
            };
            var colInviterEmail = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "InviterEmail",
                Name = "Quien Invita",
                Width = 200
            };
            var colStatus = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                Name = "Estado",
                Width = 80
            };

            dgvInvitations.Columns.AddRange(new DataGridViewColumn[] {
                colInvitationId, colGroupId, colGroupName, colInviterEmail, colStatus
            });

            dgvInvitations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInvitations.MultiSelect = false;

            // Botón Aceptar
            btnAccept.Location = new Point(20, 230);
            btnAccept.Size = new Size(100, 30);
            btnAccept.Text = "Aceptar";
            btnAccept.Click += btnAccept_Click;

            // Botón Rechazar
            btnReject.Location = new Point(140, 230);
            btnReject.Size = new Size(100, 30);
            btnReject.Text = "Rechazar";
            btnReject.Click += btnReject_Click;

            // Formulario
            ClientSize = new Size(550, 280);
            Controls.Add(dgvInvitations);
            Controls.Add(btnAccept);
            Controls.Add(btnReject);
            Text = "Invitaciones Pendientes";

            ResumeLayout(false);
        }
    }
}
