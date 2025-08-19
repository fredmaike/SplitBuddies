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
        // Lista interna de grupos cargados desde el archivo
        private List<Group> grupos;

        // Ruta al archivo JSON donde se guardan los grupos
        private string jsonPath = "Data/grupos.json";

        // Constructor: inicializa componentes y carga los grupos desde archivo
        public EditGroupsForm()
        {
            InitializeComponent();
            LoadGroups();
        }

        // Carga los grupos desde el archivo JSON y actualiza la lista visual
        private void LoadGroups()
        {
            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);
                grupos = JsonConvert.DeserializeObject<List<Group>>(json) ?? new List<Group>();

                // Limpia y llena el ListBox con los nombres de los grupos
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

        // Evento que se dispara al cambiar la selección en el ListBox
        // Actualiza los campos de texto con los datos del grupo seleccionado
        private void listBoxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxGroups.SelectedIndex;
            if (index >= 0 && index < grupos.Count)
            {
                txtGroupName.Text = grupos[index].GroupName;
                txtMembers.Text = string.Join(", ", grupos[index].Members);
            }
        }

        // Evento que se ejecuta al hacer clic en "Guardar Cambios"
        // Actualiza el grupo seleccionado con los datos editados y guarda en archivo JSON
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            int index = listBoxGroups.SelectedIndex;
            if (index >= 0 && index < grupos.Count)
            {
                grupos[index].GroupName = txtGroupName.Text;
                grupos[index].Members = txtMembers.Text
                    .Split(',')
                    .Select(m => m.Trim())
                    .ToList();

                // Serializa y guarda la lista de grupos actualizada
                string json = JsonConvert.SerializeObject(grupos, Formatting.Indented);
                File.WriteAllText(jsonPath, json);

                MessageBox.Show("Grupo actualizado correctamente.");

                // Recarga la lista para reflejar posibles cambios
                LoadGroups();
            }
        }
    }
}
