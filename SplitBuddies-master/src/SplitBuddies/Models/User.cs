namespace SplitBuddies.Models
{
    /// <summary>
    /// Representa un usuario dentro de la aplicaci�n SplitBuddies.
    /// Contiene informaci�n b�sica como nombre, correo y contrase�a.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Correo electr�nico del usuario.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Contrase�a del usuario.
        /// </summary>
        public string Password { get; set; }
    }
}
