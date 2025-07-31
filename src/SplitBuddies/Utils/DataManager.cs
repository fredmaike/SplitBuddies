using Newtonsoft.Json;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SplitBuddies.Data
{
    /// <summary>
    /// Clase singleton que maneja la carga y almacenamiento de datos 
    /// para usuarios, grupos y gastos en archivos JSON.
    /// </summary>
    public class DataManager
    {
        // Instancia estática única para implementar el patrón singleton
        private static DataManager instance;

        /// <summary>
        /// Propiedad para acceder a la instancia singleton de DataManager
        /// </summary>
        public static DataManager Instance => instance ??= new DataManager();

        /// <summary>
        /// Ruta base donde se almacenan los archivos JSON
        /// </summary>
        public string BasePath { get; set; } = "";

        /// <summary>
        /// Lista en memoria con todos los usuarios cargados
        /// </summary>
        public List<User> Users { get; private set; } = new List<User>();

        /// <summary>
        /// Lista en memoria con todos los grupos cargados
        /// </summary>
        public List<Group> Groups { get; private set; } = new List<Group>();

        /// <summary>
        /// Lista en memoria con todos los gastos cargados
        /// </summary>
        public List<Expense> Expenses { get; private set; } = new List<Expense>();

        /// <summary>
        /// Carga los usuarios desde el archivo usuarios.json ubicado en BasePath.
        /// Si el archivo no existe, inicializa la lista vacía.
        /// </summary>
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

        /// <summary>
        /// Carga los grupos desde el archivo grupos.json ubicado en BasePath.
        /// Si el archivo no existe, inicializa la lista vacía.
        /// </summary>
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

        /// <summary>
        /// Carga los gastos desde el archivo gastos.json ubicado en BasePath.
        /// Si el archivo no existe, inicializa la lista vacía.
        /// </summary>
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

        /// <summary>
        /// Guarda la lista de usuarios en el archivo indicado dentro de BasePath.
        /// Maneja excepciones mostrando un mensaje de error.
        /// </summary>
        /// <param name="fileName">Nombre del archivo donde se guardarán los usuarios</param>
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

        /// <summary>
        /// Guarda la lista de grupos en el archivo indicado dentro de BasePath.
        /// Crea la carpeta BasePath si no existe.
        /// </summary>
        /// <param name="fileName">Nombre del archivo donde se guardarán los grupos</param>
        public void SaveGroups(string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            var json = JsonConvert.SerializeObject(Groups, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Guarda la lista de gastos en el archivo indicado dentro de BasePath.
        /// Crea la carpeta BasePath si no existe.
        /// </summary>
        /// <param name="fileName">Nombre del archivo donde se guardarán los gastos</param>
        public void SaveExpenses(string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            var json = JsonConvert.SerializeObject(Expenses, Formatting.Indented);
            File.WriteAllText(path, json);
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
