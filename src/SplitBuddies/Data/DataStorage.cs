using Newtonsoft.Json;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SplitBuddies.Data
{
    public static class DataStorage
    {
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "usuarios.json");

        public static List<User> LoadUsers()
        {
            if (!File.Exists(filePath))
                return new List<User>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        public static void SaveUsers(List<User> usuarios)
        {
            string json = JsonConvert.SerializeObject(usuarios, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}