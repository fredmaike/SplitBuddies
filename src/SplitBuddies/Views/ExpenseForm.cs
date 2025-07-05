using SplitBuddies.Controllers;
using SplitBuddies.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SplitBuddies.Views
{
    public partial class ExpenseForm : Form
    {
        private Group currentGroup;

        public ExpenseForm(Group group)
        {
            InitializeComponent();
            currentGroup = group;

            // Llenar combo de pagadores
            foreach (var user in group.Members)
            {
                cbPayer.Items.Add(user);
                clbParticipants.Items.Add(user);
            }

            cbPayer.DisplayMember = "Name";
        }


        private void btnAddExpense_Click_1(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string description = txtDescription.Text.Trim();
            string amountText = txtAmount.Text.Trim();
            decimal amount;

            if (string.IsNullOrEmpty(name) || !decimal.TryParse(amountText, out amount) || cbPayer.SelectedItem == null || clbParticipants.CheckedItems.Count == 0)
            {
                MessageBox.Show("Por favor complete todos los campos correctamente.");
                return;
            }

            List<User> participants = new List<User>();
            foreach (var item in clbParticipants.CheckedItems)
            {
                participants.Add((User)item);
            }

            Expense newExpense = new Expense
            {
                Name = name,
                Description = description,
                Amount = amount,
                Payer = (User)cbPayer.SelectedItem,
                Participants = participants,
                Date = DateTime.Now
            };

            ExpenseController.AddExpense(currentGroup, newExpense);
            MessageBox.Show("Gasto registrado.");
            this.Close();
        }
    }
}
