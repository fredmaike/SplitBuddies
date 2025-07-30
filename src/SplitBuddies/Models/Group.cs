using Newtonsoft.Json;
using System.Collections.Generic;

namespace SplitBuddies.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        [JsonProperty("IMAGE")]
        public string IMAGE { get; set; }

        public List<string> Members { get; set; }

        public List<int> Expenses { get; set; } = new List<int>();
    }
}