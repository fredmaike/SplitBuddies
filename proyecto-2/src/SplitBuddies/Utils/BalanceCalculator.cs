using System;
using System.Collections.Generic;
using System.Linq;
using SplitBuddies.Models;
using SplitBuddies.Data;

namespace SplitBuddies.Utils
{
    /// <summary>
    /// Utilidad estática para calcular balances y generar liquidaciones
    /// dentro de grupos de usuarios.
    /// </summary>
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
    }
}
