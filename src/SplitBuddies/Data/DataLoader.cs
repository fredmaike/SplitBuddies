using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SplitBuddies.Models;

namespace SplitBuddies.Data
{
    public static class DataLoader
    {
        private static string userFile = "usuarios.json";
        private static string groupFile = "grupos.json";

        public static List<User> LoadUsers()
        {
            if (!File.Exists(userFile))
                return new List<User>();

            string json = File.ReadAllText(userFile);
            return JsonSerializer.Deserialize<List<User>>(json);
        }

        public static void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(userFile, json);
        }

        public static List<Group> LoadGroups()
        {
            if (!File.Exists(groupFile))
                return new List<Group>();

            string json = File.ReadAllText(groupFile);
            return JsonSerializer.Deserialize<List<Group>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public static void SaveGroups(List<Group> groups)
        {
            string json = JsonSerializer.Serialize(groups, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(groupFile, json);
        }
    }
}
