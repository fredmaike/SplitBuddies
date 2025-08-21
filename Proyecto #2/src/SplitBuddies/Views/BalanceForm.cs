using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Utils;
using SplitBuddies.Models;
using GroupModel = SplitBuddies.Models.Group; // alias por consistencia

namespace SplitBuddies.Views
{
    // NO usa diseñador: no hay InitializeComponent()
    public sealed class BalanceForm : Form
    {
        private readonly GroupModel _group;

        private DataGridView dgvNetos;
        private DataGridView dgvDeudas;
        private Button btnExportCsv;

        public BalanceForm(GroupModel group)
        {
            _group = group ?? throw new ArgumentNullException(nameof(group));
            BuildUi();
            LoadData();
        }

        private void BuildUi()
        {
            Text = $"Balance - {_group.GroupName}";
            Width = 900;
            Height = 600;
            StartPosition = FormStartPosition.CenterParent;

            dgvNetos = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 230,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            dgvDeudas = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            btnExportCsv = new Button
            {
                Text = "Exportar CSV",
                Dock = DockStyle.Bottom,
                Height = 38
            };
            btnExportCsv.Click += BtnExportCsv_Click;

            Controls.Add(dgvDeudas);
            Controls.Add(dgvNetos);
            Controls.Add(btnExportCsv);
        }

        private void LoadData()
        {
            var netos = BalanceCalculator.CalcularBalancePorUsuario(_group);
            var deudas = BalanceCalculator.CalcularDeudas(netos);

            var dtNetos = new DataTable();
            dtNetos.Columns.Add("Email");
            dtNetos.Columns.Add("Saldo (+ recibe / - debe)", typeof(decimal));
            foreach (var kv in netos.OrderBy(k => k.Key))
                dtNetos.Rows.Add(kv.Key, kv.Value);
            dgvNetos.DataSource = dtNetos;

            var dtDeudas = new DataTable();
            dtDeudas.Columns.Add("De");
            dtDeudas.Columns.Add("Para");
            dtDeudas.Columns.Add("Monto", typeof(decimal));
            foreach (var s in deudas)
                dtDeudas.Rows.Add(s.FromEmail, s.ToEmail, s.Amount);
            dgvDeudas.DataSource = dtDeudas;
        }

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog
            {
                Title = "Guardar balance como CSV",
                Filter = "CSV (*.csv)|*.csv",
                FileName = $"Balance_{_group.GroupName}_{DateTime.Now:yyyyMMdd}.csv"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var w = new System.IO.StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8);
                w.WriteLine($"Balance del grupo;{_group.GroupName}");
                w.WriteLine("Neto por usuario (+ recibe / - debe)");
                w.WriteLine("Email;Saldo");
                foreach (DataGridViewRow row in dgvNetos.Rows)
                    if (!row.IsNewRow)
                        w.WriteLine($"{row.Cells[0].Value};{row.Cells[1].Value}");
                w.WriteLine();
                w.WriteLine("Propuesta de pagos");
                w.WriteLine("De;Para;Monto");
                foreach (DataGridViewRow row in dgvDeudas.Rows)
                    if (!row.IsNewRow)
                        w.WriteLine($"{row.Cells[0].Value};{row.Cells[1].Value};{row.Cells[2].Value}");
                w.Flush();

                MessageBox.Show("CSV exportado correctamente.", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo exportar el CSV: " + ex.Message, "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
