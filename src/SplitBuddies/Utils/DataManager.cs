using Newtonsoft.Json;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SplitBuddies.Data
{
    // Singleton encargado de gestionar la carga y guardado de datos en la aplicación,
    // incluyendo usuarios, grupos y gastos, desde y hacia archivos JSON.
    public class DataManager
    {
        // Instancia única del DataManager (patrón Singleton)
        private static DataManager instance;
        public static DataManager Instance => instance ??= new DataManager();

        // Ruta base donde se almacenan los archivos JSON
        public string BasePath { get; set; } = "";

        // Listas en memoria de los datos cargados
        public List<User> Users { get; private set; } = new List<User>();
        public List<Group> Groups { get; private set; } = new List<Group>();
        public List<Expense> Expenses { get; private set; } = new List<Expense>();

        // Carga los usuarios desde el archivo JSON correspondiente, o crea una lista vacía si no existe el archivo
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

        // Carga los grupos desde el archivo JSON correspondiente, o crea una lista vacía si no existe el archivo
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

        // Carga los gastos desde el archivo JSON correspondiente, o crea una lista vacía si no existe el archivo
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

        // Guarda la lista de usuarios en un archivo JSON, mostrando mensajes de estado y capturando excepciones
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

        // Guarda la lista de grupos en un archivo JSON. Asegura que la carpeta base exista.
        public void SaveGroups(string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            var json = JsonConvert.SerializeObject(Groups, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        // Guarda la lista de gastos en un archivo JSON. Asegura que la carpeta base exista.
        public void SaveExpenses(string fileName)
        {
            EnsureBasePathExists();
            string path = Path.Combine(BasePath, fileName);
            var json = JsonConvert.SerializeObject(Expenses, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        // Verifica que la ruta base exista; si no, la crea para evitar errores al guardar archivos
        private void EnsureBasePathExists()
        {
            if (!string.IsNullOrWhiteSpace(BasePath) && !Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
        }
    }
}
