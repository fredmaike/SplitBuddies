using System.Collections.Generic;

namespace SplitBuddies.Models
{
    public class Group
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public List<User> Members { get; set; } = new List<User>();
        public List<Expense> Expenses { get; set; } = new List<Expense>();
    }
}