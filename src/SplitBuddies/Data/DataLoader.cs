using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SplitBuddies.Models;

namespace SplitBuddies.Data
{
    public static class DataLoader
    {
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuarios.json");


        public static List<User> LoadUsers()
        {
            try
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<User>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer usuarios.json: " + ex.Message);
                return new List<User>();
            }

        }

        public static void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static List<Group> LoadGroups()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "grupos.json");
            if (!File.Exists(path)) return new List<Group>();

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Group>>(json);
        }

        public static void SaveGroups(List<Group> groups)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "grupos.json");
            string json = JsonSerializer.Serialize(groups, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

    }
}
