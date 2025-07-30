using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class ExpenseForm
    {
        // Etiqueta para seleccionar el grupo al que pertenece el gasto
        private Label lblGroup;
        private ComboBox cmbGroups; 

        // Etiqueta y ComboBox para seleccionar quién pagó el gasto
        private Label lblPaidBy;
        private ComboBox cmbPaidBy;

        // Etiqueta y CheckedListBox para seleccionar los miembros incluidos en el gasto
        private Label lblIncludedMembers;
        private CheckedListBox clbIncludedMembers;

        // Etiqueta y TextBox para ingresar el nombre del gasto
        private Label lblExpenseName;
        private TextBox txtExpenseName;

        // Etiqueta y TextBox para la descripción del gasto
        private Label lblDescription;
        private TextBox txtDescription;

        // Etiqueta y TextBox para ingresar el monto del gasto
        private Label lblAmount;
        private TextBox txtAmount;

        // Botón para agregar el gasto con los datos ingresados
        private Button btnAddExpense;

        // Método para inicializar y configurar todos los controles del formulario
        private void InitializeComponent()
        {
            this.Text = "Agregar Gasto";
            this.Size = new Size(400, 500);

            // Configuración de etiqueta y comboBox para grupos
            lblGroup = new Label() { Text = "Grupo:", Left = 25, Top = 20, Width = 100 };
            cmbGroups = new ComboBox() { Left = 140, Top = 20, Width = 200 };
            cmbGroups.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroups.SelectedIndexChanged += new System.EventHandler(this.CmbGroups_SelectedIndexChanged);

            // Configuración de etiqueta y comboBox para "Pagado por"
            lblPaidBy = new Label() { Text = "Pagado por:", Left = 25, Top = 60, Width = 100 };
            cmbPaidBy = new ComboBox() { Left = 140, Top = 60, Width = 200 };
            cmbPaidBy.DropDownStyle = ComboBoxStyle.DropDownList;

            // Configuración de etiqueta y CheckedListBox para miembros incluidos
            lblIncludedMembers = new Label() { Text = "Miembros incluidos:", Left = 25, Top = 100, Width = 120 };
            clbIncludedMembers = new CheckedListBox() { Left = 140, Top = 100, Width = 200, Height = 100 };

            // Configuración de etiqueta y TextBox para nombre del gasto
            lblExpenseName = new Label() { Text = "Nombre:", Left = 25, Top = 210, Width = 100 };
            txtExpenseName = new TextBox() { Left = 140, Top = 210, Width = 200 };

            // Configuración de etiqueta y TextBox para descripción
            lblDescription = new Label() { Text = "Descripción:", Left = 25, Top = 250, Width = 100 };
            txtDescription = new TextBox() { Left = 140, Top = 250, Width = 200 };

            // Configuración de etiqueta y TextBox para monto
            lblAmount = new Label() { Text = "Monto:", Left = 25, Top = 330, Width = 100 };
            txtAmount = new TextBox() { Left = 140, Top = 330, Width = 200 };

            // Configuración del botón para agregar gasto
            btnAddExpense = new Button() { Text = "Agregar Gasto", Left = 140, Top = 380, Width = 120 };
            btnAddExpense.Click += new System.EventHandler(this.btnAddExpense_Click);

            // Añadir controles al formulario
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
