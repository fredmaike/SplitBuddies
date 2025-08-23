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
    /// Singleton que gestiona la carga y almacenamiento de usuarios, grupos, gastos e invitaciones.
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

        // --------- Ruta base ---------
        public string BasePath { get; set; } = "";

        // --------- Listas en memoria (solo lectura pública) ---------
        public List<User> Users { get; private set; } = new List<User>();
        public List<Group> Groups { get; private set; } = new List<Group>();
        public List<Expense> Expenses { get; private set; } = new List<Expense>();
        public List<Invitation> Invitations { get; private set; } = new List<Invitation>();

        private DataManager() { }

        #region CARGA DE DATOS

        /// <summary>
        /// Carga todos los usuarios desde su archivo JSON.
        /// </summary>
        public void LoadUsers() => Users = CargarDesdeArchivo<User>(USERS_FILE);

        /// <summary>
        /// Carga todos los grupos desde su archivo JSON.
        /// </summary>
        public void LoadGroups() => Groups = CargarDesdeArchivo<Group>(GROUPS_FILE);

        /// <summary>
        /// Carga todos los gastos desde su archivo JSON.
        /// </summary>
        public void LoadExpenses() => Expenses = CargarDesdeArchivo<Expense>(EXPENSES_FILE);

    

        /// <summary>
        /// Lee y deserializa una lista de objetos desde un archivo JSON.
        /// Si hay error, retorna lista vacía y muestra mensaje.
        /// </summary>
        private List<T> CargarDesdeArchivo<T>(string fileName)
        {
            string path = Path.Combine(BasePath, fileName);
            if (!File.Exists(path)) return new List<T>();

            try
            {
                var json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer {fileName}: {ex.Message}\nSe cargará una lista vacía.",
                                "Error de lectura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<T>();
            }
        }

        #endregion

        #region GUARDADO DE DATOS

        public void SaveUsers() => GuardarEnArchivo(Users, USERS_FILE);
        public void SaveGroups() => GuardarEnArchivo(Groups, GROUPS_FILE);
        public void SaveExpenses() => GuardarEnArchivo(Expenses, EXPENSES_FILE);

        /// <summary>
        /// Serializa y guarda una lista de objetos en un archivo JSON.
        /// </summary>
        private void GuardarEnArchivo<T>(List<T> data, string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);

            try
            {
                var json = JsonConvert.SerializeObject(data ?? new List<T>(), Formatting.Indented);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar {fileName}: {ex.Message}",
                                "Error de guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region GENERACIÓN DE IDS

        /// <summary>
        /// Obtiene el siguiente ID disponible para un gasto.
        /// </summary>
        public int GetNextExpenseId()
        {
            return Expenses.Max(e => e.Id) + 1; // Esto falla si Expenses está vacío
        }

        /// <summary>
        /// Obtiene el siguiente ID disponible para una invitación.
        /// </summary>
        public int GetNextInvitationId()
        {
            return (Invitations != null && Invitations.Count > 0) ?
                Invitations.Max(i => i.InvitationId) + 1
                : 1; 
        }

        #endregion

        #region UTILITARIOS

        /// <summary>
        /// Asegura que la ruta base exista; si no, la crea.
        /// </summary>
        private void EnsureBasePathExists()
        {
            if (!string.IsNullOrWhiteSpace(BasePath) && !Directory.Exists(BasePath))
                Directory.CreateDirectory(BasePath);
        }

        #endregion
    }
}
