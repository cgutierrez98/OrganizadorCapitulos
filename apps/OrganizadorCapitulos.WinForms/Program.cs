using System;
using System.Windows.Forms;
using OrganizadorCapitulos.WinForms.UI.Forms;

namespace OrganizadorCapitulos.WinForms
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new MainForm());
        }
    }
}
