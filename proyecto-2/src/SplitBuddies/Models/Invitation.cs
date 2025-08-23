namespace SplitBuddies.Models
{
    /// <summary>
    /// Representa una invitaci�n enviada a un usuario para unirse a un grupo.
    /// </summary>
    public class Invitation
    {
        /// <summary>
        /// Identificador �nico de la invitaci�n.
        /// </summary>
        public int InvitationId { get; set; }

        /// <summary>
        /// Identificador del grupo al que pertenece la invitaci�n.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Correo electr�nico del usuario invitado.
        /// </summary>
        public string InviteeEmail { get; set; } = string.Empty;

        /// <summary>
        /// Correo electr�nico del usuario que envi� la invitaci�n.
        /// </summary>
        public string InviterEmail { get; set; } = string.Empty;

        /// <summary>
        /// Estado actual de la invitaci�n.
        /// </summary>
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
    }

    /// <summary>
    /// Estados posibles para una invitaci�n.
    /// </summary>
    public enum InvitationStatus
    {
        Pending,
        Accepted,
        Rejected
    }
}
