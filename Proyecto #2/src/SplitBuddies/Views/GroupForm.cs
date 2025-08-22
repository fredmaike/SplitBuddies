using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Controllers;
using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;

namespace SplitBuddies.Views
{
    public partial class GroupForm : Form
    {
        private readonly GroupController groupController;
        private User currentUser;

        public GroupForm(User user)
        {
            InitializeComponent();

            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            if (!AppSession.IsAuthenticated && !string.IsNullOrWhiteSpace(currentUser.Email))
                AppSession.SignIn(currentUser.Email);

            EnsureDataLoaded();
            groupController = new GroupController(DataManager.Instance.Groups);

            this.Load += GroupForm_Load;
            lstGrupos.SelectedIndexChanged += LstGrupos_SelectedIndexChanged;
            btnCreateGroup.Click += btnCreateGroup_Click;
            btnDeleteGroup.Click += btnDeleteGroup_Click;

            if (this.Controls.Find("btnSelectImage", true).FirstOrDefault() is Button bImg)
                bImg.Click += BtnSelectImage_Click;

            if (this.Controls.Find("btnViewBalances", true).FirstOrDefault() is Button bView)
                bView.Click += BtnViewBalances_Click;

            if (this.Controls.Find("btnSendInvitations", true).FirstOrDefault() is Button bInvite)
                bInvite.Click += BtnSendInvitations_Click;
        }

        // ========= Infra =========
        private static void EnsureDataLoaded()
        {
            var dm = DataManager.Instance;
            dm.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dm.BasePath))
                Directory.CreateDirectory(dm.BasePath);

            dm.LoadUsers();
            dm.LoadGroups();
            dm.LoadExpenses();
            try { dm.LoadInvitations(); } catch { }
        }

        private void SaveAll()
        {
            var dm = DataManager.Instance;
            dm.SaveGroups();
            try { dm.SaveInvitations(); } catch { }
        }

        // ========= UI =========
        private void GroupForm_Load(object sender, EventArgs e)
        {
            LoadGroups();
        }

        private void LoadGroups()
        {
            var userGroups = groupController.GetGroupsForUser(currentUser.Email) ?? new List<Group>();

            lstGrupos.DataSource = null;
            lstGrupos.DataSource = userGroups;
            lstGrupos.DisplayMember = nameof(Group.GroupName);

            if (userGroups.Count == 0)
            {
                txtGroupName.Clear();
                txtImagePath.Clear();
                LimpiarImagen();
            }
        }

        // ========= Botones nuevos =========
        private void BtnViewBalances_Click(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is not Group selectedGroup)
            {
                MessageBox.Show("Seleccione un grupo.", "Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var form = new BalanceForm(selectedGroup);
            form.ShowDialog(this);
        }

        private void BtnSendInvitations_Click(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is not Group selectedGroup)
            {
                MessageBox.Show("Seleccione un grupo.", "Invitación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new InvitationForm(selectedGroup, currentUser);
            form.ShowDialog(this);
        }

        // ========= Crear grupo =========
        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            string name = (txtGroupName?.Text ?? string.Empty).Trim();
            string imageName = (txtImagePath?.Text ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Ingrese un nombre para el grupo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool exists = DataManager.Instance.Groups
                .Where(g => (g.Members ?? new List<string>()).Any(m => string.Equals(m, currentUser.Email, StringComparison.OrdinalIgnoreCase)))
                .Any(g => string.Equals(g.GroupName, name, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                MessageBox.Show("Ya existe un grupo con ese nombre para este usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrWhiteSpace(imageName) && !IsValidImageName(imageName))
            {
                MessageBox.Show("El nombre de la imagen no es válido. Use .jpg/.jpeg/.png", "Imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var members = new List<string> { currentUser.Email };
            groupController.CreateGroup(name, imageName, members);

            SaveAll();
            LoadGroups();

            MessageBox.Show("Grupo creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ========= Eliminar grupo =========
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is not Group selectedGroup)
            {
                MessageBox.Show("Seleccione un grupo para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"¿Está seguro de eliminar el grupo '{selectedGroup.GroupName}'?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                groupController.DeleteGroup(selectedGroup.GroupId);

                SaveAll();
                LoadGroups();
                LimpiarImagen();

                MessageBox.Show("Grupo eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ========= Imagen =========
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

                    try
                    {
                        if (!File.Exists(destinationPath))
                            File.Copy(selectedPath, destinationPath, overwrite: false);

                        txtImagePath.Text = fileName;
                        MostrarImagenGrupo(new Group { IMAGE = fileName });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo copiar la imagen: " + ex.Message, "Imagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void MostrarImagenGrupo(Group grupo)
        {
            try
            {
                string imagePath = ObtenerRutaImagen(grupo);
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var ms = new MemoryStream())
                    {
                        fs.CopyTo(ms);
                        ms.Position = 0;
                        using (var imgTemp = Image.FromStream(ms))
                        {
                            pbGroupImage.Image?.Dispose();
                            pbGroupImage.Image = new Bitmap(imgTemp);
                        }
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

        private static bool IsValidImageName(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName)) return false;
            if (imageName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) return false;

            string ext = Path.GetExtension(imageName).ToLowerInvariant();
            return ext == ".jpg" || ext == ".jpeg" || ext == ".png";
        }

        private void LstGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is Group selectedGroup)
            {
                txtGroupName.Text = selectedGroup.GroupName ?? string.Empty;
                txtImagePath.Text = selectedGroup.IMAGE ?? string.Empty;
                MostrarImagenGrupo(selectedGroup);
            }
            else
            {
                txtGroupName.Clear();
                txtImagePath.Clear();
                LimpiarImagen();
            }
        }
    }
}
