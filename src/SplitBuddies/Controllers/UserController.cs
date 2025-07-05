using SplitBuddies.Data;
using SplitBuddies.Models;

public static class UserController
{
    public static List<User> Users = DataLoader.LoadUsers();

    public static bool Register(User user)
    {
        if (Users.Any(u => u.Email == user.Email))
            return false;

        Users.Add(user);
        DataLoader.SaveUsers(Users);
        return true;
    }

    public static User Login(string email)
    {
        return Users.FirstOrDefault(u => u.Email == email);
    }
}
