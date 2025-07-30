using SplitBuddies.Models;

namespace SplitBuddies.Utils
{
    public static class UsuarioFactory
    {
        public static User CrearUsuario(string tipo, string nombre, string email, string password)
        {
            return new User
            {
                Name = nombre,
                Email = email,
                Password = password,
                Type = tipo
            };
        }
    }
}