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
        /// <summary>
        /// Lista interna de usuarios en memoria.
        /// </summary>
        private List<User> usuarios = new List<User>();

        /// <summary>
        /// Carga los usuarios desde el almacenamiento externo.
        /// </summary>
        public void LoadUsers()
        {
            usuarios = DataStorage.LoadUsers();
        }

        /// <summary>
        /// Valida las credenciales del usuario e inicia sesión si son correctas.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>Usuario autenticado si las credenciales son válidas.</returns>
        /// <exception cref="InvalidOperationException">Si el usuario no existe.</exception>
        /// <exception cref="UnauthorizedAccessException">Si la contraseña es incorrecta.</exception>
        public User Login(string email, string password)
        {
            // Buscar usuario por correo (ignorando mayúsculas/minúsculas)
            User user = usuarios.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            // Si no existe, lanzar excepción
            if (user == null)
            {
                throw new InvalidOperationException("Usuario no encontrado.");
            }

            // Validar la contraseña
            if (user.Password != password)
            {
                throw new UnauthorizedAccessException("Contraseña incorrecta.");
            }

            // Retornar el usuario autenticado
            return user;
        }
    }
}
