namespace SplitBuddies.Models
{
    public class Invitation
    {
        public int InvitationId { get; set; }
        public int GroupId { get; set; }
        public string InviteeEmail { get; set; }
        public string InviterEmail { get; set; }
        public string Status { get; set; } // "Pending", "Accepted", "Rejected"
    }
}
