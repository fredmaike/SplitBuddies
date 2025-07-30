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
        // Lista de usuarios en memoria
        private List<User> usuarios = new List<User>();

        // Carga los usuarios desde el almacenamiento externo
        public void LoadUsers()
        {
            usuarios = DataStorage.LoadUsers();
        }

        // Valida las credenciales del usuario e inicia sesión si son correctas
        public User Login(string email, string password)
        {
            // Buscar usuario por correo 
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
