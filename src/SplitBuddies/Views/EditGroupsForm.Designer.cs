using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class EditGroupsForm
    {
        private ListBox listBoxGroups;
        private TextBox txtGroupName;
        private TextBox txtMembers;
        private Label lblName;
        private Label lblMembers;
        private Button btnSaveChanges;

        private void InitializeComponent()
        {
            listBoxGroups = new ListBox();
            txtGroupName = new TextBox();
            txtMembers = new TextBox();
            lblName = new Label();
            lblMembers = new Label();
            btnSaveChanges = new Button();

            SuspendLayout();

            // listBoxGroups
            listBoxGroups.FormattingEnabled = true;
            listBoxGroups.Location = new System.Drawing.Point(20, 20);
            listBoxGroups.Size = new System.Drawing.Size(200, 150);
            listBoxGroups.SelectedIndexChanged += listBoxGroups_SelectedIndexChanged;

            // lblName
            lblName.Text = "Nombre del Grupo:";
            lblName.Location = new System.Drawing.Point(240, 20);
            lblName.AutoSize = true;

            // txtGroupName
            txtGroupName.Location = new System.Drawing.Point(240, 40);
            txtGroupName.Size = new System.Drawing.Size(200, 23);

            // lblMembers
            lblMembers.Text = "Miembros (separados por coma):";
            lblMembers.Location = new System.Drawing.Point(240, 70);
            lblMembers.AutoSize = true;

            // txtMembers
            txtMembers.Location = new System.Drawing.Point(240, 90);
            txtMembers.Size = new System.Drawing.Size(200, 23);

            // btnSaveChanges
            btnSaveChanges.Text = "Guardar Cambios";
            btnSaveChanges.Location = new System.Drawing.Point(240, 130);
            btnSaveChanges.Size = new System.Drawing.Size(200, 30);
            btnSaveChanges.Click += btnSaveChanges_Click;

            // EditGroupsForm
            ClientSize = new System.Drawing.Size(470, 200);
            Controls.Add(listBoxGroups);
            Controls.Add(lblName);
            Controls.Add(txtGroupName);
            Controls.Add(lblMembers);
            Controls.Add(txtMembers);
            Controls.Add(btnSaveChanges);
            Text = "Editar Grupos";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
