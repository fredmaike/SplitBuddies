using System;

namespace SplitBuddies.Utils
{
    /// <summary>
    /// Clase est�tica que gestiona el estado de sesi�n de la aplicaci�n.
    /// </summary>
    public static class AppSession
    {
        private static readonly object _lock = new object();

        /// <summary>
        /// Correo electr�nico del usuario autenticado actualmente.
        /// </summary>
        public static string CurrentUserEmail { get; private set; }

        /// <summary>
        /// Indica si hay un usuario autenticado en la sesi�n.
        /// </summary>
        public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(CurrentUserEmail);

        /// <summary>
        /// Inicia sesi�n estableciendo el correo del usuario autenticado.
        /// </summary>
        /// <param name="email">Correo electr�nico del usuario.</param>
        /// <exception cref="ArgumentException">Si el correo es nulo o vac�o.</exception>
        public static void SignIn(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo no puede estar vac�o.", nameof(email));

            lock (_lock)
            {
                CurrentUserEmail = email;
            }
        }

        /// <summary>
        /// Cierra la sesi�n del usuario actual.
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
