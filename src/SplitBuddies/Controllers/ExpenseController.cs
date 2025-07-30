using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Controllers
{
    public class ExpenseController
    {
        public Expense AddExpense(
       string name,
       string description,
       string payerEmail,
       List<string> involvedEmails,
       decimal amount,
       DateTime date,
       int groupId)
        {
            var payerUser = DataManager.Instance.Users
                .FirstOrDefault(u => u.Email.Equals(payerEmail, StringComparison.OrdinalIgnoreCase));

            if (payerUser == null)
            {
                MessageBox.Show($"Usuario pagador no encontrado: {payerEmail}",
                    "Error al agregar gasto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var expense = new Expense(name, description, payerUser.Email, involvedEmails, amount, date, groupId)
            {
                Id = DataManager.Instance.Expenses.Count > 0
                    ? DataManager.Instance.Expenses.Max(e => e.Id) + 1
                    : 1
            };

            DataManager.Instance.Expenses.Add(expense);

            var group = DataManager.Instance.Groups.FirstOrDefault(g => g.GroupId == groupId);
            if (group != null)
                group.Expenses.Add(expense.Id);

            return expense;
        }

        public List<Expense> GetExpensesForGroup(int groupId)
        {
            var group = DataManager.Instance.Groups.FirstOrDefault(g => g.GroupId == groupId);
            if (group == null) return new List<Expense>();
            return DataManager.Instance.Expenses
                .Where(e => group.Expenses.Contains(e.Id))
                .ToList();
        }
    }
}
