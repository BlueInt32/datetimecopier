using System;
using System.Windows.Forms;

namespace DateCopier
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationContext applicationContext = new CustomApplicationContext();
            Application.Run(applicationContext);
        }
    }
}
