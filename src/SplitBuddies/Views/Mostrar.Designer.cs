namespace SplitBuddies.Views
{
    partial class MostrarForm
    {
        // Contenedor para los componentes del formulario, usado para liberar recursos
        private System.ComponentModel.IContainer components = null;

        // Control TreeView para mostrar la jerarquía de grupos y detalles relacionados
        private System.Windows.Forms.TreeView treeViewGrupos;

        // Método para liberar los recursos usados por el formulario
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Método para inicializar y configurar los componentes visuales del formulario
        private void InitializeComponent()
        {
            this.treeViewGrupos = new System.Windows.Forms.TreeView();
            this.SuspendLayout();

            // 
            // treeViewGrupos
            // 
            // Ubicación y tamaño del control TreeView en el formulario
            this.treeViewGrupos.Location = new System.Drawing.Point(12, 12);
            this.treeViewGrupos.Name = "treeViewGrupos";
            this.treeViewGrupos.Size = new System.Drawing.Size(500, 400);
            this.treeViewGrupos.TabIndex = 0;

            // 
            // MostrarForm
            // 
            // Configuración general del formulario
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
