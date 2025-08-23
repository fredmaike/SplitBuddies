using SplitBuddies.Data;
using SplitBuddies.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SplitBuddies.Views
{
    public partial class BalanceForm : Form
    {
        private readonly Group _group;

        public BalanceForm(Group group)
        {
            InitializeComponent();
            _group = group ?? throw new ArgumentNullException(nameof(group));
            CargarDatos();
        }

        /// <summary>
        /// Carga los datos de gastos, saldo neto y deudas del grupo en los DataGridView.
        /// </summary>
        private void CargarDatos()
        {
            var gastos = ObtenerGastosDelGrupo();
            var saldoPorUsuario = CalcularSaldoPorUsuario(gastos);

            dgvGastos.DataSource = CrearDataTableGastos(gastos);
            dgvNetos.DataSource = CrearDataTableNetos(saldoPorUsuario);
            dgvDeudas.DataSource = CrearDataTableDeudas(saldoPorUsuario);
        }

        /// <summary>
        /// Obtiene los gastos del grupo desde DataManager. Si no hay, se generan gastos de ejemplo.
        /// </summary>
        private List<Expense> ObtenerGastosDelGrupo()
        {
            var gastos = DataManager.Instance.Expenses
                .Where(e => e.GroupId == _group.GroupId)
                .ToList();

            if (!gastos.Any())
            {
                gastos = new List<Expense>
                {
                    new Expense
                    {
                        Description = "Cena",
                        Amount = 120,
                        PaidByEmail = "ana@example.com",
                        InvolvedUsersEmails = new List<string> { "ana@example.com", "juan@example.com" },
                        Date = DateTime.Now.AddDays(-1),
                        GroupId = _group.GroupId
                    },
                    new Expense
                    {
                        Description = "Taxi",
                        Amount = 60,
                        PaidByEmail = "juan@example.com",
                        InvolvedUsersEmails = new List<string> { "ana@example.com", "juan@example.com" },
                        Date = DateTime.Now,
                        GroupId = _group.GroupId
                    }
                };
            }

            return gastos;
        }

        /// <summary>
        /// Calcula el saldo neto por usuario (positivo = recibe, negativo = debe).
        /// </summary>
        public static Dictionary<string, decimal> CalcularSaldoPorUsuario(List<Expense> gastos)
        {
            var saldoPorUsuario = new Dictionary<string, decimal>();

            foreach (var gasto in gastos)
            {
                var involucrados = gasto.InvolvedUsersEmails ?? new List<string>();
                var pagador = gasto.PaidByEmail ?? "(Sin pagador)";
                decimal parte = involucrados.Count > 0 ? Math.Abs(gasto.Amount) / involucrados.Count : Math.Abs(gasto.Amount);

                if (!saldoPorUsuario.ContainsKey(pagador)) saldoPorUsuario[pagador] = 0;
                saldoPorUsuario[pagador] += Math.Abs(gasto.Amount);

                foreach (var user in involucrados.Where(u => u != pagador))
                {
                    if (!saldoPorUsuario.ContainsKey(user)) saldoPorUsuario[user] = 0;
                    saldoPorUsuario[user] -= parte;
                }
            }

            return saldoPorUsuario;
        }

        /// <summary>
        /// Crea el DataTable con los gastos detallados.
        /// </summary>
        public static DataTable CrearDataTableGastos(List<Expense> gastos)
        {
            var dt = new DataTable();
            dt.Columns.Add("Descripción");
            dt.Columns.Add("Monto", typeof(decimal));
            dt.Columns.Add("Pagador");
            dt.Columns.Add("Usuario");
            dt.Columns.Add("Efecto");
            dt.Columns.Add("Fecha", typeof(DateTime));

            foreach (var g in gastos)
            {
                var involucrados = g.InvolvedUsersEmails ?? new List<string>();
                var pagador = g.PaidByEmail ?? "(Sin pagador)";
                decimal parte = involucrados.Count > 0 ? Math.Abs(g.Amount) / involucrados.Count : Math.Abs(g.Amount);

                dt.Rows.Add(g.Description, Math.Abs(g.Amount), pagador, pagador, "Recibe", g.Date);

                foreach (var user in involucrados.Where(u => u != pagador))
                    dt.Rows.Add(g.Description, parte, pagador, user, "Debe", g.Date);
            }

            return dt;
        }

        /// <summary>
        /// Crea el DataTable con el saldo neto por usuario.
        /// </summary>
        public static DataTable CrearDataTableNetos(Dictionary<string, decimal> saldoPorUsuario)
        {
            var dt = new DataTable();
            dt.Columns.Add("Email");
            dt.Columns.Add("Saldo (+ recibe / - debe)", typeof(decimal));

            foreach (var kv in saldoPorUsuario.OrderBy(k => k.Key))
                dt.Rows.Add(kv.Key, kv.Value);

            return dt;
        }

        /// <summary>
        /// Crea el DataTable con la propuesta de deudas entre usuarios.
        /// </summary>
        public DataTable CrearDataTableDeudas(Dictionary<string, decimal> saldoPorUsuario)
        {
            var dt = new DataTable();
            dt.Columns.Add("De");
            dt.Columns.Add("Para");
            dt.Columns.Add("Monto", typeof(decimal));

            var listaPositivos = saldoPorUsuario.Where(k => k.Value > 0)
                                               .OrderBy(k => k.Value)
                                               .Select(k => new { k.Key, Saldo = k.Value })
                                               .ToList();

            var listaNegativos = saldoPorUsuario.Where(k => k.Value < 0)
                                               .OrderBy(k => k.Value)
                                               .Select(k => new { k.Key, Saldo = k.Value })
                                               .ToList();

            foreach (var neg in listaNegativos)
            {
                decimal deudaRestante = -neg.Saldo;

                for (int i = 0; i < listaPositivos.Count; i++)
                {
                    if (deudaRestante <= 0) break;

                    var pos = listaPositivos[i];
                    decimal pago = Math.Min(pos.Saldo, deudaRestante);
                    if (pago <= 0) continue;

                    dt.Rows.Add(neg.Key, pos.Key, pago);
                    listaPositivos[i] = new { Key = pos.Key, Saldo = pos.Saldo - pago };
                    deudaRestante -= pago;
                }
            }

            return dt;
        }

        /// <summary>
        /// Exporta un DataGridView a CSV de manera segura con manejo de excepciones.
        /// </summary>
        private static void ExportarDataGrid(DataGridView dgv, System.IO.StreamWriter writer, string titulo, string[] encabezados)
        {
            writer.WriteLine(titulo);
            writer.WriteLine(string.Join(";", encabezados));

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    var valores = encabezados.Select((_, i) => row.Cells[i].Value?.ToString() ?? "").ToArray();
                    writer.WriteLine(string.Join(";", valores));
                }
            }

            writer.WriteLine();
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
                using var writer = new System.IO.StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8);

                ExportarDataGrid(dgvGastos, writer, "Gastos del grupo", new[] { "Descripción", "Monto", "Pagador", "Usuario", "Efecto", "Fecha" });
                ExportarDataGrid(dgvNetos, writer, "Balance neto por usuario (+ recibe / - debe)", new[] { "Email", "Saldo" });
                ExportarDataGrid(dgvDeudas, writer, "Propuesta de pagos", new[] { "De", "Para", "Monto" });

                writer.Flush();
                MessageBox.Show("CSV exportado correctamente.", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo exportar el CSV: " + ex.Message, "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
