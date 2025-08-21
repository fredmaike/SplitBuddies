namespace SplitBuddies.Models
{
    /// <summary>
    /// Transferencia propuesta de un usuario a otro.
    /// </summary>
    public class Settlement
    {
        public string FromEmail { get; set; }
        public string ToEmail   { get; set; }
        public decimal Amount   { get; set; }
    }
}
