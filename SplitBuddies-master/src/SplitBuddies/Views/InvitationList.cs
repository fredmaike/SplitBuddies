using System;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario para gestionar las invitaciones pendientes de un usuario.
    /// Permite aceptar o rechazar invitaciones sin modificar invitations.json,
    /// y guarda automáticamente los cambios en grupos.json al aceptar.
    /// </summary>
    public partial class InvitationList : Form
    {
        private readonly User currentUser;

        /// <summary>
        /// Inicializa el formulario de invitaciones para el usuario especificado.
        /// </summary>
        /// <param name="user">Usuario autenticado.</param>
        /// <exception cref="ArgumentNullException">Si el usuario es null.</exception>
        public InvitationList(User user)
        {
            InitializeComponent();
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            LoadPendingInvitations();
        }

        #region Carga de datos

        /// <summary>
        /// Carga las invitaciones pendientes del usuario actual en el DataGridView.
        /// </summary>
        private void LoadPendingInvitations()
        {
            try
            {
                var dm = DataManager.Instance;

                var pendingInvitations = dm.Invitations
                    .Where(i => i.InviteeEmail.Equals(currentUser.Email, StringComparison.OrdinalIgnoreCase)
                                && i.Status == InvitationStatus.Pending)
                    .Select(i => new
                    {
                        i.InvitationId,
                        i.GroupId,
                        GroupName = dm.Groups.FirstOrDefault(g => g.GroupId == i.GroupId)?.GroupName ?? "(Desconocido)",
                        i.InviterEmail,
                        Status = i.Status.ToString()
                    })
                    .ToList();

                dgvInvitations.DataSource = pendingInvitations;
            }
            catch (Exception ex)
            {
                ShowError("Error al cargar las invitaciones", ex);
            }
        }

        #endregion

        #region Eventos de botones

        private void btnAccept_Click(object sender, EventArgs e) =>
            HandleInvitationAction(InvitationStatus.Accepted);

        private void btnReject_Click(object sender, EventArgs e) =>
            HandleInvitationAction(InvitationStatus.Rejected);

        #endregion

        #region Procesamiento de invitaciones

        /// <summary>
        /// Procesa la invitación seleccionada en el grid con el nuevo estado especificado.
        /// </summary>
        /// <param name="newStatus">Nuevo estado a asignar a la invitación.</param>
        private void HandleInvitationAction(InvitationStatus newStatus)
        {
            if (dgvInvitations.CurrentRow == null) return;

            try
            {
                int invitationId = GetSelectedInvitationId();
                var invitation = FindInvitationById(invitationId);

                if (invitation == null) return;

                UpdateInvitationStatus(invitation, newStatus);

                // Recargar lista de invitaciones pendientes
                LoadPendingInvitations();
            }
            catch (Exception ex)
            {
                ShowError("Error al procesar la invitación", ex);
            }
        }

        private int GetSelectedInvitationId() =>
            (int)dgvInvitations.CurrentRow.Cells["InvitationId"].Value;

        private static Invitation FindInvitationById(int invitationId) =>
            DataManager.Instance.Invitations.FirstOrDefault(i => i.InvitationId == invitationId);

        private void UpdateInvitationStatus(Invitation invitation, InvitationStatus newStatus)
        {
            invitation.Status = newStatus;

            if (newStatus == InvitationStatus.Accepted)
                AddUserToGroup(invitation.GroupId);
        }

        /// <summary>
        /// Añade al usuario actual al grupo indicado y guarda cambios en grupos.json.
        /// </summary>
        private void AddUserToGroup(int groupId)
        {
            var dm = DataManager.Instance;
            var group = dm.Groups.FirstOrDefault(g => g.GroupId == groupId);

            if (group == null) return;

            group.Members ??= new System.Collections.Generic.List<string>();

            if (!group.Members.Contains(currentUser.Email))
            {
                group.Members.Add(currentUser.Email);
                dm.SaveGroups(); 
            }
        }

        #endregion

        #region Helpers

        private static void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
