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

        // Constructor recibe el usuario actual y configura el controlador y la UI
        public GroupForm(User user)
        {
            currentUser = user;
            InitializeComponent();
            groupController = new GroupController(DataManager.Instance.Groups);
        }

        // Evento Load del formulario: carga los grupos al iniciar
        private void GroupForm_Load(object sender, EventArgs e)
        {
            LoadGroups();
        }

        // Carga los grupos del usuario en la lista lstGrupos
        private void LoadGroups()
        {
            var userGroups = groupController.GetGroupsForUser(currentUser.Email);

            lstGrupos.DataSource = null;
            lstGrupos.DataSource = userGroups;
            lstGrupos.DisplayMember = "GroupName";
        }

        // Evento click para crear un nuevo grupo
        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            // Validar que el nombre no esté vacío
            if (string.IsNullOrWhiteSpace(txtGroupName.Text))
            {
                MessageBox.Show("Ingrese un nombre para el grupo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear el grupo con el nombre, imagen y con el usuario actual como miembro
            groupController.CreateGroup(
                txtGroupName.Text,
                txtImagePath.Text,
                new List<string> { currentUser.Email }
            );

            // Guardar los grupos y recargar la lista
            DataManager.Instance.SaveGroups("grupos.json");
            LoadGroups();

            MessageBox.Show("Grupo creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Evento click para eliminar el grupo seleccionado
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un grupo para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedGroup = (Group)lstGrupos.SelectedItem;

            // Confirmación de eliminación
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

        // Evento cuando se selecciona un grupo de la lista
        private void LstGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is Group selectedGroup)
            {
                // Mostrar imagen, nombre y ruta de imagen del grupo seleccionado
                MostrarImagenGrupo(selectedGroup);
                txtGroupName.Text = selectedGroup.GroupName;
                txtImagePath.Text = selectedGroup.IMAGE;
            }
            else
            {
                // Limpiar campos si no hay grupo seleccionado
                LimpiarImagen();
                txtGroupName.Clear();
                txtImagePath.Clear();
            }
        }

        // Método para mostrar la imagen del grupo en el PictureBox
        private void MostrarImagenGrupo(Group grupo)
        {
            try
            {
                string imagePath = ObtenerRutaImagen(grupo);
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    // Usar MemoryStream para evitar bloqueo de archivo
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
                // En caso de error, limpiar la imagen
                LimpiarImagen();
            }
        }

        // Obtiene la ruta completa de la imagen del grupo en la carpeta IMAGE
        private static string ObtenerRutaImagen(Group grupo)
        {
            if (grupo == null || string.IsNullOrWhiteSpace(grupo.IMAGE))
                return null;

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IMAGE");
            // Reemplazar barras para compatibilidad con SO
            return Path.Combine(basePath, grupo.IMAGE.Replace('/', Path.DirectorySeparatorChar));
        }

        // Limpia la imagen del PictureBox liberando recursos
        private void LimpiarImagen()
        {
            pbGroupImage.Image?.Dispose();
            pbGroupImage.Image = null;
        }

        // Evento para seleccionar una imagen usando OpenFileDialog
        private void BtnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Seleccionar imagen del grupo";
                ofd.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = ofd.FileName;

                    // Carpeta donde se guardan las imágenes localmente
                    string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IMAGE");
                    Directory.CreateDirectory(imageFolder);

                    string fileName = Path.GetFileName(selectedPath);
                    string destinationPath = Path.Combine(imageFolder, fileName);

                    // Copiar imagen solo si no existe para evitar sobreescritura
                    if (!File.Exists(destinationPath))
                    {
                        File.Copy(selectedPath, destinationPath);
                    }

                    // Actualizar la ruta y vista previa en la UI
                    txtImagePath.Text = fileName;
                    MostrarImagenGrupo(new Group { IMAGE = fileName });
                }
            }
        }
    }
}
