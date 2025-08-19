using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Controllers
{
    // Controlador responsable de manejar la lógica relacionada con los gastos
    public class ExpenseController
    {
        /// <summary>
        /// Crea y registra un nuevo gasto en el sistema.
        /// Valida que el usuario pagador exista y asigna un ID único al nuevo gasto.
        /// También lo asocia al grupo correspondiente.
        /// </summary>
        public Expense AddExpense(
            string name,
            string description,
            string payerEmail,
            List<string> involvedEmails,
            decimal amount,
            DateTime date,
            int groupId)
        {
            // Buscar el usuario que pagó el gasto
            var payerUser = DataManager.Instance.Users
                .FirstOrDefault(u => u.Email.Equals(payerEmail, StringComparison.OrdinalIgnoreCase));

            // Validar si el usuario pagador existe
            if (payerUser == null)
            {
                MessageBox.Show($"Usuario pagador no encontrado: {payerEmail}",
                    "Error al agregar gasto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Crear el objeto Expense con un ID autogenerado
            var expense = new Expense(name, description, payerUser.Email, involvedEmails, amount, date, groupId)
            {
                Id = DataManager.Instance.Expenses.Count > 0
                    ? DataManager.Instance.Expenses.Max(e => e.Id) + 1
                    : 1
            };

            // Agregar el gasto a la lista global de gastos
            DataManager.Instance.Expenses.Add(expense);

            // Asociar el ID del gasto al grupo correspondiente
            var group = DataManager.Instance.Groups.FirstOrDefault(g => g.GroupId == groupId);
            if (group != null)
                group.Expenses.Add(expense.Id);

            return expense;
        }

        /// <summary>
        /// Retorna todos los gastos asociados a un grupo específico.
        /// </summary>
        public static List<Expense> GetExpensesForGroup(int groupId)
        {
            // Buscar el grupo correspondiente
            var group = DataManager.Instance.Groups.FirstOrDefault(g => g.GroupId == groupId);
            if (group == null) return new List<Expense>();

            // Buscar todos los gastos cuyo ID está listado en el grupo
            return DataManager.Instance.Expenses
                .Where(e => group.Expenses.Contains(e.Id))
                .ToList();
        }
    }
}
