using System;
using System.Collections.Generic;
using System.Linq;
using SplitBuddies.Models;
using SplitBuddies.Data;

namespace SplitBuddies.Utils
{
    public static class BalanceCalculator
    {
        /// <summary>
        /// Calcula el balance por usuario (clave = email) dentro de un grupo.
        /// Usa los gastos registrados en DataManager.
        /// </summary>
        public static Dictionary<string, decimal> CalcularBalancePorUsuario(Group grupo)
        {
            var dm = DataManager.Instance;
            var balance = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

            if (grupo == null) return balance;

            // Inicializa los miembros del grupo en 0
            foreach (var email in (grupo.Members ?? new List<string>()).Where(e => !string.IsNullOrWhiteSpace(e)))
            {
                if (!balance.ContainsKey(email))
                    balance[email] = 0m;
            }

            // Obtener gastos del grupo
            IEnumerable<Expense> gastosGrupo;
            if (grupo.Expenses != null && grupo.Expenses.Count > 0)
                gastosGrupo = dm.Expenses.Where(e => grupo.Expenses.Contains(e.Id));
            else
                gastosGrupo = dm.Expenses.Where(e => e.GroupId == grupo.GroupId);

            foreach (var gasto in gastosGrupo)
            {
                if (gasto == null || gasto.Amount <= 0) continue;

                var participantes = (gasto.InvolvedUsersEmails ?? new List<string>())
                                    .Where(e => !string.IsNullOrWhiteSpace(e))
                                    .ToList();
                if (participantes.Count == 0) continue;

                decimal montoPorPersona = gasto.Amount / participantes.Count;

                // Cada participante debe su parte
                foreach (var email in participantes)
                {
                    if (!balance.ContainsKey(email))
                        balance[email] = 0m;

                    balance[email] -= montoPorPersona;
                }

                // El pagador recibe el total
                if (!string.IsNullOrWhiteSpace(gasto.PaidByEmail))
                {
                    if (!balance.ContainsKey(gasto.PaidByEmail))
                        balance[gasto.PaidByEmail] = 0m;

                    balance[gasto.PaidByEmail] += gasto.Amount;
                }
            }

            return balance;
        }

        /// <summary>
        /// A partir de un diccionario de netos (email → saldo),
        /// genera transferencias mínimas entre deudores y acreedores.
        /// </summary>
        public static List<Settlement> CalcularDeudas(Dictionary<string, decimal> netos)
        {
            var deudas = new List<Settlement>();

            if (netos == null || netos.Count == 0)
                return deudas;

            var debtors = netos
                .Where(kv => kv.Value < 0)
                .Select(kv => new KeyValuePair<string, decimal>(kv.Key, Math.Abs(kv.Value)))
                .OrderBy(kv => kv.Value)
                .ToList();

            var creditors = netos
                .Where(kv => kv.Value > 0)
                .Select(kv => new KeyValuePair<string, decimal>(kv.Key, kv.Value))
                .OrderBy(kv => kv.Value)
                .ToList();

            int i = 0, j = 0;
            while (i < debtors.Count && j < creditors.Count)
            {
                var d = debtors[i];
                var c = creditors[j];

                decimal pay = Math.Min(d.Value, c.Value);
                if (pay > 0)
                {
                    deudas.Add(new Settlement
                    {
                        FromEmail = d.Key,
                        ToEmail = c.Key,
                        Amount = Math.Round(pay, 2, MidpointRounding.AwayFromZero)
                    });

                    debtors[i] = new KeyValuePair<string, decimal>(d.Key, d.Value - pay);
                    creditors[j] = new KeyValuePair<string, decimal>(c.Key, c.Value - pay);
                }

                if (debtors[i].Value == 0) i++;
                if (creditors[j].Value == 0) j++;
            }

            return deudas;
        }
    }
}
