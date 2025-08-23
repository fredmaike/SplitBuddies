using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    /// <summary>
    /// Contiene la definición visual del formulario de gestión de grupos.
    /// Incluye creación, eliminación, visualización de balances, envío de invitaciones,
    /// selección de imagen para grupo y generación de reportes.
    /// </summary>
    partial class GroupForm
    {
        // --- Controles principales ---
        private ListBox lstGrupos;               // Lista de grupos disponibles
        private Label lblGroupName;              // Etiqueta para nombre del grupo
        private TextBox txtGroupName;            // Texto editable para nombre de grupo
        private Label lblImagePath;              // Etiqueta para ruta de imagen
        private TextBox txtImagePath;            // Campo solo lectura para ruta de imagen
        private Button btnCreateGroup;           // Botón para crear un grupo
        private Button btnDeleteGroup;           // Botón para eliminar un grupo
        private Button btnSelectImage;           // Botón para seleccionar imagen del grupo
        private PictureBox pbGroupImage;         // Vista previa de la imagen del grupo
        private Button btnViewBalances;          // Botón para ver balances del grupo
        private Button btnSendInvitations;       // Botón para enviar invitaciones
        private Button btnExportReport;          // Botón para exportar reportes a CSV

        // --- Controles para filtrado y reporte ---
        private Label lblFrom;                    // Etiqueta "Desde"
        private Label lblTo;                      // Etiqueta "Hasta"
        private DateTimePicker dtpFrom;           // Selector fecha inicio
        private DateTimePicker dtpTo;             // Selector fecha fin
        private Button btnGenerateReport;         // Botón para generar reporte
        private DataGridView dgvReport;           // Grilla con reporte de gastos

        /// <summary>
        /// Inicializa todos los controles del formulario, los ubica y asigna eventos.
        /// Mantiene la separación por secciones para facilitar lectura y mantenimiento.
        /// </summary>
        private void InitializeComponent()
        {
            // ===== Instanciación de controles =====
            lstGrupos = new ListBox();
            lblGroupName = new Label();
            txtGroupName = new TextBox();
            lblImagePath = new Label();
            txtImagePath = new TextBox();
            btnCreateGroup = new Button();
            btnDeleteGroup = new Button();
            btnSelectImage = new Button();
            pbGroupImage = new PictureBox();
            btnViewBalances = new Button();
            btnSendInvitations = new Button();

            lblFrom = new Label();
            lblTo = new Label();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            btnGenerateReport = new Button();
            dgvReport = new DataGridView();

            SuspendLayout();

            // ===== Lista de grupos =====
            lstGrupos.Location = new Point(20, 20);
            lstGrupos.Size = new Size(200, 200);
            lstGrupos.SelectedIndexChanged += LstGrupos_SelectedIndexChanged;

            // ===== PictureBox e info del grupo =====
            pbGroupImage.Location = new Point(240, 20);
            pbGroupImage.Size = new Size(140, 100);
            pbGroupImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbGroupImage.BorderStyle = BorderStyle.FixedSingle;

            lblGroupName.Location = new Point(240, 130);
            lblGroupName.Size = new Size(120, 23);
            lblGroupName.Text = "Nombre del grupo:";

            txtGroupName.Location = new Point(240, 150);
            txtGroupName.Size = new Size(140, 23);

            lblImagePath.Location = new Point(240, 180);
            lblImagePath.Size = new Size(120, 23);
            lblImagePath.Text = "Ruta de imagen:";

            txtImagePath.Location = new Point(240, 200);
            txtImagePath.Size = new Size(140, 23);
            txtImagePath.ReadOnly = true;

            btnSelectImage.Location = new Point(240, 230);
            btnSelectImage.Size = new Size(140, 30);
            btnSelectImage.Text = "Seleccionar imagen";
            btnSelectImage.Click += BtnSelectImage_Click;

            // ===== Botones de grupo =====
            btnCreateGroup.Location = new Point(20, 230);
            btnCreateGroup.Size = new Size(100, 30);
            btnCreateGroup.Text = "Crear grupo";
            btnCreateGroup.Click += btnCreateGroup_Click;

            btnDeleteGroup.Location = new Point(130, 230);
            btnDeleteGroup.Size = new Size(100, 30);
            btnDeleteGroup.Text = "Eliminar grupo";
            btnDeleteGroup.Click += btnDeleteGroup_Click;

            btnViewBalances.Location = new Point(20, 270);
            btnViewBalances.Size = new Size(100, 30);
            btnViewBalances.Text = "Ver balances";
            btnViewBalances.Click += BtnViewBalances_Click;

            btnSendInvitations.Location = new Point(130, 270);
            btnSendInvitations.Size = new Size(100, 30);
            btnSendInvitations.Text = "Enviar invitaciones";
            btnSendInvitations.Click += BtnSendInvitations_Click;

            // ===== Controles de filtrado de reporte =====
            lblFrom.Location = new Point(20, 320);
            lblFrom.Size = new Size(50, 23);
            lblFrom.Text = "Desde:";

            dtpFrom.Location = new Point(80, 320);
            dtpFrom.Size = new Size(150, 23);

            lblTo.Location = new Point(250, 320);
            lblTo.Size = new Size(30, 23);
            lblTo.Text = "Hasta:";

            dtpTo.Location = new Point(290, 320);
            dtpTo.Size = new Size(150, 23);

            btnGenerateReport.Location = new Point(460, 320);
            btnGenerateReport.Size = new Size(120, 23);
            btnGenerateReport.Text = "Generar reporte";
            btnGenerateReport.Click += BtnGenerateReport_Click;

            dgvReport.Location = new Point(20, 350);
            dgvReport.Size = new Size(560, 200);
            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.ReadOnly = true;

            // ===== Botón de exportar reporte =====
            btnExportReport = new Button();
            btnExportReport.Location = new Point(590, 320);
            btnExportReport.Size = new Size(100, 23);
            btnExportReport.Text = "Exportar CSV";
            btnExportReport.Click += BtnExportReport_Click;

            // ===== Configuración final del formulario =====
            ClientSize = new Size(700, 650);
            Controls.AddRange(new Control[]
            {
                lstGrupos, pbGroupImage, lblGroupName, txtGroupName, lblImagePath, txtImagePath,
                btnSelectImage, btnCreateGroup, btnDeleteGroup, btnViewBalances, btnSendInvitations,
                lblFrom, dtpFrom, lblTo, dtpTo, btnGenerateReport, dgvReport
            });
            Controls.Add(btnExportReport);

            Text = "Gestión de Grupos";
            Load += GroupForm_Load;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
