using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using System;   

namespace SplitBuddies.Tests
{
    [TestClass]
    public class SettlementTests
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

        public void Constructor_ValoresValidos_DeberiaInicializarPropiedades()
        {
            // Act
            var settlement = new Settlement("a@example.com", "b@example.com", 100m);

            // Assert
            Assert.AreEqual("a@example.com", settlement.FromEmail);
            Assert.AreEqual("b@example.com", settlement.ToEmail);
            Assert.AreEqual(100m, settlement.Amount);
        }

        [TestMethod]
        public void Constructor_FromEmailVacio_DeberiaLanzarExcepcion()
        {
            // Act & Assert
            var ex = Assert.ThrowsExactly<ArgumentException>(() =>
                new Settlement("", "b@example.com", 50m)
            );

            Assert.AreEqual("El correo del emisor no puede estar vacío. (Parameter 'fromEmail')", ex.Message);
        }

        [TestMethod]
        public void Constructor_ToEmailVacio_DeberiaLanzarExcepcion()
        {
            // Act & Assert
            var ex = Assert.ThrowsExactly<ArgumentException>(() =>
                new Settlement("a@example.com", "   ", 50m)
            );

            Assert.AreEqual("El correo del receptor no puede estar vacío. (Parameter 'toEmail')", ex.Message);
        }

        [TestMethod]
        public void Constructor_MontoNegativo_DeberiaLanzarExcepcion()
        {
            // Act & Assert
            var ex = Assert.ThrowsExactly<ArgumentOutOfRangeException>(() =>
                new Settlement("a@example.com", "b@example.com", -10m)
            );

            Assert.IsTrue(ex.Message.Contains("El monto debe ser mayor o igual a cero."));
        }
    }
}