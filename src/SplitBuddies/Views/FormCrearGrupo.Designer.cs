using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class FormCrearGrupo
    {
        private Label lblNombre;
        private TextBox txtNombreGrupo;
        private Label lblImagen;
        private TextBox txtImagen;
        private Button btnSeleccionarImagen;
        private Label lblMiembros;
        private CheckedListBox checkedListBoxMiembros;
        private Button btnGuardar;
        private Button btnCancelar;

        private void InitializeComponent()
        {
            lblNombre = new Label();
            txtNombreGrupo = new TextBox();
            lblImagen = new Label();
            txtImagen = new TextBox();
            btnSeleccionarImagen = new Button();
            lblMiembros = new Label();
            checkedListBoxMiembros = new CheckedListBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // lblNombre
            // 
            lblNombre.Location = new Point(0, 0);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(100, 23);
            lblNombre.TabIndex = 0;
            // 
            // txtNombreGrupo
            // 
            txtNombreGrupo.Location = new Point(0, 0);
            txtNombreGrupo.Name = "txtNombreGrupo";
            txtNombreGrupo.Size = new Size(100, 23);
            txtNombreGrupo.TabIndex = 1;
            // 
            // lblImagen
            // 
            lblImagen.Location = new Point(0, 0);
            lblImagen.Name = "lblImagen";
            lblImagen.Size = new Size(100, 23);
            lblImagen.TabIndex = 2;
            // 
            // txtImagen
            // 
            txtImagen.Location = new Point(0, 0);
            txtImagen.Name = "txtImagen";
            txtImagen.Size = new Size(100, 23);
            txtImagen.TabIndex = 3;
            // 
            // btnSeleccionarImagen
            // 
            btnSeleccionarImagen.Location = new Point(0, 0);
            btnSeleccionarImagen.Name = "btnSeleccionarImagen";
            btnSeleccionarImagen.Size = new Size(75, 23);
            btnSeleccionarImagen.TabIndex = 4;
            btnSeleccionarImagen.Click += btnSeleccionarImagen_Click;
            // 
            // lblMiembros
            // 
            lblMiembros.Location = new Point(0, 0);
            lblMiembros.Name = "lblMiembros";
            lblMiembros.Size = new Size(100, 23);
            lblMiembros.TabIndex = 5;
            // 
            // checkedListBoxMiembros
            // 
            checkedListBoxMiembros.Items.AddRange(new object[] { "Usuario 1", "Usuario 2", "Usuario 3" });
            checkedListBoxMiembros.Location = new Point(0, 0);
            checkedListBoxMiembros.Name = "checkedListBoxMiembros";
            checkedListBoxMiembros.Size = new Size(120, 94);
            checkedListBoxMiembros.TabIndex = 6;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(0, 0);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 7;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(0, 0);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 8;
            // 
            // FormCrearGrupo
            // 
            ClientSize = new Size(320, 250);
            Controls.Add(lblNombre);
            Controls.Add(txtNombreGrupo);
            Controls.Add(lblImagen);
            Controls.Add(txtImagen);
            Controls.Add(btnSeleccionarImagen);
            Controls.Add(lblMiembros);
            Controls.Add(checkedListBoxMiembros);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Name = "FormCrearGrupo";
            Text = "Nuevo Grupo";
            Load += FormCrearGrupo_Load;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}