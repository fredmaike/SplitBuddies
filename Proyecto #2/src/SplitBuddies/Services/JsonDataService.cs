using SplitBuddies.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SplitBuddies.Services
{
    /// <summary>
    /// Servicio estático encargado de guardar y cargar información de usuarios
    /// en formato JSON desde el sistema de archivos.
    /// </summary>
    public static class JsonDataService
    {
        /// <summary>
        /// Ruta relativa del archivo JSON donde se almacenan los usuarios.
        /// </summary>
        private const string UserFile = @"Data\users.json";

        /// <summary>
        /// Guarda la lista de usuarios en el archivo JSON con formato indentado para mejor legibilidad.
        /// </summary>
        /// <param name="users">Lista de usuarios a guardar.</param>
        public static void SaveUsers(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(UserFile, json);
        }

        /// <summary>
        /// Carga y devuelve la lista de usuarios desde el archivo JSON.
        /// Si el archivo no existe, retorna una lista vacía.
        /// </summary>
        /// <returns>Lista de usuarios cargados desde el archivo JSON.</returns>
        public static List<User> LoadUsers()
        {
            if (!File.Exists(UserFile))
                return new List<User>();

            var json = File.ReadAllText(UserFile);
            return JsonSerializer.Deserialize<List<User>>(json);
        }
    }
}
