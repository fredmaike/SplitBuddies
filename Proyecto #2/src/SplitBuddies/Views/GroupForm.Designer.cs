using System.Drawing;
using System.Windows.Forms;

namespace SplitBuddies.Views
{
    partial class GroupForm
    {
        // Controles del formulario
        private ListBox lstGrupos;
        private Label lblGroupName;
        private TextBox txtGroupName;
        private Label lblImagePath;
        private TextBox txtImagePath;
        private Button btnCreateGroup;
        private Button btnDeleteGroup;
        private Button btnSelectImage;
        private PictureBox pbGroupImage;
        private Button btnViewBalances;
        private Button btnSendInvitations;

        // Método para inicializar y configurar todos los controles y la interfaz
        private void InitializeComponent()
        {
            // Crear instancias de los controles
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

            SuspendLayout();

            // Configuración de la lista de grupos
            lstGrupos.Location = new Point(20, 20);
            lstGrupos.Size = new Size(200, 200);
            lstGrupos.SelectedIndexChanged += LstGrupos_SelectedIndexChanged;

            // Configuración del PictureBox para la imagen del grupo
            pbGroupImage.Location = new Point(240, 20);
            pbGroupImage.Size = new Size(140, 100);
            pbGroupImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbGroupImage.BorderStyle = BorderStyle.FixedSingle;

            // Etiqueta para el campo nombre del grupo
            lblGroupName.Location = new Point(240, 130);
            lblGroupName.Size = new Size(120, 23);
            lblGroupName.Text = "Nombre del grupo:";

            // TextBox para ingresar el nombre del grupo
            txtGroupName.Location = new Point(240, 150);
            txtGroupName.Size = new Size(140, 23);

            // Etiqueta para la ruta de la imagen
            lblImagePath.Location = new Point(240, 180);
            lblImagePath.Size = new Size(120, 23);
            lblImagePath.Text = "Ruta de imagen:";

            // TextBox solo lectura para mostrar la ruta de la imagen seleccionada
            txtImagePath.Location = new Point(240, 200);
            txtImagePath.Size = new Size(140, 23);
            txtImagePath.ReadOnly = true;

            // Botón para abrir el diálogo de selección de imagen
            btnSelectImage.Location = new Point(240, 230);
            btnSelectImage.Size = new Size(140, 30);
            btnSelectImage.Text = "Seleccionar imagen";
            btnSelectImage.Click += BtnSelectImage_Click;

            // Botón para crear un nuevo grupo
            btnCreateGroup.Location = new Point(20, 230);
            btnCreateGroup.Size = new Size(100, 30);
            btnCreateGroup.Text = "Crear grupo";
            btnCreateGroup.Click += btnCreateGroup_Click;

            // Botón para eliminar el grupo seleccionado
            btnDeleteGroup.Location = new Point(130, 230);
            btnDeleteGroup.Size = new Size(100, 30);
            btnDeleteGroup.Text = "Eliminar grupo";
            btnDeleteGroup.Click += btnDeleteGroup_Click;

            //  Botón para ver balances
            btnViewBalances.Location = new Point(20, 270);
            btnViewBalances.Size = new Size(100, 30);
            btnViewBalances.Text = "Ver balances";
            btnViewBalances.Click += BtnViewBalances_Click;

              // Botón para enviar invitaciones
            btnSendInvitations.Location = new Point(130, 270);
            btnSendInvitations.Size = new Size(100, 30);
            btnSendInvitations.Text = "Enviar invitaciones";
            btnSendInvitations.Click += BtnSendInvitations_Click;

            // Configuración general del formulario
            ClientSize = new Size(420, 360); // Aumentamos tamaño para que quepan los botones
            Controls.Add(lstGrupos);
            Controls.Add(pbGroupImage);
            Controls.Add(lblGroupName);
            Controls.Add(txtGroupName);
            Controls.Add(lblImagePath);
            Controls.Add(txtImagePath);
            Controls.Add(btnSelectImage);
            Controls.Add(btnCreateGroup);
            Controls.Add(btnDeleteGroup);
            Controls.Add(btnViewBalances);   
            Controls.Add(btnSendInvitations); 
            Text = "Gestión de Grupos";

            Load += GroupForm_Load;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}