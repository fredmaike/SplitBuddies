namespace SplitBuddies.Models
{
    /// <summary>
    /// Invitación para que un usuario (por email) se una a un grupo.
    /// </summary>
    public class Invitation
    {
        public int InvitationId { get; set; }
        public int GroupId { get; set; }

        /// <summary> Email del invitado. </summary>
        public string InviteeEmail { get; set; }

        /// <summary> Email del emisor (opcional). </summary>
        public string InviterEmail { get; set; }

        /// <summary> Pending / Accepted / Rejected. </summary>
        public string Status { get; set; } = "Pending";
    }
}
