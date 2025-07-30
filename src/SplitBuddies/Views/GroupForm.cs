using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SplitBuddies.Controllers;
using SplitBuddies.Models;
using SplitBuddies.Data;
using System.Drawing;
using System.IO;


namespace SplitBuddies.Views
{
    public partial class GroupForm : Form
    {
        private GroupController groupController;
        private User currentUser;

        public GroupForm(User user)
        {
            currentUser = user;
            InitializeComponent();
            groupController = new GroupController(DataManager.Instance.Groups);
        }

    
        private void GroupForm_Load(object sender, EventArgs e)
        {
            LoadGroups();
        }

        private void LoadGroups()
        {
            var userGroups = groupController.GetGroupsForUser(currentUser.Email);
            MessageBox.Show($"Grupos para usuario {currentUser.Email}: {userGroups.Count}");

            lstGrupos.DataSource = null;
            lstGrupos.DataSource = userGroups;
            lstGrupos.DisplayMember = "GroupName";
        }


     
        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGroupName.Text))
            {
                MessageBox.Show("Ingrese un nombre para el grupo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            groupController.CreateGroup(
                txtGroupName.Text,
                txtImagePath.Text,
                new List<string> { currentUser.Email }
            );

            DataManager.Instance.SaveGroups("grupos.json");
            LoadGroups();

            MessageBox.Show("Grupo creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un grupo para eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedGroup = (Group)lstGrupos.SelectedItem;

            var confirm = MessageBox.Show(
                $"¿Está seguro de eliminar el grupo '{selectedGroup.GroupName}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                groupController.DeleteGroup(selectedGroup.GroupId);
                DataManager.Instance.SaveGroups("grupos.json");
                LoadGroups();
                pbGroupImage.Image = null; 
                MessageBox.Show("Grupo eliminado correctamente.", "Éxito");
            }
        }

        private readonly string baseImagePath = @"E:\SplitBuddies-master\SplitBuddies-master\src\SplitBuddies";

        private void lstGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is Group selectedGroup && selectedGroup != null)
            {
                try
                {
                    string basePath = @"E:\SplitBuddies-master\SplitBuddies-master\src\SplitBuddies\";
                    string imagePath = Path.Combine(basePath, selectedGroup.IMAGE?.Replace('/', Path.DirectorySeparatorChar) ?? "");

                    if (!string.IsNullOrEmpty(selectedGroup.IMAGE) && File.Exists(imagePath))
                    {
                        pbGroupImage.Image?.Dispose();  
                        pbGroupImage.Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        pbGroupImage.Image?.Dispose();
                        pbGroupImage.Image = null;
                    }
                }
                catch
                {
                    pbGroupImage.Image?.Dispose();
                    pbGroupImage.Image = null;
                }
            }
            else
            {
                pbGroupImage.Image?.Dispose();
                pbGroupImage.Image = null;
            }
        }
    }
}
         

