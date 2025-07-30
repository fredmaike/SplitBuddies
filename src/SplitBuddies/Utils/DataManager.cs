using Newtonsoft.Json;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SplitBuddies.Data
{
    public class DataManager
    {
        private static DataManager instance;
        public static DataManager Instance => instance ??= new DataManager();

        public string BasePath { get; set; } = "";

        public List<User> Users { get; private set; } = new List<User>();
        public List<Group> Groups { get; private set; } = new List<Group>();
        public List<Expense> Expenses { get; private set; } = new List<Expense>();

        public void LoadUsers()
        {
            string path = Path.Combine(BasePath, "usuarios.json");
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                Users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            }
            else
            {
                Users = new List<User>();
            }
        }

        public void LoadGroups()
        {
            string path = Path.Combine(BasePath, "grupos.json");
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                Groups = JsonConvert.DeserializeObject<List<Group>>(json) ?? new List<Group>();
            }
            else
            {
                Groups = new List<Group>();
            }
        }

        public void LoadExpenses()
        {
            string path = Path.Combine(BasePath, "gastos.json");
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                Expenses = JsonConvert.DeserializeObject<List<Expense>>(json) ?? new List<Expense>();
            }
            else
            {
                Expenses = new List<Expense>();
            }
        }

        public void SaveUsers(string fileName)
        {
            string path = Path.Combine(BasePath, fileName);
            try
            {
                MessageBox.Show($"Guardando usuarios en: {path}"); 
                var json = JsonConvert.SerializeObject(Users, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar usuarios: {ex.Message}");
            }
        }

        public void SaveGroups(string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            var json = JsonConvert.SerializeObject(Groups, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public void SaveExpenses(string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            var json = JsonConvert.SerializeObject(Expenses, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        private void EnsureBasePathExists()
        {
            if (!string.IsNullOrWhiteSpace(BasePath) && !Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
        }
    }
}
