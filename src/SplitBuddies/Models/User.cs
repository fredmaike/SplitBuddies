namespace SplitBuddies.Models
{
    // Representa un usuario dentro de la aplicación SplitBuddies.
    // Contiene información básica como nombre, correo, contraseña y tipo de usuario.
    public class User
    {
        // Nombre completo del usuario
        public string Name { get; set; }

        // Correo electrónico del usuario
        public string Email { get; set; }

        // Contraseña del usuario 
        public string Password { get; set; }
    }
}
