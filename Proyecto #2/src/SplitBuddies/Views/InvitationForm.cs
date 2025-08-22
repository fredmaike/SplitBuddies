using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

using GroupModel = SplitBuddies.Models.Group; // Alias para evitar conflicto con Regex.Group

namespace SplitBuddies.Views
{
    public partial class InvitationForm : Form
    {
        private GroupModel group;
        private User currentUser;

        public InvitationForm(GroupModel group, User user)
        {
            InitializeComponent(); // Inicializa los controles declarados en el Designer
            this.group = group ?? throw new ArgumentNullException(nameof(group));
            this.currentUser = user ?? throw new ArgumentNullException(nameof(user));

            this.Text = $"Invitar usuarios a {group.GroupName}";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string inviteeEmail = txtEmail.Text.Trim();

            if (!IsValidEmail(inviteeEmail))
            {
                MessageBox.Show("Ingrese un email válido.", "Invitación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            group.Members ??= new System.Collections.Generic.List<string>();

            if (group.Members.Any(m => string.Equals(m, inviteeEmail, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ese email ya es miembro del grupo.", "Invitación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dm = DataManager.Instance;

            dm.Invitations.Add(new Invitation
            {
                InvitationId = dm.GetNextInvitationId(),
                GroupId = group.GroupId,
                InviteeEmail = inviteeEmail,
                InviterEmail = currentUser.Email,
                Status = "Pending"
            });

            dm.SaveInvitations();
            MessageBox.Show("Invitación enviada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtEmail.Clear();
        }

        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }
    }
}
