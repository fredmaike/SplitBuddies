using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    // Formulario para editar grupos: mostrar lista, seleccionar grupo,
    // modificar nombre y miembros, y guardar cambios en archivo JSON.
    public partial class EditGroupsForm : Form
    {
        private List<Group> grupos;
        private string jsonPath = "Data/grupos.json";
        private readonly User currentUser; // ✅ usuario logueado

        // Constructor que recibe el usuario logueado
        public EditGroupsForm(User user)
        {
            InitializeComponent();
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            LoadGroups();
        }

        // Carga solo los grupos donde participa el usuario actual
        private void LoadGroups()
        {
            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);
                var todosLosGrupos = JsonConvert.DeserializeObject<List<Group>>(json) ?? new List<Group>();

                // ✅ Filtrar solo los grupos donde el usuario está como miembro
                grupos = todosLosGrupos
                    .Where(g => g.Members != null &&
                                g.Members.Contains(currentUser.Email, StringComparer.OrdinalIgnoreCase))
                    .ToList();

                listBoxGroups.Items.Clear();
                foreach (var group in grupos)
                {
                    listBoxGroups.Items.Add(group.GroupName);
                }
            }
            else
            {
                grupos = new List<Group>();
            }
        }

        private void listBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxGroups.SelectedIndex;
            if (index >= 0 && index < grupos.Count)
            {
                txtGroupName.Text = grupos[index].GroupName;
                txtMembers.Text = string.Join(", ", grupos[index].Members);
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            int index = listBoxGroups.SelectedIndex;
            if (index >= 0 && index < grupos.Count)
            {
                grupos[index].GroupName = txtGroupName.Text;
                grupos[index].Members = txtMembers.Text
                    .Split(',')
                    .Select(m => m.Trim())
                    .Where(m => !string.IsNullOrWhiteSpace(m))
                    .ToList();

                // Guardar nuevamente TODOS los grupos (incluyendo los que no pertenecen al user)
                string jsonOriginal = File.ReadAllText(jsonPath);
                var todosLosGrupos = JsonConvert.DeserializeObject<List<Group>>(jsonOriginal) ?? new List<Group>();

                // Reemplazar solo el grupo editado dentro de la lista global
                var grupoEditado = todosLosGrupos.FirstOrDefault(g => g.GroupId == grupos[index].GroupId);
                if (grupoEditado != null)
                {
                    grupoEditado.GroupName = grupos[index].GroupName;
                    grupoEditado.Members = grupos[index].Members;
                }

                string json = JsonConvert.SerializeObject(todosLosGrupos, Formatting.Indented);
                File.WriteAllText(jsonPath, json);

                MessageBox.Show("Grupo actualizado correctamente.");

                LoadGroups(); // refrescar vista
            }
        }
    }
}
