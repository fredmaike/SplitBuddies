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
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "usuarios.json");

        /// <summary>
        /// Carga la lista de usuarios desde el archivo JSON.
        /// Si el archivo no existe, retorna una lista vacía.
        /// </summary>
        /// <returns>Lista de usuarios cargados desde el archivo JSON.</returns>
        public static List<User> LoadUsers()
        {
            if (!File.Exists(filePath))
                return new List<User>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        /// <summary>
        /// Guarda la lista de usuarios en el archivo JSON con formato indentado para facilitar su lectura.
        /// </summary>
        /// <param name="usuarios">Lista de usuarios a guardar.</param>
        public static void SaveUsers(List<User> usuarios)
        {
            string json = JsonConvert.SerializeObject(usuarios, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
