using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class ExpenseForm
    {
        private Label lblGroup;
        private ComboBox cmbGroups;

        private Label lblPaidBy;
        private ComboBox cmbPaidBy;

        private Label lblIncludedMembers;
        private CheckedListBox clbIncludedMembers;

        private Label lblExpenseName;
        private TextBox txtExpenseName;

        private Label lblDescription;
        private TextBox txtDescription;

        private Label lblAmount;
        private TextBox txtAmount;

        private Button btnAddExpense;

        private void InitializeComponent()
        {
            this.Text = "Agregar Gasto";
            this.Size = new Size(400, 500);

            lblGroup = new Label() { Text = "Grupo:", Left = 25, Top = 20, Width = 100 };
            cmbGroups = new ComboBox() { Left = 140, Top = 20, Width = 200 };
            cmbGroups.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroups.SelectedIndexChanged += new System.EventHandler(this.CmbGroups_SelectedIndexChanged);

            lblPaidBy = new Label() { Text = "Pagado por:", Left = 25, Top = 60, Width = 100 };
            cmbPaidBy = new ComboBox() { Left = 140, Top = 60, Width = 200 };
            cmbPaidBy.DropDownStyle = ComboBoxStyle.DropDownList;

            lblIncludedMembers = new Label() { Text = "Miembros incluidos:", Left = 25, Top = 100, Width = 120 };
            clbIncludedMembers = new CheckedListBox() { Left = 140, Top = 100, Width = 200, Height = 100 };

            lblExpenseName = new Label() { Text = "Nombre:", Left = 25, Top = 210, Width = 100 };
            txtExpenseName = new TextBox() { Left = 140, Top = 210, Width = 200 };

            lblDescription = new Label() { Text = "Descripción:", Left = 25, Top = 250, Width = 100 };
            txtDescription = new TextBox() { Left = 140, Top = 250, Width = 200 };

            lblAmount = new Label() { Text = "Monto:", Left = 25, Top = 330, Width = 100 };
            txtAmount = new TextBox() { Left = 140, Top = 330, Width = 200 };

            btnAddExpense = new Button() { Text = "Agregar Gasto", Left = 140, Top = 380, Width = 120 };
            btnAddExpense.Click += new System.EventHandler(this.btnAddExpense_Click);

            this.Controls.Add(lblGroup);
            this.Controls.Add(cmbGroups);
            this.Controls.Add(lblPaidBy);
            this.Controls.Add(cmbPaidBy);
            this.Controls.Add(lblIncludedMembers);
            this.Controls.Add(clbIncludedMembers);
            this.Controls.Add(lblExpenseName);
            this.Controls.Add(txtExpenseName);
            this.Controls.Add(lblDescription);
            this.Controls.Add(txtDescription);
            this.Controls.Add(lblAmount);
            this.Controls.Add(txtAmount);
            this.Controls.Add(btnAddExpense);
        }
    }
}
