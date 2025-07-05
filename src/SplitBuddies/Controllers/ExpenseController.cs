using SplitBuddies.Models;
using SplitBuddies.Data;

namespace SplitBuddies.Controllers
{
    public static class ExpenseController
    {
        public static void AddExpense(Group group, Expense expense)
        {
            group.Expenses.Add(expense);
            GroupController.SaveAllGroups(); // Guarda cambios
        }
    }
}
