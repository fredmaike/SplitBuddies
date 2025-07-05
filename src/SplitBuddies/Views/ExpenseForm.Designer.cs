
namespace SplitBuddies.Views
{
    partial class ExpenseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtName = new TextBox();
            txtDescription = new TextBox();
            txtAmount = new TextBox();
            cbPayer = new ComboBox();
            clbParticipants = new CheckedListBox();
            btnAddExpense = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 6);
            label1.Name = "label1";
            label1.Size = new Size(130, 20);
            label1.TabIndex = 0;
            label1.Text = "Nombre del gasto";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(56, 114);
            label2.Name = "label2";
            label2.Size = new Size(53, 20);
            label2.TabIndex = 1;
            label2.Text = "Monto";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(204, 9);
            label3.Name = "label3";
            label3.Size = new Size(87, 20);
            label3.TabIndex = 2;
            label3.Text = "Descripcion";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(220, 114);
            label4.Name = "label4";
            label4.Size = new Size(86, 20);
            label4.TabIndex = 3;
            label4.Text = "Pagado por";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(476, 9);
            label5.Name = "label5";
            label5.Size = new Size(93, 20);
            label5.TabIndex = 4;
            label5.Text = "Principiantes";
            // 
            // txtName
            // 
            txtName.Location = new Point(28, 47);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 5;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(204, 47);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(125, 27);
            txtDescription.TabIndex = 6;
            // 
            // txtAmount
            // 
            txtAmount.Location = new Point(12, 161);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(125, 27);
            txtAmount.TabIndex = 7;
            // 
            // cbPayer
            // 
            cbPayer.FormattingEnabled = true;
            cbPayer.Location = new Point(204, 161);
            cbPayer.Name = "cbPayer";
            cbPayer.Size = new Size(151, 28);
            cbPayer.TabIndex = 10;
            // 
            // clbParticipants
            // 
            clbParticipants.FormattingEnabled = true;
            clbParticipants.Location = new Point(476, 47);
            clbParticipants.Name = "clbParticipants";
            clbParticipants.Size = new Size(150, 114);
            clbParticipants.TabIndex = 11;
            // 
            // btnAddExpense
            // 
            btnAddExpense.Location = new Point(234, 327);
            btnAddExpense.Name = "btnAddExpense";
            btnAddExpense.Size = new Size(94, 29);
            btnAddExpense.TabIndex = 12;
            btnAddExpense.Text = "Guardar";
            btnAddExpense.UseVisualStyleBackColor = true;
            btnAddExpense.Click += btnAddExpense_Click_1;
            // 
            // ExpenseForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnAddExpense);
            Controls.Add(clbParticipants);
            Controls.Add(cbPayer);
            Controls.Add(txtAmount);
            Controls.Add(txtDescription);
            Controls.Add(txtName);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ExpenseForm";
            Text = "ExpenseForm";
            Load += ExpenseForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void ExpenseForm_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtName;
        private TextBox txtDescription;
        private TextBox txtAmount;
        private ComboBox cbPayer;
        private CheckedListBox clbParticipants;
        private Button btnAddExpense;
    }
}