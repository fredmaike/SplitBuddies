using System;
using System.Collections.Generic;
using System.Linq;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Controllers
{
    public class ExpenseController
    {
        /// <summary>
        /// Crea y registra un nuevo gasto en el sistema.
        /// </summary>
        public Expense AddExpense(
            string name,
            string description,
            string paidByEmail,
            List<string> participants,
            decimal amount,
            DateTime date,
            int groupId)
        {
            var group = DataManager.Instance.Groups.FirstOrDefault(g => g.GroupId == groupId);
            if (group == null) return null;

            var payer = DataManager.Instance.Users.FirstOrDefault(u => u.Email == paidByEmail);
            if (payer == null) return null;

            if (participants.Any(p => !group.Members.Contains(p))) return null;

            var expense = new Expense
            {
                Id = DataManager.Instance.GetNextExpenseId(),
                Name = name,
                Description = description,
                PaidByEmail = paidByEmail,
                InvolvedUsersEmails = participants,
                Amount = amount,
                Date = date,
                GroupId = groupId
            };

            DataManager.Instance.Expenses.Add(expense);
            group.Expenses.Add(expense.Id);

            return expense;
        }
    }
}