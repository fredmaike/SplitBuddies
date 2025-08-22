using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class BalanceForm
    {
        private DataGridView dgvGastos;
        private DataGridView dgvNetos;
        private DataGridView dgvDeudas;
        private Button btnExportCsv;

        private void InitializeComponent()
        {
            this.dgvGastos = new DataGridView();
            this.dgvNetos = new DataGridView();
            this.dgvDeudas = new DataGridView();
            this.btnExportCsv = new Button();

            // --- dgvGastos ---
            this.dgvGastos.Location = new System.Drawing.Point(12, 12);
            this.dgvGastos.Size = new System.Drawing.Size(960, 300);
            this.dgvGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- dgvNetos ---
            this.dgvNetos.Location = new System.Drawing.Point(12, 320);
            this.dgvNetos.Size = new System.Drawing.Size(450, 200);
            this.dgvNetos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- dgvDeudas ---
            this.dgvDeudas.Location = new System.Drawing.Point(480, 320);
            this.dgvDeudas.Size = new System.Drawing.Size(492, 200);
            this.dgvDeudas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- btnExportCsv ---
            this.btnExportCsv.Location = new System.Drawing.Point(12, 530);
            this.btnExportCsv.Size = new System.Drawing.Size(150, 30);
            this.btnExportCsv.Text = "Exportar CSV";
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);

            // --- BalanceForm ---
            this.ClientSize = new System.Drawing.Size(984, 571);
            this.Controls.Add(this.dgvGastos);
            this.Controls.Add(this.dgvNetos);
            this.Controls.Add(this.dgvDeudas);
            this.Controls.Add(this.btnExportCsv);
            this.Text = "Balance del Grupo";
        }
    }
}
