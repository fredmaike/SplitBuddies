using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using System.Collections.Generic;

namespace SplitBuddies.Tests
{
    [TestClass]
    public class DataManagerTests
    {
        private DataManager manager = null!;

        [TestInitialize]
        public void Setup()
        {
            manager = DataManager.Instance; 
            manager.Users.Clear();
            manager.Groups.Clear();
            manager.Expenses.Clear();
            manager.Invitations.Clear();
            AppSession.SignOut();
        }

        [TestMethod]
        public void AddUser_ShouldAddUserToUsersList()
        {
            var user = new User { Email = "alice@test.com", Name = "Alice", Password = "1234" };
            manager.Users.Add(user);

            Assert.AreEqual(1, manager.Users.Count);
            Assert.AreEqual("alice@test.com", manager.Users[0].Email);
        }

        [TestMethod]
        public void AddGroup_ShouldAddGroupToGroupsList()
        {
            var group = new Group { GroupId = 1, Members = new List<string> { "alice@test.com" } };
            manager.Groups.Add(group);

            Assert.AreEqual(1, manager.Groups.Count);
            Assert.AreEqual(1, manager.Groups[0].GroupId);
        }

        [TestMethod]
        public void GetNextExpenseId_ShouldReturnIncrementalId()
        {
            manager.Expenses.Add(new Expense { Id = 1, Name = "Cena" });
            manager.Expenses.Add(new Expense { Id = 2, Name = "Almuerzo" });

            int nextId = manager.GetNextExpenseId();

            Assert.AreEqual(3, nextId);
        }

        [TestMethod]
        public void GetNextInvitationId_ShouldReturnIncrementalId()
        {
            manager.Invitations.Add(new Invitation { InvitationId = 10 });
            manager.Invitations.Add(new Invitation { InvitationId = 11 });

            int nextId = manager.GetNextInvitationId();

            Assert.AreEqual(12, nextId);
        }
    }
}
