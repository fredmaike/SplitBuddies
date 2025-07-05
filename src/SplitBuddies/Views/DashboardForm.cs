using System;
using System.Windows.Forms;
using SplitBuddies.Models;

namespace SplitBuddies.Views
{
    public partial class DashboardForm : Form
    {
        private User currentUser;

        public DashboardForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            lblWelcome.Text = $"¡Hola, {user.Name}!";
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }
    }
}
