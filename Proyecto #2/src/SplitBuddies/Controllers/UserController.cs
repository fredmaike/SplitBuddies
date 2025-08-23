using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SplitBuddies.Controllers
{
    // Controlador encargado de manejar la lógica relacionada con los usuarios,
    // como la carga de datos y el inicio de sesión 
    public class UserController
    {
        private List<User> usuarios;

        // Constructor vacío
        public UserController()
        {
            usuarios = new List<User>();
        }

        // Constructor de prueba para inyectar usuarios
        public UserController(List<User> testUsers)
        {
            usuarios = testUsers;
        }

        public void LoadUsers()
        {
            usuarios = DataStorage.LoadUsers();
        }

        public User Login(string email, string password)
        {
            User user = usuarios.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                throw new InvalidOperationException("Usuario no encontrado.");

            if (user.Password != password)
                throw new UnauthorizedAccessException("Contraseña incorrecta.");

            return user;
        }
    }
}
