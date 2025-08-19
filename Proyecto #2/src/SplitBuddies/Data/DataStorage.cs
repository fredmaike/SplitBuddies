using Newtonsoft.Json;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace SplitBuddies.Data
{
    // Clase estática encargada de gestionar la carga y guardado de usuarios en un archivo JSON.
    public static class DataStorage
    {
        // Ruta del archivo donde se almacenan los datos de los usuarios
        private static string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "usuarios.json");

        // Carga la lista de usuarios desde el archivo JSON.
        // Si el archivo no existe, retorna una lista vacía.
        public static List<User> LoadUsers()
        {
            if (!File.Exists(filePath))
                return new List<User>();

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }

        // Guarda la lista de usuarios en el archivo JSON con formato legible.
        public static void SaveUsers(List<User> usuarios)
        {
            string json = JsonConvert.SerializeObject(usuarios, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}
