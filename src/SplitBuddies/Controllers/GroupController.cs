using System.Collections.Generic;
using SplitBuddies.Models;
using SplitBuddies.Data;

namespace SplitBuddies.Controllers
{
    public static class GroupController
    {
        public static List<Group> Groups = DataLoader.LoadGroups();

        public static void AddGroup(Group group)
        {
            Groups.Add(group);
            DataLoader.SaveGroups(Groups);
        }
        public static void SaveAllGroups()
        {
            DataLoader.SaveGroups(Groups);
        }

    }
}
