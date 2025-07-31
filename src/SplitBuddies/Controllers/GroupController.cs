using System.Collections.Generic;
using System.Linq;
using SplitBuddies.Models;

namespace SplitBuddies.Controllers
{
    /// <summary>
    /// Controlador responsable de gestionar operaciones relacionadas con grupos,
    /// como crear, eliminar y obtener grupos por usuario.
    /// </summary>
    public class GroupController
    {
        /// <summary>
        /// Lista de grupos que este controlador maneja.
        /// </summary>
        private readonly List<Group> groups;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="GroupController"/> con la lista de grupos existente.
        /// </summary>
        /// <param name="groups">Lista de grupos existentes.</param>
        public GroupController(List<Group> groups)
        {
            this.groups = groups;
        }

        /// <summary>
        /// Retorna todos los grupos a los que pertenece un usuario según su correo electrónico.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <returns>Lista de grupos en los que el usuario es miembro.</returns>
        public List<Group> GetGroupsForUser(string email)
        {
            return groups.Where(g => g.Members.Contains(email)).ToList();
        }

        /// <summary>
        /// Crea un nuevo grupo con nombre, imagen y lista de miembros, asignando un ID único.
        /// </summary>
        /// <param name="name">Nombre del nuevo grupo.</param>
        /// <param name="imagePath">Ruta de la imagen asociada al grupo.</param>
        /// <param name="memberEmails">Lista de correos electrónicos de los miembros.</param>
        public void CreateGroup(string name, string imagePath, List<string> memberEmails)
        {
            var newGroup = new Group
            {
                // Asignar un ID único al nuevo grupo
                GroupId = groups.Count > 0 ? groups.Max(g => g.GroupId) + 1 : 1,

                GroupName = name,
                IMAGE = imagePath,
                Members = memberEmails,
                Expenses = new List<int>() // Se inicia sin gastos
            };

            // Agregar el nuevo grupo a la lista de grupos
            groups.Add(newGroup);
        }

        /// <summary>
        /// Elimina un grupo por su ID si existe.
        /// </summary>
        /// <param name="groupId">ID del grupo a eliminar.</param>
        public void DeleteGroup(int groupId)
        {
            var groupToRemove = groups.FirstOrDefault(g => g.GroupId == groupId);
            if (groupToRemove != null)
            {
                groups.Remove(groupToRemove);
            }
        }
    }
}
