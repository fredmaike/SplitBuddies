using Newtonsoft.Json;
using System.Collections.Generic;

namespace SplitBuddies.Models
{
    /// <summary>
    /// Representa un grupo dentro de la aplicación SplitBuddies.
    /// Cada grupo tiene un nombre, una imagen, una lista de miembros y una lista de gastos asociados.
    /// </summary>
    public class Group
    {
        /// <summary>
        /// Identificador único del grupo.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Nombre del grupo.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Ruta de la imagen asociada al grupo.
        /// Se serializa y deserializa con la clave "IMAGE" en JSON.
        /// </summary>
        [JsonProperty("IMAGE")]
        public string IMAGE { get; set; }

        /// <summary>
        /// Lista de correos electrónicos de los miembros que pertenecen al grupo.
        /// </summary>
        public List<string> Members { get; set; }

        /// <summary>
        /// Lista de IDs de gastos asociados a este grupo.
        /// </summary>
        public List<int> Expenses { get; set; } = new List<int>();
    }
}
