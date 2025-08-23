using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitBuddies.Data;
using SplitBuddies.Utils;
using System;

namespace SplitBuddies.Tests
{
    [TestClass]
    [DoNotParallelize] 
    public class AppSessionTests
    {
        [TestInitialize]

        public void Setup()
        {
            var dm = DataManager.Instance;
            dm.Users.Clear();
            dm.Groups.Clear();
            dm.Expenses.Clear();
            dm.Invitations.Clear();
        }

        [TestMethod]
        public void SignIn_UsuarioValido_DeberiaEstablecerCorreo()
        {
            // Act
            AppSession.SignIn("juan@example.com");

            // Assert
            Assert.AreEqual("juan@example.com", AppSession.CurrentUserEmail, "El correo del usuario autenticado no coincide.");
            Assert.IsTrue(AppSession.IsAuthenticated, "IsAuthenticated debería ser true después de SignIn.");
        }

        [TestMethod]
        public void SignOut_DeberiaLimpiarSesion()
        {
            // Arrange: aseguramos que hay un usuario autenticado
            AppSession.SignIn("ana@example.com");

            // Act
            AppSession.SignOut();

            // Assert
            Assert.IsNull(AppSession.CurrentUserEmail, "CurrentUserEmail debería ser null después de SignOut.");
            Assert.IsFalse(AppSession.IsAuthenticated, "IsAuthenticated debería ser false después de SignOut.");
        }

        [TestMethod]
        public void SignIn_CorreoVacio_DeberiaLanzarExcepcion()
        {
            // Act & Assert
            var ex = Assert.ThrowsExactly<ArgumentException>(() =>
            {
                AppSession.SignIn(""); // lanza excepción
            });

            Assert.AreEqual(
                "El correo no puede estar vacío. (Parameter 'email')",
                ex.Message
            );
        }

        [TestMethod]
        public void IsAuthenticated_SinUsuario_DeberiaSerFalso()
        {
            // Act & Assert
            Assert.IsFalse(AppSession.IsAuthenticated, "IsAuthenticated debería ser false cuando no hay usuario autenticado.");
            Assert.IsNull(AppSession.CurrentUserEmail, "CurrentUserEmail debería ser null cuando no hay usuario autenticado.");
        }
    }
}
