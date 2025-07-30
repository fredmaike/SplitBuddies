using System;
using System.IO;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Views;

namespace SplitBuddies
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            DataManager.Instance.BasePath = Path.Combine(baseDir, "Data");

            DataManager.Instance.LoadUsers();
            DataManager.Instance.LoadGroups();
            DataManager.Instance.LoadExpenses();

            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainForm(loginForm.LoggedInUser));
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
