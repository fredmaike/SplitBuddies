namespace SplitBuddies.Models
{
    // Representa un usuario dentro de la aplicaci�n SplitBuddies.
    // Contiene informaci�n b�sica como nombre, correo, contrase�a y tipo de usuario.
    public class User
    {
        // Nombre completo del usuario
        public string Name { get; set; }

        // Correo electr�nico del usuario
        public string Email { get; set; }

        // Contrase�a del usuario 
        public string Password { get; set; }
    }
}
