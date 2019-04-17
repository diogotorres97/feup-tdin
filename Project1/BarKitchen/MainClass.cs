using System;
using System.Windows.Forms;

namespace BarKitchen
{
    internal static class MainClass
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BarKitchenChooseMenu());
        }
    }
}