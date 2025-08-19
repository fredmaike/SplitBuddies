using System.Collections.Generic;
using System.Linq;
using SplitBuddies.Models;

namespace SplitBuddies.Controllers
{
    // Controlador responsable de gestionar operaciones relacionadas con grupos,
    // como crear, eliminar y obtener grupos por usuario.
    public class GroupController
    {
        // Lista de grupos que este controlador maneja 
        private readonly List<Group> groups;

        // Constructor que recibe la lista de grupos existente
        public GroupController(List<Group> groups)
        {
            this.groups = groups;
        }

        // Retorna todos los grupos a los que pertenece un usuario según su correo
        public List<Group> GetGroupsForUser(string email)
        {
            return groups.Where(g => g.Members.Contains(email)).ToList();
        }

        // Crea un nuevo grupo con nombre, imagen y lista de miembros
        public void CreateGroup(string name, string imagePath, List<string> memberEmails)
        {
            var newGroup = new Group
            {
                // Asignar un ID único al nuevo grupo
                GroupId = groups.Count > 0 ? groups.Max(g => g.GroupId) + 1 : 1,

                GroupName = name,
                Members = memberEmails,
                Expenses = new List<int>() // Se inicia sin gastos
            };

            // Agregar el nuevo grupo a la lista de grupos
            groups.Add(newGroup);
        }

        // Elimina un grupo por su ID si existe
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
