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
    /// <summary>
    /// Formulario principal para la gestión de grupos.
    /// Permite al usuario crear, eliminar y administrar grupos,
    /// seleccionar imágenes, ver balances, enviar invitaciones
    /// y generar reportes de gastos.
    /// </summary>
    public partial class GroupForm : Form
    {
        private readonly GroupController groupController;
        private User currentUser;

        public GroupForm(User user)
        {
            InitializeComponent();

            currentUser = user ?? throw new ArgumentNullException(nameof(user));

            // Iniciar sesión si es necesario
            if (!AppSession.IsAuthenticated && !string.IsNullOrWhiteSpace(currentUser.Email))
                AppSession.SignIn(currentUser.Email);

            EnsureDataLoaded();
            groupController = new GroupController(DataManager.Instance.Groups);

            // Suscribimos solo eventos que no estén en el diseñador
            this.Load += GroupForm_Load;
            lstGrupos.SelectedIndexChanged += LstGrupos_SelectedIndexChanged;
        }

        #region Infraestructura

        private static void EnsureDataLoaded()
        {
            var dm = DataManager.Instance;
            dm.BasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            if (!Directory.Exists(dm.BasePath))
                Directory.CreateDirectory(dm.BasePath);

            dm.LoadUsers();
            dm.LoadGroups();
            dm.LoadExpenses();

            try { dm.LoadInvitations(); }
            catch { /* Ignorar errores si no hay invitaciones */ }
        }

        private static void SaveAll()
        {
            var dm = DataManager.Instance;
            dm.SaveGroups();

            try { dm.SaveInvitations(); }
            catch { /* Ignorar errores si falla guardar invitaciones */ }
        }

        #endregion

        #region Carga de UI

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

        #endregion

        #region Gestión de Grupos

        private void btnCreateGroup_Click(object sender, EventArgs e)
        {
            string name = txtGroupName.Text.Trim();
            string imageName = txtImagePath.Text.Trim();

            if (!ValidarNuevoGrupo(name, imageName))
                return;

            var members = new List<string> { currentUser.Email };
            groupController.CreateGroup(name, imageName, members);

            SaveAll();
            LoadGroups();

            MessageBox.Show("Grupo creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidarNuevoGrupo(string name, string imageName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Ingrese un nombre para el grupo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            bool exists = DataManager.Instance.Groups
                .Where(g => (g.Members ?? new List<string>()).Any(m => string.Equals(m, currentUser.Email, StringComparison.OrdinalIgnoreCase)))
                .Any(g => string.Equals(g.GroupName, name, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                MessageBox.Show("Ya existe un grupo con ese nombre para este usuario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(imageName) && !IsValidImageName(imageName))
            {
                MessageBox.Show("El nombre de la imagen no es válido. Use .jpg/.jpeg/.png", "Imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

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

        #endregion

        #region Gestión de Imagen

        private void BtnSelectImage_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Seleccionar imagen del grupo",
                Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                string selectedPath = ofd.FileName;
                string imageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IMAGE");
                Directory.CreateDirectory(imageFolder);

                string fileName = Path.GetFileName(selectedPath);
                string destinationPath = Path.Combine(imageFolder, fileName);

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

        private void MostrarImagenGrupo(Group grupo)
        {
            try
            {
                string imagePath = ObtenerRutaImagen(grupo);
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    using var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using var ms = new MemoryStream();
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    using var imgTemp = Image.FromStream(ms);

                    pbGroupImage.Image?.Dispose();
                    pbGroupImage.Image = new Bitmap(imgTemp);
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

        private static string ObtenerRutaImagen(Group grupo)
        {
            if (grupo == null || string.IsNullOrWhiteSpace(grupo.IMAGE))
                return null;

            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "IMAGE");
            return Path.Combine(basePath, grupo.IMAGE.Replace('/', Path.DirectorySeparatorChar));
        }

        #endregion

        #region Botones adicionales

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

        #endregion

        #region Reportes

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            var dt = CrearDataTableReporte();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No hay gastos en el rango de fechas seleccionado.", "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvReport.DataSource = null;
                return;
            }

            dgvReport.DataSource = dt;
        }

        private System.Data.DataTable CrearDataTableReporte()
        {
            var from = dtpFrom.Value.Date;
            var to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);

            var userGroups = groupController.GetGroupsForUser(currentUser.Email) ?? new List<Group>();
            var gastos = DataManager.Instance.Expenses
                .Where(exp => userGroups.Any(g => g.GroupId == exp.GroupId) && exp.Date >= from && exp.Date <= to)
                .ToList();

            var dt = new System.Data.DataTable();
            dt.Columns.Add("Grupo");
            dt.Columns.Add("Descripción");
            dt.Columns.Add("Monto", typeof(decimal));
            dt.Columns.Add("Pagador");
            dt.Columns.Add("Usuario");
            dt.Columns.Add("Efecto");
            dt.Columns.Add("Fecha", typeof(DateTime));

            var saldoPorUsuario = new Dictionary<string, decimal>();

            foreach (var g in gastos)
            {
                var groupName = userGroups.First(gr => gr.GroupId == g.GroupId).GroupName ?? "(Sin nombre)";
                var involved = g.InvolvedUsersEmails ?? new List<string>();
                var paidBy = g.PaidByEmail ?? "(Sin pagador)";
                decimal parte = involved.Count > 0 ? Math.Abs(g.Amount) / involved.Count : Math.Abs(g.Amount);

                dt.Rows.Add(groupName, g.Description, Math.Abs(g.Amount), paidBy, paidBy, "Recibe", g.Date);
                if (!saldoPorUsuario.ContainsKey(paidBy)) saldoPorUsuario[paidBy] = 0;
                saldoPorUsuario[paidBy] += Math.Abs(g.Amount);

                foreach (var user in involved)
                {
                    if (user == paidBy) continue;
                    dt.Rows.Add(groupName, g.Description, parte, paidBy, user, "Debe", g.Date);
                    if (!saldoPorUsuario.ContainsKey(user)) saldoPorUsuario[user] = 0;
                    saldoPorUsuario[user] -= parte;
                }
            }

            return dt;
        }

        private void BtnExportReport_Click(object sender, EventArgs e)
        {
            if (dgvReport.DataSource == null)
            {
                MessageBox.Show("No hay datos para exportar.", "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using var sfd = new SaveFileDialog
                {
                    Filter = "CSV (*.csv)|*.csv",
                    FileName = "Reporte.csv"
                };

                if (sfd.ShowDialog() != DialogResult.OK) return;

                var dt = (System.Data.DataTable)dgvReport.DataSource;
                using var sw = new StreamWriter(sfd.FileName);

                sw.WriteLine(string.Join(";", dt.Columns.Cast<System.Data.DataColumn>().Select(c => c.ColumnName)));

                foreach (System.Data.DataRow row in dt.Rows)
                    sw.WriteLine(string.Join(";", row.ItemArray.Select(f => f.ToString())));

                MessageBox.Show("Reporte exportado correctamente.", "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar: " + ex.Message, "Exportar CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Selección de grupo

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

        #endregion
    }
}
