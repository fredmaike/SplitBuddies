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
    /// Clase singleton que maneja la carga y almacenamiento de datos 
    /// para usuarios, grupos, gastos e invitaciones en archivos JSON.
    /// </summary>
    public class DataManager
    {
        // --------- Singleton ---------
        private static DataManager instance;
        public static DataManager Instance => instance ??= new DataManager();

        // --------- Archivos por defecto ---------
        private const string USERS_FILE        = "usuarios.json";
        private const string GROUPS_FILE       = "grupos.json";
        private const string EXPENSES_FILE     = "gastos.json";
        private const string INVITATIONS_FILE  = "invitations.json";

        /// <summary>
        /// Ruta base donde se almacenan los archivos JSON.
        /// </summary>
        public string BasePath { get; set; } = "";

        /// <summary>
        /// Lista en memoria con todos los usuarios cargados.
        /// </summary>
        public List<User> Users { get; private set; } = new List<User>();

        /// <summary>
        /// Lista en memoria con todos los grupos cargados.
        /// </summary>
        public List<Group> Groups { get; private set; } = new List<Group>();

        /// <summary>
        /// Lista en memoria con todos los gastos cargados.
        /// </summary>
        public List<Expense> Expenses { get; private set; } = new List<Expense>();

        /// <summary>
        /// Lista en memoria con todas las invitaciones cargadas.
        /// </summary>
        public List<Invitation> Invitations { get; private set; } = new List<Invitation>();

        // ========= CARGA =========

        /// <summary>
        /// Carga los usuarios desde BasePath/usuarios.json (o lista vacía si no existe).
        /// </summary>
        public void LoadUsers()
        {
            string path = Path.Combine(BasePath, USERS_FILE);
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

        /// <summary>
        /// Carga los grupos desde BasePath/grupos.json (o lista vacía si no existe).
        /// </summary>
        public void LoadGroups()
        {
            string path = Path.Combine(BasePath, GROUPS_FILE);
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

        /// <summary>
        /// Carga los gastos desde BasePath/gastos.json (o lista vacía si no existe).
        /// </summary>
        public void LoadExpenses()
        {
            string path = Path.Combine(BasePath, EXPENSES_FILE);
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

        /// <summary>
        /// Carga las invitaciones desde BasePath/invitations.json (o lista vacía si no existe).
        /// </summary>
        public void LoadInvitations()
        {
            string path = Path.Combine(BasePath, INVITATIONS_FILE);
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                Invitations = JsonConvert.DeserializeObject<List<Invitation>>(json) ?? new List<Invitation>();
            }
            else
            {
                Invitations = new List<Invitation>();
            }
        }

        // ========= GUARDADO (con nombre explícito) =========

        /// <summary>
        /// Guarda la lista de usuarios en el archivo indicado dentro de BasePath.
        /// </summary>
        /// <param name="fileName">Nombre del archivo destino (p. ej. usuarios.json)</param>
        public void SaveUsers(string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            try
            {
                // Este MessageBox estaba en tu versión original; lo dejo para mantener comportamiento.
                MessageBox.Show($"Guardando usuarios en: {path}");
                var json = JsonConvert.SerializeObject(Users, Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar usuarios: {ex.Message}");
            }
        }

        /// <summary>
        /// Guarda la lista de grupos en el archivo indicado dentro de BasePath.
        /// </summary>
        public void SaveGroups(string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            var json = JsonConvert.SerializeObject(Groups, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Guarda la lista de gastos en el archivo indicado dentro de BasePath.
        /// </summary>
        public void SaveExpenses(string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            var json = JsonConvert.SerializeObject(Expenses, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Guarda la lista de invitaciones en el archivo indicado dentro de BasePath.
        /// </summary>
        public void SaveInvitations(string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            var json = JsonConvert.SerializeObject(Invitations, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        // ========= GUARDADO (overloads sin parámetros, usan nombres por defecto) =========

        public void SaveUsers()        => SaveUsers(USERS_FILE);
        public void SaveGroups()       => SaveGroups(GROUPS_FILE);
        public void SaveExpenses()     => SaveExpenses(EXPENSES_FILE);
        public void SaveInvitations()  => SaveInvitations(INVITATIONS_FILE);

        // ========= UTILITARIOS =========

        /// <summary>
        /// Genera un nuevo ID incremental para gastos.
        /// </summary>
        public int GetNextExpenseId()
        {
            return (Expenses.Count == 0) ? 1 : Expenses.Max(e => e.Id) + 1;
        }

        /// <summary>
        /// Genera un nuevo ID incremental para invitaciones.
        /// </summary>
        public int GetNextInvitationId()
        {
            return (Invitations.Count == 0) ? 1 : Invitations.Max(i => i.InvitationId) + 1;
        }

        /// <summary>
        /// Verifica que la carpeta BasePath exista; si no, la crea.
        /// </summary>
        private void EnsureBasePathExists()
        {
            if (!string.IsNullOrWhiteSpace(BasePath) && !Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
        }
    }
}
