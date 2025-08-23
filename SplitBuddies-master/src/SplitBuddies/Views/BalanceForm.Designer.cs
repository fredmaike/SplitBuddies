using System;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario que muestra el balance de un grupo,
    /// incluyendo gastos, balances netos y deudas entre usuarios.
    /// </summary>
    partial class BalanceForm
    {
        // --- Controles del formulario ---
        private DataGridView dgvGastos;
        private DataGridView dgvNetos;
        private DataGridView dgvDeudas;
        private Button btnExportCsv;

        /// <summary>
        /// Inicializa los componentes del formulario.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvGastos = new DataGridView();
            this.dgvNetos = new DataGridView();
            this.dgvDeudas = new DataGridView();
            this.btnExportCsv = new Button();

            ConfigurarDataGridGastos();
            ConfigurarDataGridNetos();
            ConfigurarDataGridDeudas();
            ConfigurarBotonExportCsv();

            // --- Configuración del formulario ---
            this.ClientSize = new System.Drawing.Size(984, 571);
            this.Controls.Add(this.dgvGastos);
            this.Controls.Add(this.dgvNetos);
            this.Controls.Add(this.dgvDeudas);
            this.Controls.Add(this.btnExportCsv);
            this.Text = "Balance del Grupo";
        }

        #region Métodos privados de configuración

        /// <summary>
        /// Configura el DataGridView de gastos.
        /// </summary>
        private void ConfigurarDataGridGastos()
        {
            dgvGastos.Location = new System.Drawing.Point(12, 12);
            dgvGastos.Size = new System.Drawing.Size(960, 300);
            dgvGastos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGastos.ReadOnly = true;
        }

        /// <summary>
        /// Configura el DataGridView de balances netos.
        /// </summary>
        private void ConfigurarDataGridNetos()
        {
            dgvNetos.Location = new System.Drawing.Point(12, 320);
            dgvNetos.Size = new System.Drawing.Size(450, 200);
            dgvNetos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNetos.ReadOnly = true;
        }

        /// <summary>
        /// Configura el DataGridView de deudas.
        /// </summary>
        private void ConfigurarDataGridDeudas()
        {
            dgvDeudas.Location = new System.Drawing.Point(480, 320);
            dgvDeudas.Size = new System.Drawing.Size(492, 200);
            dgvDeudas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeudas.ReadOnly = true;
        }

        /// <summary>
        /// Configura el botón para exportar los datos a CSV.
        /// </summary>
        private void ConfigurarBotonExportCsv()
        {
            btnExportCsv.Location = new System.Drawing.Point(12, 530);
            btnExportCsv.Size = new System.Drawing.Size(150, 30);
            btnExportCsv.Text = "Exportar CSV";
            btnExportCsv.Click += BtnExportCsv_Click;
        }

        #endregion

        #region Eventos

        /// <summary>
        /// Maneja el evento click del botón Exportar CSV.
        /// Se recomienda envolver la lógica de exportación en try/catch.
        /// </summary>
        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            try
            {
                // Lógica de exportación aquí
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar CSV: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}
