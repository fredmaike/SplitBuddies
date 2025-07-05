using System;
using System.Collections.Generic;

namespace SplitBuddies.Models
{
    public class Expense
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public User Payer { get; set; }  // Quién pagó
        public List<User> Participants { get; set; }  // A quiénes afecta
        public DateTime Date { get; set; }
        public string ReferenceUrl { get; set; }  // Opcional: URL o comprobante
    }
}
