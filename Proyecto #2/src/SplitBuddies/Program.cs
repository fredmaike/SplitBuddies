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
            // Habilita estilos visuales para la aplicación Windows Forms
            Application.EnableVisualStyles();

            // Configura el modo de renderizado compatible con versiones anteriores de Windows Forms
            Application.SetCompatibleTextRenderingDefault(false);

            // Obtiene la ruta base de la aplicación y establece la carpeta "Data" para almacenamiento
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            DataManager.Instance.BasePath = Path.Combine(baseDir, "Data");

            // Carga datos desde archivos JSON para usuarios, grupos y gastos
            DataManager.Instance.LoadUsers();
            DataManager.Instance.LoadGroups();
            DataManager.Instance.LoadExpenses();

            // Muestra el formulario de login en modo modal
            using (var loginForm = new LoginForm())
            {
                // Si el login es exitoso (DialogResult.OK), abre la ventana principal con el usuario logueado
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainForm(loginForm.LoggedInUser));
                }
                else
                {
                    // Si se cancela o falla el login, cierra la aplicación
                    Application.Exit();
                }
            }
        }
    }
}
