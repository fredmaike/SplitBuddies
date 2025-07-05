using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SplitBuddies.Models;
using SplitBuddies.Controllers;

namespace SplitBuddies.Views
{
    public partial class GroupForm : Form
    {
        private string imagePath = "";

        public GroupForm()
        {
            InitializeComponent();

            // Llenar la lista con usuarios
            foreach (var user in UserController.Users)
            {
                clbMembers.Items.Add(user, false);
            }
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imágenes (*.png;*.jpg)|*.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imagePath = ofd.FileName;
                picGroupImage.ImageLocation = imagePath;
            }
        }

        private void btnCreateGroup_Click_1(object sender, EventArgs e)
        {

        }

        private void GroupForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            string name = txtGroupName.Text.Trim();

            if (string.IsNullOrEmpty(name) || clbMembers.CheckedItems.Count == 0)
            {
                MessageBox.Show("Complete todos los campos y seleccione al menos un miembro.");
                return;
            }

            List<User> selectedUsers = new List<User>();
            foreach (var item in clbMembers.CheckedItems)
            {
                selectedUsers.Add((User)item);
            }

            Group newGroup = new Group
            {
                Name = name,
                ImagePath = imagePath,
                Members = selectedUsers
            };

            GroupController.AddGroup(newGroup);
            MessageBox.Show("Grupo creado correctamente.");
            this.Close();
        }

        private void btnChooseImage_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imágenes (*.png;*.jpg)|*.png;*.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imagePath = ofd.FileName;
                picGroupImage.ImageLocation = imagePath;
            }
        }
    }
}
