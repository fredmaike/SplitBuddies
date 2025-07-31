using SplitBuddies.Models;

namespace SplitBuddies.Utils
{
    /// <summary>
    /// Clase fábrica estática para crear objetos User.
    /// Centraliza la creación y configuración de usuarios.
    /// </summary>
    public static class UsuarioFactory
    {
        /// <summary>
        /// Crea y devuelve un nuevo usuario con los datos proporcionados.
        /// </summary>
        /// <param name="nombre">Nombre completo del usuario.</param>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <returns>Nuevo objeto User con las propiedades asignadas.</returns>
        public static User CrearUsuario(string nombre, string email, string password)
        {
            return new User
            {
                Name = nombre,
                Email = email,
                Password = password,
            };
        }
    }
}
