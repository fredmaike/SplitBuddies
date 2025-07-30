using System;
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
            pbGroupImage = new PictureBox();

            SuspendLayout();
            // 
            // lstGroups
            // 
            lstGrupos.Location = new Point(20, 20);
            lstGrupos.Name = "lstGroups";
            lstGrupos.Size = new Size(200, 200);
            lstGrupos.TabIndex = 0;
            lstGrupos.SelectedIndexChanged += lstGroups_SelectedIndexChanged; 

            pbGroupImage.Location = new Point(240, 20);
            pbGroupImage.Size = new Size(140, 100);
            pbGroupImage.SizeMode = PictureBoxSizeMode.Zoom; 
            pbGroupImage.BorderStyle = BorderStyle.FixedSingle;
            //
            // lblGroupName
            //
            lblGroupName.Location = new Point(240, 130);
            lblGroupName.Name = "lblGroupName";
            lblGroupName.Size = new Size(120, 23);
            lblGroupName.Text = "Nombre del grupo:";
            lblGroupName.TabIndex = 1;
            // 
            // txtGroupName
            // 
            txtGroupName.Location = new Point(240, 150);
            txtGroupName.Name = "txtGroupName";
            txtGroupName.Size = new Size(140, 23);
            txtGroupName.TabIndex = 2;
            //
            // lblImagePath
            //
            lblImagePath.Location = new Point(240, 180);
            lblImagePath.Name = "lblImagePath";
            lblImagePath.Size = new Size(120, 23);
            lblImagePath.Text = "Ruta de imagen:";
            lblImagePath.TabIndex = 3;
            // 
            // txtImagePath
            // 
            txtImagePath.Location = new Point(240, 200);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.Size = new Size(140, 23);
            txtImagePath.TabIndex = 4;
            // 
            // btnCreateGroup
            // 
            btnCreateGroup.Location = new Point(20, 230);
            btnCreateGroup.Name = "btnCreateGroup";
            btnCreateGroup.Size = new Size(100, 30);
            btnCreateGroup.Text = "Crear grupo";
            btnCreateGroup.TabIndex = 5;
            btnCreateGroup.Click += btnCreateGroup_Click;
            // 
            // btnDeleteGroup
            // 
            btnDeleteGroup.Location = new Point(130, 230);
            btnDeleteGroup.Name = "btnDeleteGroup";
            btnDeleteGroup.Size = new Size(100, 30);
            btnDeleteGroup.Text = "Eliminar grupo";
            btnDeleteGroup.TabIndex = 6;
            btnDeleteGroup.Click += btnDeleteGroup_Click;
            // 
            // GroupForm
            // 
            ClientSize = new Size(420, 280);
            Controls.Add(lstGrupos);
            Controls.Add(pbGroupImage); 
            Controls.Add(lblGroupName);
            Controls.Add(txtGroupName);
            Controls.Add(lblImagePath);
            Controls.Add(txtImagePath);
            Controls.Add(btnCreateGroup);
            Controls.Add(btnDeleteGroup);
            Name = "GroupForm";
            Text = "Grupos";
            Load += GroupForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}


