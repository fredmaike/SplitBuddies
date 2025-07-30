using System;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public partial class MainForm : Form
    {
        private User currentUser;

        
        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Bienvenido, {currentUser.Name}!";
        }

        private void btnGroups_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            var groupForm = new GroupForm(currentUser);
            groupForm.ShowDialog();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            var expenseForm = new ExpenseForm(currentUser);
            expenseForm.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataManager.Instance.SaveUsers("usuarios.json");
            DataManager.Instance.SaveGroups("grupos.json");
            DataManager.Instance.SaveExpenses("gastos.json");
            MessageBox.Show("Datos guardados.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            var mostrarForm = new MostrarForm();
            mostrarForm.ShowDialog();
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
       
            this.Hide();

            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    currentUser = loginForm.LoggedInUser;
                    lblWelcome.Text = $"Bienvenido, {currentUser.Name}!";
                }
                else
                {
                    
                    Application.Exit();
                }
            }

            this.Show();
        }
    }
}
