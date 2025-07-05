
namespace SplitBuddies.Views
{
    partial class GroupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtGroupName = new TextBox();
            btnChooseImage = new Button();
            picGroupImage = new PictureBox();
            clbMembers = new CheckedListBox();
            btnCreateGroup = new Button();
            ((System.ComponentModel.ISupportInitialize)picGroupImage).BeginInit();
            SuspendLayout();
            // 
            // txtGroupName
            // 
            txtGroupName.Location = new Point(283, 241);
            txtGroupName.Name = "txtGroupName";
            txtGroupName.Size = new Size(125, 27);
            txtGroupName.TabIndex = 0;
            // 
            // btnChooseImage
            // 
            btnChooseImage.Location = new Point(118, 363);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(147, 29);
            btnChooseImage.TabIndex = 1;
            btnChooseImage.Text = "Elegir Imagen";
            btnChooseImage.UseVisualStyleBackColor = true;
            btnChooseImage.Click += btnChooseImage_Click_1;
            // 
            // picGroupImage
            // 
            picGroupImage.Location = new Point(118, 218);
            picGroupImage.Name = "picGroupImage";
            picGroupImage.Size = new Size(125, 62);
            picGroupImage.TabIndex = 2;
            picGroupImage.TabStop = false;
            // 
            // clbMembers
            // 
            clbMembers.FormattingEnabled = true;
            clbMembers.Location = new Point(105, 36);
            clbMembers.Name = "clbMembers";
            clbMembers.Size = new Size(329, 114);
            clbMembers.TabIndex = 3;
            // 
            // btnCreateGroup
            // 
            btnCreateGroup.Location = new Point(575, 363);
            btnCreateGroup.Name = "btnCreateGroup";
            btnCreateGroup.Size = new Size(147, 29);
            btnCreateGroup.TabIndex = 4;
            btnCreateGroup.Text = "Crear Grupo";
            btnCreateGroup.UseVisualStyleBackColor = true;
            btnCreateGroup.Click += btnCreateGroup_Click;
            // 
            // GroupForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCreateGroup);
            Controls.Add(clbMembers);
            Controls.Add(picGroupImage);
            Controls.Add(btnChooseImage);
            Controls.Add(txtGroupName);
            Name = "GroupForm";
            Text = "GroupForm";
            Load += GroupForm_Load;
            ((System.ComponentModel.ISupportInitialize)picGroupImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private TextBox txtGroupName;
        private Button btnChooseImage;
        private PictureBox picGroupImage;
        private CheckedListBox clbMembers;
        private Button btnCreateGroup;
    }
}