using Newtonsoft.Json;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SplitBuddies.Data
{
    /// <summary>
    /// Singleton que maneja la carga y almacenamiento de usuarios, grupos, gastos e invitaciones.
    /// </summary>
    public class DataManager
    {
        // --------- Singleton ---------
        private static DataManager instance;
        public static DataManager Instance => instance ??= new DataManager();

        // --------- Archivos por defecto ---------
        private const string USERS_FILE = "usuarios.json";
        private const string GROUPS_FILE = "grupos.json";
        private const string EXPENSES_FILE = "gastos.json";
        private const string INVITATIONS_FILE = "invitations.json";

        // --------- Ruta base ---------
        public string BasePath { get; set; } = "";

        // --------- Listas en memoria ---------
        public List<User> Users { get; private set; } = new List<User>();
        public List<Group> Groups { get; private set; } = new List<Group>();
        public List<Expense> Expenses { get; private set; } = new List<Expense>();
        public List<Invitation> Invitations { get; private set; } = new List<Invitation>();

        private DataManager() { }

        // ================== CARGA ==================
        public void LoadUsers() => Users = LoadFromFile<User>(USERS_FILE);
        public void LoadGroups() => Groups = LoadFromFile<Group>(GROUPS_FILE);
        public void LoadExpenses() => Expenses = LoadFromFile<Expense>(EXPENSES_FILE);
        public void LoadInvitations() => Invitations = LoadFromFile<Invitation>(INVITATIONS_FILE);

        private List<T> LoadFromFile<T>(string fileName)
        {
            string path = Path.Combine(BasePath, fileName);
            if (File.Exists(path))
            {
                try
                {
                    var json = File.ReadAllText(path);
                    return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
                }
                catch
                {
                    MessageBox.Show($"Error al leer {fileName}. Se cargará lista vacía.");
                }
            }
            return new List<T>();
        }

        // ================== GUARDADO ==================
        public void SaveUsers() => SaveToFile(Users, USERS_FILE);
        public void SaveGroups() => SaveToFile(Groups, GROUPS_FILE);
        public void SaveExpenses() => SaveToFile(Expenses, EXPENSES_FILE);
        public void SaveInvitations() => SaveToFile(Invitations, INVITATIONS_FILE);

        private void SaveToFile<T>(List<T> data, string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            try
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar {fileName}: {ex.Message}");
            }
        }

        // ================== ID AUTO-INCREMENT ==================
        public int GetNextExpenseId()
        {
            return Expenses.Any() ? Expenses.Max(e => e.Id) + 1 : 1;
        }

        public int GetNextInvitationId()
        {
            return Invitations.Any() ? Invitations.Max(i => i.InvitationId) + 1 : 1;
        }

        // ================== UTILITARIOS ==================
        private void EnsureBasePathExists()
        {
            if (!string.IsNullOrWhiteSpace(BasePath) && !Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath);
        }
    }
}
