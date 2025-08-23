using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitBuddies.Controllers;
using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SplitBuddies.Tests
{
    [TestClass]
    public class GroupControllerTests
    {
        private GroupController? controller;
        private List<Group> groups = new List<Group>();

        [TestInitialize]
        public void Setup()
        {
            var dm = DataManager.Instance;
            dm.Users.Clear();
            dm.Groups.Clear();
            dm.Expenses.Clear();
            dm.Invitations.Clear();

            // Agregar usuarios
            dm.Users.Add(new User { Email = "alice@test.com", Name = "Alice", Password = "123" });
            dm.Users.Add(new User { Email = "bob@test.com", Name = "Bob", Password = "123" });

            // Inicializar controlador pasando la lista de grupos de DataManager
            controller = new GroupController(dm.Groups);

            // Mantener referencia a la lista de grupos para verificaciones en los tests
            groups = dm.Groups;
        }

        [TestMethod]
        public void CreateGroup_ShouldAddGroupToList()
        {
            // Arrange
            string name = "GrupoTest";
            string imagePath = "ruta/imagen.png";
            var members = new List<string> { "alice@test.com", "bob@test.com" };

            // Act
            var newGroup = controller!.CreateGroup(name, imagePath, members);

            // Assert
            Assert.IsNotNull(newGroup, "El grupo creado no debe ser null");
            Assert.AreEqual(name, newGroup.GroupName);
            Assert.AreEqual(imagePath, newGroup.IMAGE);
            CollectionAssert.AreEqual(members, newGroup.Members);
            Assert.AreEqual(1, newGroup.GroupId); // Primer grupo -> ID = 1
            Assert.IsTrue(groups.Contains(newGroup), "El grupo debe estar en la lista interna");
        }

        [TestMethod]
        public void GetGroupsForUser_ShouldReturnOnlyGroupsWhereUserIsMember()
        {
            // Arrange
            var group1 = controller!.CreateGroup("Grupo1", "", new List<string> { "alice@test.com" });
            controller.CreateGroup("Grupo2", "", new List<string> { "bob@test.com" });
            var group3 = controller.CreateGroup("Grupo3", "", new List<string> { "alice@test.com", "bob@test.com" });

            // Act
            var aliceGroups = controller.GetGroupsForUser("alice@test.com");

            // Assert
            CollectionAssert.AreEquivalent(new List<Group> { group1, group3 }, aliceGroups);
        }

        [TestMethod]
        public void DeleteGroup_ExistingGroup_ShouldReturnTrueAndRemoveGroup()
        {
            // Arrange
            var group = controller!.CreateGroup("Grupo1", "", new List<string> { "alice@test.com" });
            int groupId = group.GroupId;

            // Act
            bool result = controller.DeleteGroup(groupId);

            // Assert
            Assert.IsTrue(result, "Debe retornar true al eliminar un grupo existente");
            Assert.IsFalse(groups.Contains(group), "El grupo debe ser eliminado de la lista");
        }

        [TestMethod]
        public void DeleteGroup_NonExistingGroup_ShouldReturnFalse()
        {
            // Act
            bool result = controller!.DeleteGroup(999); // ID que no existe

            // Assert
            Assert.IsFalse(result, "Debe retornar false al intentar eliminar un grupo inexistente");
        }
    }
}
