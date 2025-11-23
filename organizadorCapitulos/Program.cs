using System;
using System.Windows.Forms;
using organizadorCapitulos.UI.Forms;

namespace organizadorCapitulos
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Usar el nombre completo para evitar ambig�edad
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new MainForm());
        }
    }
}