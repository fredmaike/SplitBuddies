using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SplitBuddies.Controllers;
using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using GroupModel = SplitBuddies.Models.Group; // alias para evitar conflicto con Regex.Group

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

            // Asegurar datos
            EnsureDataLoaded();

            // El controlador opera sobre la lista viva del DataManager
            groupController = new GroupController(DataManager.Instance.Groups);

            // Enlaces de eventos (por si no están conectados en el diseñador)
            this.Load += GroupForm_Load;
            lstGrupos.SelectedIndexChanged += LstGrupos_SelectedIndexChanged;
            btnCreateGroup.Click += btnCreateGroup_Click;
            btnDeleteGroup.Click += btnDeleteGroup_Click;

            // Conectar botón de Calcular/Ver balance si existe
            if (this.Controls.Find("btnCalculateBalance", true).FirstOrDefault() is Button bCalc)
                bCalc.Click += btnCalculateBalance_Click;

            // Conectar botón de seleccionar imagen si existe
            if (this.Controls.Find("btnSelectImage", true).FirstOrDefault() is Button bImg)
                bImg.Click += BtnSelectImage_Click;

            // Botón de invitar (opcional Proyecto 2)
            if (this.Controls.Find("btnInvite", true).FirstOrDefault() is Button bInvite)
                bInvite.Click += btnInvite_Click;
        }

        // ========= Infra =========
        private void EnsureDataLoaded()
        {
            var dm = DataManager.Instance;
            dm.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dm.BasePath))
                Directory.CreateDirectory(dm.BasePath);

            dm.LoadUsers();
            dm.LoadGroups();
            dm.LoadExpenses();
            try { dm.LoadInvitations(); } catch { /* si no existe, lista vacía */ }
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
            var userGroups = groupController.GetGroupsForUser(currentUser.Email) ?? new List<GroupModel>();

            lstGrupos.DataSource = null;
            lstGrupos.DataSource = userGroups;
            lstGrupos.DisplayMember = nameof(GroupModel.GroupName);

            if (userGroups.Count == 0)
            {
                txtGroupName.Clear();
                txtImagePath.Clear();
                LimpiarImagen();
            }
        }

        // Crear grupo
        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            string name = (txtGroupName?.Text ?? string.Empty).Trim();
            string imageName = (txtImagePath?.Text ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Ingrese un nombre para el grupo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Evitar duplicados por nombre (del mismo usuario)
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

        // Eliminar grupo
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is not GroupModel selectedGroup)
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

        // Selección de grupo
        private void LstGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is GroupModel selectedGroup)
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
                        MostrarImagenGrupo(new GroupModel { IMAGE = fileName });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo copiar la imagen: " + ex.Message, "Imagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void MostrarImagenGrupo(GroupModel grupo)
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

        private static string ObtenerRutaImagen(GroupModel grupo)
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

        // ========= Invitaciones (opcional para Proyecto 2) =========
        private void btnInvite_Click(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is not GroupModel selectedGroup)
            {
                MessageBox.Show("Seleccione un grupo para invitar usuarios.", "Invitación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var txt = this.Controls.Find("txtInviteEmail", true).FirstOrDefault() as TextBox;
            if (txt == null)
            {
                MessageBox.Show("No existe el campo txtInviteEmail en el formulario.", "Invitación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string invitee = (txt.Text ?? string.Empty).Trim();
            if (!IsValidEmail(invitee))
            {
                MessageBox.Show("Ingrese un email válido para invitar.", "Invitación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedGroup.Members ??= new List<string>();
            bool alreadyMember = selectedGroup.Members.Any(m => string.Equals(m, invitee, StringComparison.OrdinalIgnoreCase));
            if (alreadyMember)
            {
                MessageBox.Show("Ese email ya es miembro del grupo.", "Invitación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dm = DataManager.Instance;

            var inv = new Invitation
            {
                InvitationId = dm.GetNextInvitationId(),
                GroupId = selectedGroup.GroupId,
                InviteeEmail = invitee,
                InviterEmail = currentUser?.Email,
                Status = "Pending"
            };

            dm.Invitations.Add(inv);
            dm.SaveInvitations();

            MessageBox.Show("Invitación enviada.", "Invitación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txt.Clear();
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            }
            catch { return false; }
        }

        // ========= Balance =========
        private void btnCalculateBalance_Click(object sender, EventArgs e)
        {
            if (lstGrupos.SelectedItem is not GroupModel selectedGroup)
            {
                MessageBox.Show("Seleccione un grupo.", "Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var form = new BalanceForm(selectedGroup);
            form.ShowDialog(this);
        }
    }
}
