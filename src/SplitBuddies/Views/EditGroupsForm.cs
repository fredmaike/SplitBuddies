using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public partial class EditGroupsForm : Form
    {
        private List<Group> grupos;
        private string jsonPath = "Data/grupos.json";

        public EditGroupsForm()
        {
            InitializeComponent();
            LoadGroups();
        }

        private void LoadGroups()
        {
            if (File.Exists(jsonPath))
            {
                string json = File.ReadAllText(jsonPath);
                grupos = JsonConvert.DeserializeObject<List<Group>>(json) ?? new List<Group>();
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
                grupos[index].Members = txtMembers.Text.Split(',').Select(m => m.Trim()).ToList();

                string json = JsonConvert.SerializeObject(grupos, Formatting.Indented);
                File.WriteAllText(jsonPath, json);


                MessageBox.Show("Grupo actualizado correctamente.");
                LoadGroups();
            }
        }
    }
}