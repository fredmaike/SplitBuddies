namespace SplitBuddies.Models
{
    /// <summary>
    /// Representa una invitación enviada a un usuario para unirse a un grupo.
    /// </summary>
    public class Invitation
    {
        /// <summary>
        /// Identificador único de la invitación.
        /// </summary>
        public int InvitationId { get; set; }

        /// <summary>
        /// Identificador del grupo al que pertenece la invitación.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Correo electrónico del usuario invitado.
        /// </summary>
        public string InviteeEmail { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario que envió la invitación.
        /// </summary>
        public string InviterEmail { get; set; } = string.Empty;

        /// <summary>
        /// Estado actual de la invitación.
        /// </summary>
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
    }

    /// <summary>
    /// Estados posibles para una invitación.
    /// </summary>
    public enum InvitationStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
