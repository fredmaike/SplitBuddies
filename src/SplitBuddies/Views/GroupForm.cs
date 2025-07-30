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
                MessageBox.Show("Seleccione un grupo para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                LimpiarImagen();
                MessageBox.Show("Grupo eliminado correctamente.", "Éxito");
            }
        }

        private void LstGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is Group selectedGroup)
            {
                MostrarImagenGrupo(selectedGroup);
                txtGroupName.Text = selectedGroup.GroupName;
                txtImagePath.Text = selectedGroup.IMAGE;
            }
            else
            {
                LimpiarImagen();
                txtGroupName.Clear();
                txtImagePath.Clear();
            }
        }

        private void MostrarImagenGrupo(Group grupo)
        {
            try
            {
                string imagePath = ObtenerRutaImagen(grupo);
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    using (var ms = new MemoryStream(File.ReadAllBytes(imagePath)))
                    {
                        var imgTemp = Image.FromStream(ms);
                        pbGroupImage.Image?.Dispose();
                        pbGroupImage.Image = new Bitmap(imgTemp);
                    }
                }
                else
                {
                    LimpiarImagen();
                }
            }
            catch
            {
                LimpiarImagen();
            }
        }

        private static string ObtenerRutaImagen(Group grupo)
        {
            if (grupo == null || string.IsNullOrWhiteSpace(grupo.IMAGE))
                return null;

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IMAGE");
            return Path.Combine(basePath, grupo.IMAGE.Replace('/', Path.DirectorySeparatorChar));
        }

        private void LimpiarImagen()
        {
            pbGroupImage.Image?.Dispose();
            pbGroupImage.Image = null;
        }

        private void BtnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar imagen del grupo";
                ofd.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = ofd.FileName;

                    string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IMAGE");
                    Directory.CreateDirectory(imageFolder);

                    string fileName = Path.GetFileName(selectedPath);
                    string destinationPath = Path.Combine(imageFolder, fileName);

                    if (!File.Exists(destinationPath))
                    {
                        File.Copy(selectedPath, destinationPath);
                    }

                    txtImagePath.Text = fileName;
                    MostrarImagenGrupo(new Group { IMAGE = fileName });
                }
            }
        }
    }
}
