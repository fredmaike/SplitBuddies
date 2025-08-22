using SplitBuddies.Data;
using SplitBuddies.Models;
using SplitBuddies.Utils;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    public partial class BalanceForm : Form
    {
        private readonly Group _group;

        public BalanceForm(Group group)
        {
            InitializeComponent(); 
            _group = group ?? throw new ArgumentNullException(nameof(group));
            LoadData();
        }

        private void LoadData()
        {
            var gastos = DataManager.Instance.Expenses
                .Where(e => e.GroupId == _group.GroupId)
                .ToList();

          
            if (gastos.Count == 0)
            {
                gastos = new System.Collections.Generic.List<Expense>
        {
            new Expense
            {
                Description = "Cena",
                Amount = 120,
                PaidByEmail = "ana@example.com",
                InvolvedUsersEmails = new System.Collections.Generic.List<string> { "ana@example.com", "juan@example.com" },
                Date = DateTime.Now.AddDays(-1),
                GroupId = _group.GroupId
            },
            new Expense
            {
                Description = "Taxi",
                Amount = 60,
                PaidByEmail = "juan@example.com",
                InvolvedUsersEmails = new System.Collections.Generic.List<string> { "ana@example.com", "juan@example.com" },
                Date = DateTime.Now,
                GroupId = _group.GroupId
            }
        };
            }

            // --- DataTable Gastos ---
            var dtGastos = new DataTable();
            dtGastos.Columns.Add("Descripción");
            dtGastos.Columns.Add("Monto", typeof(decimal));
            dtGastos.Columns.Add("Pagador");
            dtGastos.Columns.Add("Usuario");
            dtGastos.Columns.Add("Efecto");
            dtGastos.Columns.Add("Fecha", typeof(DateTime));

            // Diccionario para calcular saldo neto
            var saldoPorUsuario = new System.Collections.Generic.Dictionary<string, decimal>();

            foreach (var g in gastos)
            {
                var involved = g.InvolvedUsersEmails ?? new System.Collections.Generic.List<string>();
                var paidBy = g.PaidByEmail ?? "(Sin pagador)";
                decimal parte = involved.Count > 0 ? Math.Abs(g.Amount) / involved.Count : Math.Abs(g.Amount);

                // Pagador recibe
                dtGastos.Rows.Add(g.Description, Math.Abs(g.Amount), paidBy, paidBy, "Recibe", g.Date);
                if (!saldoPorUsuario.ContainsKey(paidBy)) saldoPorUsuario[paidBy] = 0;
                saldoPorUsuario[paidBy] += Math.Abs(g.Amount);

                // Involucrados que deben
                foreach (var user in involved)
                {
                    if (user == paidBy) continue;
                    dtGastos.Rows.Add(g.Description, parte, paidBy, user, "Debe", g.Date);
                    if (!saldoPorUsuario.ContainsKey(user)) saldoPorUsuario[user] = 0;
                    saldoPorUsuario[user] -= parte;
                }
            }
            dgvGastos.DataSource = dtGastos;

            // --- DataTable Balance neto ---
            var dtNetos = new DataTable();
            dtNetos.Columns.Add("Email");
            dtNetos.Columns.Add("Saldo (+ recibe / - debe)", typeof(decimal));

            foreach (var kv in saldoPorUsuario.OrderBy(k => k.Key))
                dtNetos.Rows.Add(kv.Key, kv.Value);

            dgvNetos.DataSource = dtNetos;

            // --- DataTable Deudas ---
            var dtDeudas = new DataTable();
            dtDeudas.Columns.Add("De");
            dtDeudas.Columns.Add("Para");
            dtDeudas.Columns.Add("Monto", typeof(decimal));

           
            // Convertir a lista mutable
            var listaPositivos = saldoPorUsuario.Where(k => k.Value > 0)
                                               .OrderBy(k => k.Value)
                                               .Select(k => new { k.Key, Saldo = k.Value })
                                               .ToList();

            var listaNegativos = saldoPorUsuario.Where(k => k.Value < 0)
                                               .OrderBy(k => k.Value)
                                               .Select(k => new { k.Key, Saldo = k.Value })
                                               .ToList();

            // Generar deudas
            foreach (var neg in listaNegativos)
            {
                decimal deudaRestante = -neg.Saldo;

                for (int i = 0; i < listaPositivos.Count; i++)
                {
                    if (deudaRestante <= 0) break;

                    var pos = listaPositivos[i];
                    decimal pago = Math.Min(pos.Saldo, deudaRestante);

                    if (pago > 0)
                    {
                        dtDeudas.Rows.Add(neg.Key, pos.Key, pago);

                        // Actualizar saldo restante del positivo
                        listaPositivos[i] = new { Key = pos.Key, Saldo = pos.Saldo - pago };
                        deudaRestante -= pago;
                    }
                }
            }

            dgvDeudas.DataSource = dtDeudas;
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
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

                // Gastos
                w.WriteLine($"Gastos del grupo;{_group.GroupName}");
                w.WriteLine("Descripción;Monto;Pagador;Usuario;Efecto;Fecha");
                foreach (DataGridViewRow row in dgvGastos.Rows)
                    if (!row.IsNewRow)
                        w.WriteLine($"{row.Cells[0].Value};{row.Cells[1].Value};{row.Cells[2].Value};{row.Cells[3].Value};{row.Cells[4].Value};{row.Cells[5].Value}");

                w.WriteLine();

                // Balance neto
                w.WriteLine("Balance neto por usuario (+ recibe / - debe)");
                w.WriteLine("Email;Saldo");
                foreach (DataGridViewRow row in dgvNetos.Rows)
                    if (!row.IsNewRow)
                        w.WriteLine($"{row.Cells[0].Value};{row.Cells[1].Value}");

                w.WriteLine();

                // Deudas
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
