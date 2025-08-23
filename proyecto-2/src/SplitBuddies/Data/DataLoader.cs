using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SplitBuddies.Models;

namespace SplitBuddies.Data
{
    /// <summary>
    /// Clase estática responsable de cargar y guardar datos en archivos JSON.
    /// </summary>
    public static class DataLoader
    {
        private static readonly string UserFilePath = "usuarios.json";
        private static readonly string GroupFilePath = "grupos.json";

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Carga la lista de usuarios desde el archivo JSON.
        /// </summary>
        public static List<User> LoadUsers()
        {
            return LoadFromFile<User>(UserFilePath);
        }

        /// <summary>
        /// Guarda la lista de usuarios en el archivo JSON.
        /// </summary>
        public static void SaveUsers(List<User> users)
        {
            SaveToFile(UserFilePath, users);
        }

        /// <summary>
        /// Carga la lista de grupos desde el archivo JSON.
        /// </summary>
        public static List<Group> LoadGroups()
        {
            return LoadFromFile<Group>(GroupFilePath);
        }

        /// <summary>
        /// Guarda la lista de grupos en el archivo JSON.
        /// </summary>
        public static void SaveGroups(List<Group> groups)
        {
            SaveToFile(GroupFilePath, groups);
        }

        #region Métodos privados genéricos

        private static List<T> LoadFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<T>();

            try
            {
                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(json, JsonOptions) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar datos desde {filePath}: {ex.Message}");
                return new List<T>();
            }
        }

        private static void SaveToFile<T>(string filePath, List<T> data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, JsonOptions);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar datos en {filePath}: {ex.Message}");
            }
        }

        #endregion
    }
}
