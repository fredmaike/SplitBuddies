using System;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario para gestionar las invitaciones pendientes de un usuario.
    /// Permite aceptar o rechazar invitaciones y actualiza automáticamente los grupos.
    /// </summary>
    public partial class InvitationList : Form
    {
        /// <summary>
        /// Usuario actualmente logueado que recibirá las invitaciones.
        /// </summary>
        private readonly User currentUser;

        /// <summary>
        /// Inicializa el formulario y carga las invitaciones pendientes.
        /// </summary>
        /// <param name="user">Usuario que recibirá las invitaciones.</param>
        public InvitationList(User user)
        {
            InitializeComponent();
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            LoadPendingInvitations();
        }

        #region Carga de datos

        /// <summary>
        /// Carga las invitaciones pendientes del usuario en el DataGridView.
        /// Muestra solo las invitaciones con estado "Pending".
        /// </summary>
        private void LoadPendingInvitations()
        {
            try
            {
                var dm = DataManager.Instance;

                // Filtra solo invitaciones pendientes del usuario actual
                var pendingInvitations = dm.Invitations
                    .Where(i => i.InviteeEmail.Equals(currentUser.Email, StringComparison.OrdinalIgnoreCase)
                                && i.Status == InvitationStatus.Pending)
                    .Select(i => new
                    {
                        i.InvitationId,
                        i.GroupId,
                        // Si el grupo no existe, se muestra "(Desconocido)"
                        GroupName = dm.Groups.FirstOrDefault(g => g.GroupId == i.GroupId)?.GroupName ?? "(Desconocido)",
                        i.InviterEmail,
                        Status = i.Status.ToString()
                    })
                    .ToList();

                dgvInvitations.DataSource = pendingInvitations;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las invitaciones: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Eventos de botones

        /// <summary>
        /// Evento al hacer clic en el botón Aceptar:
        /// Cambia el estado de la invitación a "Accepted" y agrega al usuario al grupo.
        /// </summary>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            ProcessSelectedInvitation(InvitationStatus.Accepted);
        }

        /// <summary>
        /// Evento al hacer clic en el botón Rechazar:
        /// Cambia el estado de la invitación a "Rejected".
        /// </summary>
        private void btnReject_Click(object sender, EventArgs e)
        {
            ProcessSelectedInvitation(InvitationStatus.Rejected);
        }

        #endregion

        #region Procesamiento de invitaciones

        /// <summary>
        /// Procesa la invitación seleccionada en el DataGridView,
        /// cambiando su estado y, si aplica, agregando al usuario al grupo.
        /// </summary>
        /// <param name="newStatus">Nuevo estado de la invitación (Accepted o Rejected).</param>
        private void ProcessSelectedInvitation(InvitationStatus newStatus)
        {
            if (dgvInvitations.CurrentRow == null) return;

            try
            {
                int invitationId = (int)dgvInvitations.CurrentRow.Cells["InvitationId"].Value;
                var dm = DataManager.Instance;

                // Buscar la invitación por ID
                var invitation = dm.Invitations.FirstOrDefault(i => i.InvitationId == invitationId);
                if (invitation == null) return;

                // Actualizar estado
                invitation.Status = newStatus;

                // Si se acepta, añadir usuario al grupo
                if (newStatus == InvitationStatus.Accepted)
                    AddUserToGroup(invitation.GroupId);

                // Guardar cambios y recargar la lista
                dm.SaveInvitations();
                dm.SaveGroups();
                LoadPendingInvitations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar la invitación: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Agrega el usuario actual a la lista de miembros de un grupo.
        /// Evita duplicados y maneja listas nulas.
        /// </summary>
        /// <param name="groupId">ID del grupo al cual agregar el usuario.</param>
        private void AddUserToGroup(int groupId)
        {
            var dm = DataManager.Instance;
            var group = dm.Groups.FirstOrDefault(g => g.GroupId == groupId);

            if (group == null) return;

            group.Members ??= new System.Collections.Generic.List<string>();

            if (!group.Members.Contains(currentUser.Email))
                group.Members.Add(currentUser.Email);
        }

        #endregion
    }
}
