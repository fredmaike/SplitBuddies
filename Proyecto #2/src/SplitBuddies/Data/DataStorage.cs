using Newtonsoft.Json;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SplitBuddies.Data
{
    /// <summary>
    /// Clase estática encargada de gestionar la carga y guardado de usuarios en un archivo JSON.
    /// </summary>
    public static class DataStorage
    {
        /// <summary>
        /// Ruta del archivo donde se almacenan los datos de los usuarios.
        /// </summary>
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "usuarios.json");

        /// <summary>
        /// Carga la lista de usuarios desde el archivo JSON.
        /// Si el archivo no existe o hay error, retorna una lista vacía.
        /// </summary>
        public static List<User> LoadUsers()
        {
            try
            {
                EnsureDirectoryExists();

                if (!File.Exists(FilePath))
                    return new List<User>();

                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar usuarios: {ex.Message}");
                return new List<User>();
            }
        }

        /// <summary>
        /// Guarda la lista de usuarios en el archivo JSON con formato indentado.
        /// </summary>
        public static void SaveUsers(List<User> users)
        {
            try
            {
                EnsureDirectoryExists();

                string json = JsonConvert.SerializeObject(users ?? new List<User>(), Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar usuarios: {ex.Message}");
            }
        }

        #region Métodos privados auxiliares

        /// <summary>
        /// Asegura que la carpeta donde se guarda el archivo JSON exista.
        /// </summary>
        private static void EnsureDirectoryExists()
        {
            string directory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        #endregion
    }
}
