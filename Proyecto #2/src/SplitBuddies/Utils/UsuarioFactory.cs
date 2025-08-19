using SplitBuddies.Models;

namespace SplitBuddies.Utils
{
    // Clase fábrica para crear objetos User.
    // Permite centralizar la creación y configuración de usuarios
    public static class UsuarioFactory
    {
        // Crea un nuevo usuario con los datos proporcionados.
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
