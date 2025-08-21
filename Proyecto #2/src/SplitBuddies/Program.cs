using System;
using System.IO;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Views;

namespace SplitBuddies
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Inicialización moderna de WinForms (.NET 6+)
            // (Si tu proyecto no tiene ApplicationConfiguration, puedes volver a EnableVisualStyles + SetCompatibleTextRenderingDefault)
            try
            {
                ApplicationConfiguration.Initialize();
            }
            catch
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            }

            try
            {
                // 1) Preparar carpeta Data junto al ejecutable
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string dataDir = Path.Combine(baseDir, "Data");
                if (!Directory.Exists(dataDir))
                    Directory.CreateDirectory(dataDir);

                // 2) Configurar DataManager y cargar datos
                var dm = DataManager.Instance;
                dm.BasePath = dataDir;

                dm.LoadUsers();
                dm.LoadGroups();
                dm.LoadExpenses();
                dm.LoadInvitations(); // <-- añadimos invitaciones

                // 3) Mostrar Login como modal
                using (var loginForm = new LoginForm())
                {
                    var result = loginForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        // Importante:
                        // - Mantengo tu flujo actual: abrir MainForm con el usuario logueado
                        // - Si prefieres abrir Dashboard directamente, aquí podrías usar:
                        //   Application.Run(new DashboardForm(loginForm.LoggedInUser.Email));
                        Application.Run(new MainForm(loginForm.LoggedInUser));
                    }
                    else
                    {
                        // Cancelado o falló: cierra limpio
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                // Falla inesperada: mostrar y salir
                MessageBox.Show(
                    "Ocurrió un error al iniciar la aplicación:\n\n" + ex.Message,
                    "SplitBuddies - Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                Environment.Exit(1);
            }
        }
    }
}
