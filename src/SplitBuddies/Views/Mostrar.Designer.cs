namespace SplitBuddies.Views
{
    partial class MostrarForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TreeView treeViewGrupos;

      
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

     
        private void InitializeComponent()
        {
            this.treeViewGrupos = new System.Windows.Forms.TreeView();
            this.SuspendLayout();

            // 
            // treeViewGrupos
            // 
            this.treeViewGrupos.Location = new System.Drawing.Point(12, 12);
            this.treeViewGrupos.Name = "treeViewGrupos";
            this.treeViewGrupos.Size = new System.Drawing.Size(500, 400);
            this.treeViewGrupos.TabIndex = 0;

            // 
            // MostrarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 430);
            this.Controls.Add(this.treeViewGrupos);
            this.Name = "MostrarForm";
            this.Text = "Grupos y Detalles";
            this.ResumeLayout(false);
        }
    }
}
