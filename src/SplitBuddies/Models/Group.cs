using System.Collections.Generic;

namespace SplitBuddies.Models
{
    public class Group
    {
        public string Name { get; set; }
        public List<User> Members { get; set; } = new();
        public List<Expense> Expenses { get; set; } = new();
    }
}
