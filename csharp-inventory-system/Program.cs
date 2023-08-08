using csharp_inventory_system.Layers.UI;
using csharp_inventory_system.Layers.UI.Acerca_de;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp_inventory_system
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          //  Application.Run(new frmLogin());
            Application.Run(new frmPrincipal());
        }
    }
}
