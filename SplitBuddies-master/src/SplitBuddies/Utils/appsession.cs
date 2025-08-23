using System;

namespace SplitBuddies.Utils
{
    /// <summary>
    /// Clase estática que gestiona el estado de sesión de la aplicación.
    /// </summary>
    public static class AppSession
    {
        private static readonly object _lock = new object();

        /// <summary>
        /// Correo electrónico del usuario autenticado actualmente.
        /// </summary>
        public static string CurrentUserEmail { get; private set; }

        /// <summary>
        /// Indica si hay un usuario autenticado en la sesión.
        /// </summary>
        public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(CurrentUserEmail);

        /// <summary>
        /// Inicia sesión estableciendo el correo del usuario autenticado.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <exception cref="ArgumentException">Si el correo es nulo o vacío.</exception>
        public static void SignIn(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo no puede estar vacío.", nameof(email));

            lock (_lock)
            {
                CurrentUserEmail = email;
            }
        }

        /// <summary>
        /// Cierra la sesión del usuario actual.
        /// </summary>
        public static void SignOut()
        {
            lock (_lock)
            {
                CurrentUserEmail = null;
            }
        }
    }
}
