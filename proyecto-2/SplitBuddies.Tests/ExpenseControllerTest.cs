using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitBuddies.Controllers;
using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SplitBuddies.Tests
{
    [TestClass]
    public class ExpenseControllerTests
    {
        private ExpenseController controller = null!;

        [TestInitialize]
        public void Setup()
        {
            var manager = DataManager.Instance;
            manager.Users.Clear();
            manager.Groups.Clear();
            manager.Expenses.Clear();
            manager.Invitations.Clear();
            AppSession.SignOut();

            // Agregar usuarios de prueba
            manager.Users.Add(new User { Email = "payer@test.com", Name = "Alice", Password = "123" });
            manager.Users.Add(new User { Email = "user1@test.com", Name = "Bob", Password = "123" });

            // Crear grupo de prueba con miembros
            manager.Groups.Add(new Group
            {
                GroupId = 1,
                Expenses = new List<int>(),
                Members = new List<string> { "payer@test.com", "user1@test.com" } // Muy importante
            });

            // Inicializar controlador
            controller = new ExpenseController();
        }

        [TestMethod]
        public void AddExpense_ValidData_ShouldAddExpense()
        {
            var expense = controller.CreateExpense(
                "Cena",
                "Pizza y bebidas",
                "payer@test.com",
                new List<string> { "payer@test.com", "user1@test.com" },
                120m,
                DateTime.Now,
                1
            );


            Assert.IsNotNull(expense);
            Assert.AreEqual("Cena", expense.Name);
            Assert.AreEqual("Pizza y bebidas", expense.Description);
            Assert.AreEqual("payer@test.com", expense.PaidByEmail);
            Assert.AreEqual(120m, expense.Amount);
            Assert.AreEqual(1, expense.GroupId);

            var group = DataManager.Instance.Groups.First(g => g.GroupId == 1);
            Assert.IsTrue(group.Expenses.Contains(expense.Id));
            Assert.IsTrue(DataManager.Instance.Expenses.Contains(expense));
        }

        [TestMethod]
        public void AddExpense_InvalidPayer_ShouldReturnNull()
        {
            var expense = controller.CreateExpense(
                "Cena",
                "Pizza y bebidas",
                "noexiste@test.com",
                new List<string> { "payer@test.com", "user1@test.com" },
                100m,
                DateTime.Now,
                1
            );


            Assert.IsNull(expense);
        }
    }
}
