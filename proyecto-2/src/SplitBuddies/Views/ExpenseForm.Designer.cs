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

        private Label lblEffect; // Debe/Recibe
        private ComboBox cmbEffect;

        private Button btnAddExpense;

        /// <summary>
        /// Inicializa los controles y la UI del formulario.
        /// </summary>
        private void InitializeComponent()
        {
            this.Text = "Agregar Gasto";
            this.Size = new Size(400, 500);

            InicializarControles();
            AgregarControlesAlFormulario();
        }

        /// <summary>
        /// Configura y posiciona los controles del formulario.
        /// </summary>
        private void InicializarControles()
        {
            // Grupo
            lblGroup = CrearLabel("Grupo:", 25, 20, 100);
            cmbGroups = CrearComboBox(140, 20, 200);
            cmbGroups.SelectedIndexChanged += CmbGroups_SelectedIndexChanged;

            // Pagado por
            lblPaidBy = CrearLabel("Pagado por:", 25, 60, 100);
            cmbPaidBy = CrearComboBox(140, 60, 200);

            // Miembros incluidos
            lblIncludedMembers = CrearLabel("Miembros incluidos:", 25, 100, 120);
            clbIncludedMembers = new CheckedListBox { Left = 140, Top = 100, Width = 200, Height = 100 };

            // Nombre del gasto
            lblExpenseName = CrearLabel("Nombre:", 25, 210, 100);
            txtExpenseName = new TextBox { Left = 140, Top = 210, Width = 200 };

            // Descripción
            lblDescription = CrearLabel("Descripción:", 25, 250, 100);
            txtDescription = new TextBox { Left = 140, Top = 250, Width = 200 };

            // Monto
            lblAmount = CrearLabel("Monto:", 25, 290, 100);
            txtAmount = new TextBox { Left = 140, Top = 290, Width = 200 };

            // Debe / Recibe
            lblEffect = CrearLabel("Efecto:", 25, 330, 100);
            cmbEffect = CrearComboBox(140, 330, 200);
            cmbEffect.Items.AddRange(new string[] { "Debe", "Recibe" });
            cmbEffect.SelectedIndex = 0;

            // Botón agregar
            btnAddExpense = new Button { Text = "Agregar Gasto", Left = 140, Top = 380, Width = 120 };
            btnAddExpense.Click += btnAddExpense_Click;
        }

        /// <summary>
        /// Agrega los controles al formulario.
        /// </summary>
        private void AgregarControlesAlFormulario()
        {
            this.Controls.AddRange(new Control[]
            {
                lblGroup, cmbGroups,
                lblPaidBy, cmbPaidBy,
                lblIncludedMembers, clbIncludedMembers,
                lblExpenseName, txtExpenseName,
                lblDescription, txtDescription,
                lblAmount, txtAmount,
                lblEffect, cmbEffect,
                btnAddExpense
            });
        }

        /// <summary>
        /// Crea un Label con propiedades básicas.
        /// </summary>
        private Label CrearLabel(string texto, int left, int top, int width)
        {
            return new Label
            {
                Text = texto,
                Left = left,
                Top = top,
                Width = width
            };
        }

        /// <summary>
        /// Crea un ComboBox con propiedades básicas.
        /// </summary>
        private ComboBox CrearComboBox(int left, int top, int width)
        {
            return new ComboBox
            {
                Left = left,
                Top = top,
                Width = width,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }
    }
}
