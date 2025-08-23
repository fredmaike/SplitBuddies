using System;
using System.IO;
using System.Windows.Forms;
using SplitBuddies.Data;
using SplitBuddies.Views;

namespace SplitBuddies
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada de la aplicación SplitBuddies.
        /// Inicializa WinForms, asegura la carpeta de datos, carga los datos y muestra el login.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeWinForms();

            try
            {
                PrepareDataDirectory();
                LoadApplicationData();
                ShowLoginAndStartMainForm();
            }
            catch (Exception ex)
            {
                ShowFatalError(ex);
            }
        }

        #region Helpers

        /// <summary>
        /// Inicializa la configuración de WinForms (.NET 6+ o compatibilidad antigua).
        /// </summary>
        private static void InitializeWinForms()
        {
            try
            {
                ApplicationConfiguration.Initialize();
            }
            catch
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
            }
        }

        /// <summary>
        /// Asegura que exista la carpeta "Data" y asigna la ruta al DataManager.
        /// </summary>
        private static void PrepareDataDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dataDirectory = Path.Combine(baseDirectory, "Data");

            if (!Directory.Exists(dataDirectory))
                Directory.CreateDirectory(dataDirectory);

            DataManager.Instance.BasePath = dataDirectory;
        }

        /// <summary>
        /// Carga todos los datos necesarios al inicio de la aplicación.
        /// </summary>
        private static void LoadApplicationData()
        {
            var dm = DataManager.Instance;
            dm.LoadUsers();
            dm.LoadGroups();
            dm.LoadExpenses();

            // Intentar cargar invitaciones; se ignora si no existe el archivo
            try
            {
                dm.LoadInvitations();
            }
            catch (FileNotFoundException)
            {
                // Archivo de invitaciones aún no existe, se ignora
            }
            catch (Exception ex)
            {
                // Log opcional para otros errores
                Console.WriteLine("Error al cargar invitaciones: " + ex.Message);
            }
        }

        /// <summary>
        /// Muestra el formulario de login y, si el usuario se autentica, abre MainForm.
        /// </summary>
        private static void ShowLoginAndStartMainForm()
        {
            using var loginForm = new LoginForm();
            var dialogResult = loginForm.ShowDialog();

            if (dialogResult == DialogResult.OK && loginForm.LoggedInUser != null)
            {
                Application.Run(new MainForm(loginForm.LoggedInUser));
            }
            else
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Muestra un mensaje de error crítico y cierra la aplicación.
        /// </summary>
        private static void ShowFatalError(Exception ex)
        {
            MessageBox.Show(
                "Ocurrió un error al iniciar la aplicación:\n\n" + ex.Message,
                "SplitBuddies - Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
            Environment.Exit(1);
        }

        #endregion
    }
}
