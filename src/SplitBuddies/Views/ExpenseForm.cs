using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SplitBuddies.Models;
using SplitBuddies.Data;

namespace SplitBuddies.Views
{
    public class ExpenseForm : Form
    {
        private TextBox txtName;
        private TextBox txtAmount;
        private ComboBox cbPayer;
        private CheckedListBox clbParticipants;
        private Button btnAdd;

        public Expense NewExpense { get; private set; }

        private List<User> allUsers;

        public ExpenseForm()
        {
            allUsers = DataLoader.LoadUsers();

            this.Text = "Registrar Gasto";
            this.Size = new Size(350, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblName = new Label();
            lblName.Text = "Nombre del gasto:";
            lblName.Location = new Point(10, 20);
            this.Controls.Add(lblName);

            txtName = new TextBox();
            txtName.Location = new Point(10, 45);
            txtName.Width = 300;
            this.Controls.Add(txtName);

            Label lblAmount = new Label();
            lblAmount.Text = "Monto:";
            lblAmount.Location = new Point(10, 80);
            this.Controls.Add(lblAmount);

            txtAmount = new TextBox();
            txtAmount.Location = new Point(10, 105);
            txtAmount.Width = 300;
            this.Controls.Add(txtAmount);

            Label lblPayer = new Label();
            lblPayer.Text = "Pagado por:";
            lblPayer.Location = new Point(10, 140);
            this.Controls.Add(lblPayer);

            cbPayer = new ComboBox();
            cbPayer.Location = new Point(10, 165);
            cbPayer.Width = 300;
            cbPayer.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var user in allUsers)
                cbPayer.Items.Add(user);
            cbPayer.DisplayMember = "Name";
            this.Controls.Add(cbPayer);

            Label lblParticipants = new Label();
            lblParticipants.Text = "Participantes:";
            lblParticipants.Location = new Point(10, 200);
            this.Controls.Add(lblParticipants);

            clbParticipants = new CheckedListBox();
            clbParticipants.Location = new Point(10, 225);
            clbParticipants.Size = new Size(300, 100);
            foreach (var user in allUsers)
                clbParticipants.Items.Add(user);
            clbParticipants.DisplayMember = "Name";
            this.Controls.Add(clbParticipants);

            btnAdd = new Button();
            btnAdd.Text = "Agregar Gasto";
            btnAdd.Location = new Point(10, 340);
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtAmount.Text) ||
                cbPayer.SelectedItem == null || clbParticipants.CheckedItems.Count == 0 ||
                !decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Complete todos los campos correctamente.");
                return;
            }

            List<User> selectedParticipants = new List<User>();
            foreach (var item in clbParticipants.CheckedItems)
            {
                selectedParticipants.Add((User)item);
            }

            NewExpense = new Expense
            {
                Name = txtName.Text.Trim(),
                Amount = amount,
                Description = "Registrado manualmente",
                Date = DateTime.Now,
                Payer = (User)cbPayer.SelectedItem,
                Participants = selectedParticipants
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
