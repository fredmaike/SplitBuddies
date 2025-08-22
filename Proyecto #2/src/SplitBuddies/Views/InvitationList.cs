using System;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public partial class InvitationList : Form
    {
        private User currentUser;

        public InvitationList(User user)
        {
            InitializeComponent();
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            LoadPendingInvitations();
        }

        private void LoadPendingInvitations()
        {
            var dm = DataManager.Instance;

            var pending = dm.Invitations
                .Where(i => i.InviteeEmail.Equals(currentUser.Email, StringComparison.OrdinalIgnoreCase)
                         && i.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase))
                .Select(i => new
                {
                    i.InvitationId,
                    i.GroupId,
                    GroupName = dm.Groups.FirstOrDefault(g => g.GroupId == i.GroupId)?.GroupName ?? "(Desconocido)",
                    i.InviterEmail,
                    i.Status
                })
                .ToList();

            dgvInvitations.DataSource = pending;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (dgvInvitations.CurrentRow == null) return;

            int id = (int)dgvInvitations.CurrentRow.Cells["InvitationId"].Value;
            var dm = DataManager.Instance;

            var invitation = dm.Invitations.FirstOrDefault(i => i.InvitationId == id);
            if (invitation != null)
            {
                invitation.Status = "Accepted";

                var group = dm.Groups.FirstOrDefault(g => g.GroupId == invitation.GroupId);
                if (group != null)
                {
                    group.Members ??= new System.Collections.Generic.List<string>();
                    if (!group.Members.Contains(currentUser.Email))
                        group.Members.Add(currentUser.Email);
                }

                dm.SaveInvitations();
                dm.SaveGroups();
                LoadPendingInvitations();
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dgvInvitations.CurrentRow == null) return;

            int id = (int)dgvInvitations.CurrentRow.Cells["InvitationId"].Value;
            var dm = DataManager.Instance;

            var invitation = dm.Invitations.FirstOrDefault(i => i.InvitationId == id);
            if (invitation != null)
            {
                invitation.Status = "Rejected";
                dm.SaveInvitations();
                LoadPendingInvitations();
            }
        }
    }
}
