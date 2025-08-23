namespace SplitBuddies.Models
{
    /// <summary>
    /// Representa un usuario dentro de la aplicación SplitBuddies.
    /// Contiene información básica como nombre, correo y contraseña.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Contraseña del usuario.
        /// </summary>
        public string Password { get; set; }
    }
}
