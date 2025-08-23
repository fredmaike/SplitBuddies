using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitBuddies.Controllers;
using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using System;
using System.Collections.Generic;

namespace SplitBuddies.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController controller = null!;

        [TestInitialize]
        public void Setup()
        {

            {
                var dm = DataManager.Instance;
                dm.Users.Clear();
                dm.Groups.Clear();
                dm.Expenses.Clear();
                dm.Invitations.Clear();
                AppSession.SignOut();
            }
            // Usuarios de prueba
            var testUsers = new List<User>
            {
                new User { Email = "juan@example.com", Password = "1234", Name = "Juan" },
                new User { Email = "ana@example.com", Password = "abcd", Name = "Ana" }
            };

            // Inyectamos usuarios en el controlador
            controller = new UserController(testUsers);
        }

        [TestMethod]
        public void Login_UsuarioExistente_ContraseñaCorrecta_DeberiaRetornarUsuario()
        {
            // Act
            var user = controller.Login("juan@example.com", "1234");

            // Assert
            Assert.IsNotNull(user, "El usuario retornado no debería ser null.");
            Assert.AreEqual("Juan", user.Name, "El nombre del usuario no coincide.");
        }

        [TestMethod]
        public void Login_UsuarioNoExistente_DeberiaLanzarExcepcion()
        {
            // Act & Assert
            var ex = Assert.ThrowsExactly<InvalidOperationException>(() =>
                controller.Login("noexiste@example.com", "1234")
            );

            Assert.AreEqual("Usuario no encontrado.", ex.Message, "El mensaje de excepción no coincide.");
        }

        [TestMethod]
        public void Login_ContraseñaIncorrecta_DeberiaLanzarExcepcion()
        {
            // Act & Assert
            var ex = Assert.ThrowsExactly<UnauthorizedAccessException>(() =>
                controller.Login("ana@example.com", "wrongpassword")
            );

            Assert.AreEqual("Contraseña incorrecta.", ex.Message, "El mensaje de excepción no coincide.");
        }
    }
}
