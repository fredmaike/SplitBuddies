namespace SplitBuddies.Views
{
    partial class MostrarForm
    {
        /// <summary>
        /// Contenedor para los componentes del formulario, utilizado para liberar recursos.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// TreeView para mostrar la jerarquía de grupos y detalles relacionados (miembros y gastos).
        /// </summary>
        private System.Windows.Forms.TreeView treeViewGrupos;

        /// <summary>
        /// Libera los recursos utilizados por el formulario.
        /// </summary>
        /// <param name="disposing">Indica si se deben liberar recursos administrados.</param>
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
        /// Inicializa y configura todos los componentes visuales del formulario MostrarForm.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeViewGrupos = new System.Windows.Forms.TreeView();
            this.SuspendLayout();

            // ==========================
            // Configuración del TreeView
            // ==========================
            this.treeViewGrupos.Location = new System.Drawing.Point(12, 12);
            this.treeViewGrupos.Name = "treeViewGrupos";
            this.treeViewGrupos.Size = new System.Drawing.Size(500, 400);
            this.treeViewGrupos.TabIndex = 0;

            // ==========================
            // Configuración general del formulario
            // ==========================
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 430);
            this.Controls.Add(this.treeViewGrupos);
            this.Name = "MostrarForm";
            this.Text = "Grupos y Detalles";

            this.ResumeLayout(false);
        }

        #endregion
    }
}
