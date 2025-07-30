using System.Collections.Generic;
using System.Linq;
using SplitBuddies.Models;

namespace SplitBuddies.Controllers
{
    public class GroupController
    {
        private readonly List<Group> groups;

        public GroupController(List<Group> groups)
        {
            this.groups = groups;
        }

        public List<Group> GetGroupsForUser(string email)
        {
            return groups.Where(g => g.Members.Contains(email)).ToList();
        }

        public void CreateGroup(string name, string imagePath, List<string> memberEmails)
        {
            var newGroup = new Group
            {
                GroupId = groups.Count > 0 ? groups.Max(g => g.GroupId) + 1 : 1,  
                GroupName = name,
                Members = memberEmails, 
                Expenses = new List<int>()
            };

            groups.Add(newGroup);
        }
        public void DeleteGroup(int groupId)
        {
            var groupToRemove = groups.FirstOrDefault(g => g.GroupId == groupId);
            if (groupToRemove != null)
            {
                groups.Remove(groupToRemove);
            }
        }

    }
}
