using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SplitBuddies.Controllers
{
    /// <summary>
    /// Controlador encargado de manejar la lógica relacionada con los usuarios,
    /// como la carga de datos y el inicio de sesión.
    /// </summary>
    public class UserController
    {
        private List<User> users;

        /// <summary>
        /// Constructor por defecto. Inicializa la lista de usuarios vacía.
        /// </summary>
        public UserController() : this(new List<User>()) { }

        /// <summary>
        /// Constructor para inyectar una lista de usuarios (ideal para pruebas).
        /// </summary>
        /// <param name="testUsers">Lista inicial de usuarios.</param>
        public UserController(List<User> testUsers)
        {
            users = testUsers ?? new List<User>();
        }

        /// <summary>
        /// Carga los usuarios desde el almacenamiento de datos.
        /// </summary>
        public void LoadUsers()
        {
            users = DataStorage.LoadUsers();
        }

        /// <summary>
        /// Intenta iniciar sesión con el correo y contraseña proporcionados.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>El objeto <see cref="User"/> si el login es exitoso.</returns>
        /// <exception cref="ArgumentException">Si email o password están vacíos.</exception>
        /// <exception cref="InvalidOperationException">Si el usuario no existe.</exception>
        /// <exception cref="UnauthorizedAccessException">Si la contraseña es incorrecta.</exception>
        public User Login(string email, string password)
        {
            ValidateLoginInput(email, password);

            var user = FindUserByEmail(email);

            EnsurePasswordIsCorrect(user, password);

            return user;
        }

        #region Private Helpers

        private static void ValidateLoginInput(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo electrónico es obligatorio.", nameof(email));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña es obligatoria.", nameof(password));
        }

        private User FindUserByEmail(string email)
        {
            var user = users.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            return user ?? throw new InvalidOperationException($"No se encontró el usuario con correo {email}.");
        }

        private static void EnsurePasswordIsCorrect(User user, string password)
        {
            if (user.Password != password)
                throw new UnauthorizedAccessException("La contraseña ingresada es incorrecta.");
        }

        #endregion
    }
}
