using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Formulario para editar grupos y sus gastos.
    /// Permite modificar nombre de grupo, miembros, y gestionar los gastos asociados.
    /// </summary>
    partial class EditGroupsForm
    {
        /// <summary>
        /// Lista de grupos existentes.
        /// </summary>
        private ListBox listBoxGroups;

        /// <summary>
        /// TextBox para editar el nombre del grupo seleccionado.
        /// </summary>
        private TextBox txtGroupName;

        /// <summary>
        /// TextBox para editar los miembros del grupo, separados por comas.
        /// </summary>
        private TextBox txtMembers;

        /// <summary>
        /// Label del campo nombre del grupo.
        /// </summary>
        private Label lblName;

        /// <summary>
        /// Label del campo miembros del grupo.
        /// </summary>
        private Label lblMembers;

        /// <summary>
        /// Botón para guardar cambios del grupo.
        /// </summary>
        private Button btnSaveChanges;

        /// <summary>
        /// Lista de gastos del grupo seleccionado.
        /// </summary>
        private ListBox listBoxExpenses;

        /// <summary>
        /// TextBox para el nombre del gasto.
        /// </summary>
        private TextBox txtExpenseName;

        /// <summary>
        /// TextBox para el monto del gasto.
        /// </summary>
        private TextBox txtAmount;

        /// <summary>
        /// TextBox para la descripción del gasto.
        /// </summary>
        private TextBox txtDescription;

        /// <summary>
        /// CheckedListBox con los miembros que participaron en el gasto.
        /// </summary>
        private CheckedListBox clbMembersPaid;

        /// <summary>
        /// Botón para editar un gasto seleccionado.
        /// </summary>
        private Button btnEditExpense;

        /// <summary>
        /// Botón para borrar un gasto seleccionado.
        /// </summary>
        private Button btnDeleteExpense;

        /// <summary>
        /// Inicializa todos los controles del formulario.
        /// </summary>
        private void InitializeComponent()
        {
            // Inicializar todos los controles
            InicializarControlesGrupo();
            InicializarControlesGasto();

            // Configuración general del formulario
            ClientSize = new System.Drawing.Size(470, 500);
            Controls.Add(listBoxGroups);
            Controls.Add(lblName);
            Controls.Add(txtGroupName);
            Controls.Add(lblMembers);
            Controls.Add(txtMembers);
            Controls.Add(btnSaveChanges);
            Controls.Add(listBoxExpenses);
            Controls.Add(txtExpenseName);
            Controls.Add(txtAmount);
            Controls.Add(txtDescription);
            Controls.Add(clbMembersPaid);
            Controls.Add(btnEditExpense);
            Controls.Add(btnDeleteExpense);

            Text = "Editar Grupos y Gastos";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            ResumeLayout(false);
            PerformLayout();
        }

        /// <summary>
        /// Inicializa los controles relacionados con la edición de grupos.
        /// </summary>
        private void InicializarControlesGrupo()
        {
            listBoxGroups = new ListBox
            {
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(200, 150)
            };
            listBoxGroups.SelectedIndexChanged += listBoxGroups_SelectedIndexChanged;

            lblName = new Label
            {
                Text = "Nombre del Grupo:",
                Location = new System.Drawing.Point(240, 20),
                AutoSize = true
            };

            txtGroupName = new TextBox
            {
                Location = new System.Drawing.Point(240, 40),
                Size = new System.Drawing.Size(200, 23)
            };

            lblMembers = new Label
            {
                Text = "Miembros (separados por coma):",
                Location = new System.Drawing.Point(240, 70),
                AutoSize = true
            };

            txtMembers = new TextBox
            {
                Location = new System.Drawing.Point(240, 90),
                Size = new System.Drawing.Size(200, 23)
            };

            btnSaveChanges = new Button
            {
                Text = "Guardar Cambios",
                Location = new System.Drawing.Point(240, 130),
                Size = new System.Drawing.Size(200, 30)
            };
            btnSaveChanges.Click += btnSaveChanges_Click;
        }

        /// <summary>
        /// Inicializa los controles relacionados con la edición de gastos.
        /// </summary>
        private void InicializarControlesGasto()
        {
            listBoxExpenses = new ListBox
            {
                Location = new System.Drawing.Point(20, 180),
                Size = new System.Drawing.Size(200, 150)
            };
            listBoxExpenses.SelectedIndexChanged += listBoxExpenses_SelectedIndexChanged;

            txtExpenseName = new TextBox
            {
                Location = new System.Drawing.Point(240, 180),
                Size = new System.Drawing.Size(200, 23),
                PlaceholderText = "Nombre del gasto"
            };

            txtAmount = new TextBox
            {
                Location = new System.Drawing.Point(240, 210),
                Size = new System.Drawing.Size(200, 23),
                PlaceholderText = "Monto"
            };

            txtDescription = new TextBox
            {
                Location = new System.Drawing.Point(240, 240),
                Size = new System.Drawing.Size(200, 60),
                Multiline = true,
                PlaceholderText = "Descripción del gasto"
            };

            clbMembersPaid = new CheckedListBox
            {
                Location = new System.Drawing.Point(240, 310),
                Size = new System.Drawing.Size(200, 80)
            };

            btnEditExpense = new Button
            {
                Text = "Editar Gasto",
                Location = new System.Drawing.Point(240, 400),
                Size = new System.Drawing.Size(200, 30)
            };
            btnEditExpense.Click += btnEditExpense_Click;

            btnDeleteExpense = new Button
            {
                Text = "Borrar Gasto",
                Location = new System.Drawing.Point(240, 440),
                Size = new System.Drawing.Size(200, 30)
            };
            btnDeleteExpense.Click += btnDeleteExpense_Click;
        }
    }
}
