using SplitBuddies.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SplitBuddies.Services
{
    public static class JsonDataService
    {
        private const string UserFile = @"Data\users.json";

        public static void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(UserFile, json);
        }

        public static List<User> LoadUsers()
        {
            if (!File.Exists(UserFile)) return new List<User>();
            var json = File.ReadAllText(UserFile);
            return JsonSerializer.Deserialize<List<User>>(json);
        }
    }
}
