using System;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class FormCrearGrupo : Form
    {
        public FormCrearGrupo()
        {
            InitializeComponent();
        }

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Imágenes|*.jpg;*.png" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtImagen.Text = ofd.FileName;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Grupo registrado (ejemplo)");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormCrearGrupo_Load(object sender, EventArgs e)
        {

        }
    }
}