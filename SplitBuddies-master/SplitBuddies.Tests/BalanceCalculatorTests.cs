using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using SplitBuddies.Data;
using System.Collections.Generic;

namespace SplitBuddies.Tests
{
    [TestClass]
    public class BalanceCalculatorTests
    {
        [TestInitialize]
        public void Setup()
        {
            var dm = DataManager.Instance;
            dm.Users.Clear();
            dm.Groups.Clear();
            dm.Expenses.Clear();
            dm.Invitations.Clear();
            AppSession.SignOut();
        }

        [TestMethod]
        public void CalcularBalancePorUsuario_SinGastos_DeberiaRetornarCero()
        {
            var grupo = new Group
            {
                GroupId = 1,
                Members = new List<string> { "a@example.com", "b@example.com" },
                Expenses = new List<int>()
            };

            var balances = BalanceCalculator.CalcularBalancePorUsuario(grupo);

            Assert.AreEqual(2, balances.Count);
            Assert.AreEqual(0m, balances["a@example.com"]);
            Assert.AreEqual(0m, balances["b@example.com"]);
        }

        [TestMethod]
        public void CalcularBalancePorUsuario_GastoConParticipanteExterno_DeberiaIgnorarParticipante()
        {
            var grupo = new Group
            {
                GroupId = 1,
                Members = new List<string> { "a@example.com", "b@example.com" },
                Expenses = new List<int>()
            };

            var gasto = new Expense
            {
                Id = 1,
                Name = "Cena",
                Amount = 100m,
                PaidByEmail = "a@example.com",
                InvolvedUsersEmails = new List<string> { "a@example.com", "b@example.com", "c@example.com" },
                GroupId = 1
            };

            // Registrar el gasto global
            DataManager.Instance.Expenses.Add(gasto);

            // Asociar el gasto al grupo
            grupo.Expenses.Add(gasto.Id);

            var balances = BalanceCalculator.CalcularBalancePorUsuario(grupo);

            Assert.AreEqual(2, balances.Count);
            Assert.AreEqual(50m, balances["a@example.com"]);
            Assert.AreEqual(-50m, balances["b@example.com"]);
        }

        [TestMethod]
        public void CalcularDeudas_BalancesPositivosYNegativos_DeberiaGenerarLiquidaciones()
        {
            var netos = new Dictionary<string, decimal>
            {
                { "a@example.com", -50m },
                { "b@example.com", 30m },
                { "c@example.com", 20m }
            };

            var deudas = BalanceCalculator.CalcularDeudas(netos);

            Assert.AreEqual(2, deudas.Count);
            Assert.AreEqual("a@example.com", deudas[0].FromEmail);
            Assert.AreEqual("b@example.com", deudas[0].ToEmail);
            Assert.AreEqual(30m, deudas[0].Amount);

            Assert.AreEqual("a@example.com", deudas[1].FromEmail);
            Assert.AreEqual("c@example.com", deudas[1].ToEmail);
            Assert.AreEqual(20m, deudas[1].Amount);
        }
    }
}
