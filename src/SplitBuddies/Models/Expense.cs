using System;
using System.Collections.Generic;

namespace SplitBuddies.Models
{
    public class Expense
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public User Payer { get; set; }
        public List<User> Participants { get; set; } = new();
        public DateTime Date { get; set; }
    }
}
