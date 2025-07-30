using SplitBuddies.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SplitBuddies.Services
{
    // Servicio estático encargado de guardar y cargar información de usuarios
    // en formato JSON desde el sistema de archivos.
    public static class JsonDataService
    {
        // Ruta relativa del archivo JSON donde se almacenan los usuarios
        private const string UserFile = @"Data\users.json";

        // Guarda la lista de usuarios en el archivo JSON con formato indentado
        public static void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(UserFile, json);
        }

        // Carga y devuelve la lista de usuarios desde el archivo JSON.
        // Si el archivo no existe, retorna una lista vacía.
        public static List<User> LoadUsers()
        {
            if (!File.Exists(UserFile)) return new List<User>();
            var json = File.ReadAllText(UserFile);
            return JsonSerializer.Deserialize<List<User>>(json);
        }
    }
}
