using SplitBuddies.Data;
using SplitBuddies.Views;
using System;
using System.Windows.Forms;

namespace SplitBuddies
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           
            DataManager.Instance.BasePath = @"E:\SplitBuddies-master\SplitBuddies-master\src\SplitBuddies\Data\";

          
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
