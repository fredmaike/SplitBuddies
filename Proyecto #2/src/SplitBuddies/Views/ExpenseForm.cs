using System;
using System.Linq;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public partial class ExpenseForm : Form
    {
        private User currentUser;

        public ExpenseForm(User user)
        {
            InitializeComponent();
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            LoadGroups();
        }

        private void LoadGroups()
        {
            var groups = DataManager.Instance.Groups
                .Where(g => g.Members.Contains(currentUser.Email))
                .ToList();

            cmbGroups.DataSource = groups;
            cmbGroups.DisplayMember = nameof(Group.GroupName);
            cmbGroups.SelectedIndex = 0;
        }

        private void CmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedItem is Group selectedGroup)
            {
                cmbPaidBy.DataSource = selectedGroup.Members.ToList();
                clbIncludedMembers.Items.Clear();
                foreach (var member in selectedGroup.Members)
                    clbIncludedMembers.Items.Add(member, true); // todos seleccionados por defecto
            }
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedItem is not Group selectedGroup)
            {
                MessageBox.Show("Seleccione un grupo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Monto inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Debe / Recibe
            string effect = cmbEffect.SelectedItem?.ToString() ?? "Recibe";
            if (effect == "Debe") amount = -Math.Abs(amount); // negativo si debe
            else amount = Math.Abs(amount);                  // positivo si recibe

            var included = clbIncludedMembers.CheckedItems.Cast<string>().ToList();

            if (included.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un miembro involucrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var expense = new Expense
            {
                Id = DataManager.Instance.GetNextExpenseId(),
                GroupId = selectedGroup.GroupId,
                Name = txtExpenseName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                PaidByEmail = cmbPaidBy.SelectedItem.ToString(),
                Amount = amount,
                Date = DateTime.Now,
                InvolvedUsersEmails = included
            };

            DataManager.Instance.Expenses.Add(expense);
            DataManager.Instance.SaveExpenses();

            MessageBox.Show("Gasto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtExpenseName.Clear();
            txtDescription.Clear();
            txtAmount.Clear();
        }
    }
}
