using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class GroupForm
    {
        private ListBox lstGrupos;
        private Label lblGroupName;
        private TextBox txtGroupName;
        private Label lblImagePath;
        private TextBox txtImagePath;
        private Button btnCreateGroup;
        private Button btnDeleteGroup;
        private Button btnSelectImage;
        private PictureBox pbGroupImage;

        private void InitializeComponent()
        {
            lstGrupos = new ListBox();
            lblGroupName = new Label();
            txtGroupName = new TextBox();
            lblImagePath = new Label();
            txtImagePath = new TextBox();
            btnCreateGroup = new Button();
            btnDeleteGroup = new Button();
            btnSelectImage = new Button();
            pbGroupImage = new PictureBox();

            SuspendLayout();

            // lstGrupos
            lstGrupos.Location = new Point(20, 20);
            lstGrupos.Size = new Size(200, 200);
            lstGrupos.SelectedIndexChanged += LstGrupos_SelectedIndexChanged;

            // pbGroupImage
            pbGroupImage.Location = new Point(240, 20);
            pbGroupImage.Size = new Size(140, 100);
            pbGroupImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbGroupImage.BorderStyle = BorderStyle.FixedSingle;

            // lblGroupName
            lblGroupName.Location = new Point(240, 130);
            lblGroupName.Size = new Size(120, 23);
            lblGroupName.Text = "Nombre del grupo:";

            // txtGroupName
            txtGroupName.Location = new Point(240, 150);
            txtGroupName.Size = new Size(140, 23);

            // lblImagePath
            lblImagePath.Location = new Point(240, 180);
            lblImagePath.Size = new Size(120, 23);
            lblImagePath.Text = "Ruta de imagen:";

            // txtImagePath
            txtImagePath.Location = new Point(240, 200);
            txtImagePath.Size = new Size(140, 23);
            txtImagePath.ReadOnly = true;

            // btnSelectImage
            btnSelectImage.Location = new Point(240, 230);
            btnSelectImage.Size = new Size(140, 30);
            btnSelectImage.Text = "Seleccionar imagen";
            btnSelectImage.Click += BtnSelectImage_Click;

            // btnCreateGroup
            btnCreateGroup.Location = new Point(20, 230);
            btnCreateGroup.Size = new Size(100, 30);
            btnCreateGroup.Text = "Crear grupo";
            btnCreateGroup.Click += btnCreateGroup_Click;

            // btnDeleteGroup
            btnDeleteGroup.Location = new Point(130, 230);
            btnDeleteGroup.Size = new Size(100, 30);
            btnDeleteGroup.Text = "Eliminar grupo";
            btnDeleteGroup.Click += btnDeleteGroup_Click;

            // Form
            ClientSize = new Size(420, 280);
            Controls.Add(lstGrupos);
            Controls.Add(pbGroupImage);
            Controls.Add(lblGroupName);
            Controls.Add(txtGroupName);
            Controls.Add(lblImagePath);
            Controls.Add(txtImagePath);
            Controls.Add(btnSelectImage);
            Controls.Add(btnCreateGroup);
            Controls.Add(btnDeleteGroup);
            Text = "Gestión de Grupos";
            Load += GroupForm_Load;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
