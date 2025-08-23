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
        /// Calcula el balance neto por usuario dentro de un grupo.
        /// Saldo positivo = acreedor, saldo negativo = deudor.
        /// </summary>
        public static Dictionary<string, decimal> CalcularBalancePorUsuario(Group group)
        {
            var balances = new Dictionary<string, decimal>();
            foreach (var member in group.Members)
            {
                balances[member] = 0m;
            }

            foreach (var expenseId in group.Expenses)
            {
                var expense = DataManager.Instance.Expenses.FirstOrDefault(e => e.Id == expenseId);
                if (expense == null) continue;

                // Solo considerar participantes que están en el grupo
                var participantesValidos = expense.InvolvedUsersEmails.Where(p => group.Members.Contains(p)).ToList();
                if (!participantesValidos.Any()) continue;

                decimal share = expense.Amount / participantesValidos.Count;

                foreach (var p in participantesValidos)
                {
                    if (p == expense.PaidByEmail)
                        balances[p] += expense.Amount - share; // el que paga adelanta el resto
                    else
                        balances[p] -= share; // los demás deben pagar su parte
                }
            }

            return balances;
        }

        /// <summary>
        /// Genera liquidaciones mínimas a partir de los balances netos.
        /// </summary>
        public static List<Settlement> CalcularDeudas(Dictionary<string, decimal> balances)
        {
            var deudores = balances.Where(b => b.Value < 0).Select(b => (b.Key, Amount: -b.Value)).ToList();
            var acreedores = balances.Where(b => b.Value > 0).Select(b => (b.Key, Amount: b.Value)).ToList();
            var settlements = new List<Settlement>();

            int i = 0, j = 0;
            while (i < deudores.Count && j < acreedores.Count)
            {
                var debtor = deudores[i];
                var creditor = acreedores[j];

                decimal pago = Math.Min(debtor.Amount, creditor.Amount);

                settlements.Add(new Settlement(debtor.Key, creditor.Key, pago));

                deudores[i] = (debtor.Key, debtor.Amount - pago);
                acreedores[j] = (creditor.Key, creditor.Amount - pago);

                if (deudores[i].Amount == 0) i++;
                if (acreedores[j].Amount == 0) j++;
            }

            return settlements;
        }


        #region Métodos privados

        private static void AplicarGastoAlBalance(Expense gasto, Dictionary<string, decimal> balance)
        {
            if (gasto == null || gasto.Amount <= 0) return;

            // Solo participantes que son miembros del grupo
            var participantesValidos = (gasto.InvolvedUsersEmails ?? new List<string>())
                                      .Where(email => !string.IsNullOrWhiteSpace(email) && balance.ContainsKey(email))
                                      .ToList();

            if (participantesValidos.Count == 0) return;

            decimal montoPorPersona = gasto.Amount / participantesValidos.Count;

            foreach (var email in participantesValidos)
            {
                balance[email] -= montoPorPersona;
            }

            if (!string.IsNullOrWhiteSpace(gasto.PaidByEmail) && balance.ContainsKey(gasto.PaidByEmail))
                balance[gasto.PaidByEmail] += gasto.Amount;
        }

        private static void ResolverPagos(List<KeyValuePair<string, decimal>> deudores,
                                          List<KeyValuePair<string, decimal>> acreedores,
                                          List<Settlement> deudas)
        {
            int i = 0, j = 0;

            while (i < deudores.Count && j < acreedores.Count)
            {
                var deudor = deudores[i];
                var acreedor = acreedores[j];

                decimal monto = Math.Min(deudor.Value, acreedor.Value);

                if (monto > 0)
                {
                    deudas.Add(new Settlement
                    {
                        FromEmail = deudor.Key,
                        ToEmail = acreedor.Key,
                        Amount = Math.Round(monto, 2, MidpointRounding.AwayFromZero)
                    });

                    deudores[i] = new KeyValuePair<string, decimal>(deudor.Key, deudor.Value - monto);
                    acreedores[j] = new KeyValuePair<string, decimal>(acreedor.Key, acreedor.Value - monto);
                }

                if (deudores[i].Value == 0) i++;
                if (acreedores[j].Value == 0) j++;
            }
        }

        #endregion
    }
}
