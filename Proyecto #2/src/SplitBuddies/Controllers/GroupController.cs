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
        /// Lista de grupos gestionados por el controlador.
        /// </summary>
        private readonly List<Group> _groups;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="GroupController"/> con la lista de grupos existente.
        /// </summary>
        /// <param name="groups">Lista de grupos existentes.</param>
        public GroupController(List<Group> groups)
        {
            _groups = groups ?? new List<Group>();
        }

        /// <summary>
        /// Retorna todos los grupos a los que pertenece un usuario según su correo electrónico.
        /// </summary>
        /// <param name="email">Correo electrónico del usuario.</param>
        /// <returns>Lista de grupos en los que el usuario es miembro.</returns>
        public List<Group> GetGroupsForUser(string email)
        {
            return _groups
                .Where(group => group.Members.Contains(email))
                .ToList();
        }

        /// <summary>
        /// Crea un nuevo grupo con nombre, imagen y lista de miembros, asignando un ID único.
        /// </summary>
        /// <param name="name">Nombre del nuevo grupo.</param>
        /// <param name="imagePath">Ruta de la imagen asociada al grupo.</param>
        /// <param name="memberEmails">Lista de correos electrónicos de los miembros.</param>
        /// <returns>El grupo creado.</returns>
        public Group CreateGroup(string name, string imagePath, List<string> memberEmails)
        {
            var newGroup = new Group
            {
                GroupId = GenerateNextGroupId(),
                GroupName = name,
                IMAGE = imagePath,
                Members = memberEmails ?? new List<string>(),
                Expenses = new List<int>()
            };

            _groups.Add(newGroup);
            return newGroup;
        }

        /// <summary>
        /// Elimina un grupo por su ID si existe.
        /// </summary>
        /// <param name="groupId">ID del grupo a eliminar.</param>
        /// <returns>True si el grupo fue eliminado, false en caso contrario.</returns>
        public bool DeleteGroup(int groupId)
        {
            var groupToRemove = _groups.FirstOrDefault(g => g.GroupId == groupId);
            if (groupToRemove == null) return false;

            _groups.Remove(groupToRemove);
            return true;
        }

        #region Métodos privados auxiliares

        /// <summary>
        /// Genera un nuevo ID único para un grupo.
        /// </summary>
        /// <returns>Un ID único.</returns>
        private int GenerateNextGroupId()
        {
            return _groups.Count > 0 ? _groups.Max(g => g.GroupId) + 1 : 1;
        }

        #endregion
    }
}
