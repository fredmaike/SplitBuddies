using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SplitBuddies.Controllers
{
    public class UserController
    {
        private List<User> usuarios = new List<User>();

        public void LoadUsers()
        {
            usuarios = DataStorage.LoadUsers();
        }

        public User Login(string email, string password)
        {
            User user = usuarios.FirstOrDefault(u =>
                u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                throw new InvalidOperationException("Usuario no encontrado.");
            }

            if (user.Password != password)
            {
                throw new UnauthorizedAccessException("Contraseña incorrecta.");
            }

            return user;
        }
    }
}