using System;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public partial class MainForm : Form
    {
        // Usuario actualmente autenticado en la aplicación
        private User currentUser;

        // Constructor que recibe el usuario actual y configura el formulario
        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            Load += MainForm_Load; // Evento que se ejecuta al cargar el formulario
        }

        // Evento que se ejecuta cuando el formulario se carga
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Mostrar mensaje de bienvenida con el nombre del usuario actual
            lblWelcome.Text = $"Bienvenido, {currentUser.Name}!";
        }

        // Evento para abrir el formulario de gestión de grupos
        private void btnGroups_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return; 
            var groupForm = new GroupForm(currentUser);
            groupForm.ShowDialog(); 
        }

        // Evento para abrir el formulario de gestión de gastos
        private void btnExpenses_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return; 
            var expenseForm = new ExpenseForm(currentUser);
            expenseForm.ShowDialog();
        }

        // Evento para guardar todos los datos (usuarios, grupos, gastos) en archivos JSON
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataManager.Instance.SaveUsers("usuarios.json");
            DataManager.Instance.SaveGroups("grupos.json");
            DataManager.Instance.SaveExpenses("gastos.json");

            // Mostrar mensaje informando que se guardó correctamente
            MessageBox.Show("Datos guardados.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Evento para mostrar información combinada de grupos y gastos
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            var mostrarForm = new MostrarForm();
            mostrarForm.ShowDialog();
        }

        // Evento para cerrar sesión y volver al formulario de login
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ocultar el formulario actual

            using (var loginForm = new LoginForm())
            {
                // Mostrar formulario de login y esperar resultado
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Si el login fue exitoso, actualizar el usuario actual y el mensaje de bienvenida
                    currentUser = loginForm.LoggedInUser;
                    lblWelcome.Text = $"Bienvenido, {currentUser.Name}!";
                    this.Show(); 
                }
                else
                {
                    // Si se canceló el login, cerrar la aplicación
                    Application.Exit();
                }
            }
        }

        // Evento para abrir un formulario para editar grupos (se asume que existe EditGroupsForm)
        private void btnEditGroups_Click(object sender, EventArgs e)
        {
            var editForm = new EditGroupsForm();
            editForm.ShowDialog();
        }
    }
}
