using System;

namespace SplitBuddies.Utils
{
    public static class AppSession
    {
        public static string CurrentUserEmail { get; private set; }

        public static void SignIn(string email)
        {
            CurrentUserEmail = email ?? throw new ArgumentNullException(nameof(email));
        }

        public static void SignOut()
        {
            CurrentUserEmail = null;
        }

        public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(CurrentUserEmail);
    }
}


