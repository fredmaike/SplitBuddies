using System;
using System.Collections.Generic;
using System.Linq;
using SplitBuddies.Models;

namespace SplitBuddies.Utils
{
    public static class BalanceCalculator
    {
        public static Dictionary<User, decimal> CalcularBalancePorUsuario(Group grupo)
        {
            Dictionary<User, decimal> balance = new();

            // Iniciar solo con los miembros del grupo
            foreach (var user in grupo.Members)
            {
                balance[user] = 0;
            }

            // Recorrer los gastos del grupo
            foreach (var gasto in grupo.Expenses)
            {
                if (gasto.Participants == null || gasto.Participants.Count == 0)
                    continue;

                // Filtrar solo participantes que estén en el grupo
                var participantesValidos = gasto.Participants
                    .Where(p => grupo.Members.Contains(p))
                    .ToList();

                if (participantesValidos.Count == 0)
                    continue;

                decimal montoPorPersona = gasto.Amount / participantesValidos.Count;

                foreach (var participante in participantesValidos)
                {
                    balance[participante] -= montoPorPersona;
                }

                // Sumar al pagador si está en el grupo
                if (gasto.Payer != null && grupo.Members.Contains(gasto.Payer))
                {
                    balance[gasto.Payer] += gasto.Amount;
                }
            }

            return balance;
        }
    }
}
