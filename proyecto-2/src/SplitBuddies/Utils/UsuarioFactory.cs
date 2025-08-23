using SplitBuddies.Models;
using System;

namespace SplitBuddies.Utils
{
    /// <summary>
    /// Fábrica estática para crear instancias de <see cref="User"/>.
    /// Centraliza la creación y validación de usuarios.
    /// </summary>
    public static class UsuarioFactory
    {
        /// <summary>
        /// Crea y devuelve un nuevo usuario con los datos proporcionados.
        /// </summary>
        /// <param name="nombre">Nombre completo del usuario.</param>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>Nuevo objeto <see cref="User"/> con las propiedades asignadas.</returns>
        /// <exception cref="ArgumentException">Si algún parámetro es nulo o vacío.</exception>
        public static User CrearUsuario(string nombre, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo electrónico no puede estar vacío.", nameof(email));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));

            return new User
            {
                Name = nombre,
                Email = email,
                Password = password
            };
        }
    }
}
