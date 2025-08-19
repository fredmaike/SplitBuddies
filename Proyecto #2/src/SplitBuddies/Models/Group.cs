using Newtonsoft.Json;
using System.Collections.Generic;

namespace SplitBuddies.Models
{
    // Representa un grupo dentro de la aplicación SplitBuddies.
    // Cada grupo tiene un nombre, una imagen, una lista de miembros y una lista de gastos asociados.
    public class Group
    {
        // Identificador único del grupo
        public int GroupId { get; set; }

        // Nombre del grupo 
        public string GroupName { get; set; }

        // Ruta de la imagen asociada al grupo 
        // Esta propiedad se serializa/deserializa como "IMAGE" en el archivo JSON
        [JsonProperty("IMAGE")]
        public string IMAGE { get; set; }

        // Lista de correos electrónicos de los miembros del grupo
        public List<string> Members { get; set; }

        // Lista de IDs de gastos asociados a este grupo
        public List<int> Expenses { get; set; } = new List<int>();
    }
}
