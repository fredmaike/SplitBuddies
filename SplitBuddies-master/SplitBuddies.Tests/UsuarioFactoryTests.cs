using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using System;

namespace SplitBuddies.Tests
{
    [TestClass]
    public class UsuarioFactoryTests
    {
        [TestInitialize]
        public void Setup()
        {
            try
            {
                // Limpiar datos del singleton
                var dm = DataManager.Instance;
                dm.Users.Clear();
                dm.Groups.Clear();
                dm.Expenses.Clear();
                dm.Invitations.Clear();

                // Reiniciar sesión
                AppSession.SignOut();
            }
            catch (Exception ex)
            {
                Assert.Fail("Error en TestInitialize: " + ex.Message);
            }
        }

        [TestMethod]
        public void CrearUsuario_ValoresValidos_DeberiaCrearUsuario()
        {
            // Act
            var user = UsuarioFactory.CrearUsuario("Juan Perez", "juan@example.com", "1234");

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual("Juan Perez", user.Name);
            Assert.AreEqual("juan@example.com", user.Email);
            Assert.AreEqual("1234", user.Password);
        }

        [TestMethod]
        public void CrearUsuario_NombreVacio_DeberiaLanzarExcepcion()
        {
            // Act & Assert
            var ex = Assert.ThrowsExactly<ArgumentException>(() =>
                UsuarioFactory.CrearUsuario("", "juan@example.com", "1234")
            );

            Assert.AreEqual("El nombre no puede estar vacío. (Parameter 'nombre')", ex.Message);
        }

        [TestMethod]
        public void CrearUsuario_EmailVacio_DeberiaLanzarExcepcion()
        {
            // Act & Assert
            var ex = Assert.ThrowsExactly<ArgumentException>(() =>
                UsuarioFactory.CrearUsuario("Juan Perez", "", "1234")
            );

            Assert.AreEqual("El correo electrónico no puede estar vacío. (Parameter 'email')", ex.Message);
        }

        [TestMethod]
        public void CrearUsuario_ContraseñaVacia_DeberiaLanzarExcepcion()
        {
            // Act & Assert
            var ex = Assert.ThrowsExactly<ArgumentException>(() =>
                UsuarioFactory.CrearUsuario("Juan Perez", "juan@example.com", "   ")
            );

            Assert.AreEqual("La contraseña no puede estar vacía. (Parameter 'password')", ex.Message);
        }
    }
}