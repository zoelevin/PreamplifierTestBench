//using AudioPrecision.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using UA_GUI;
using System.Security.Cryptography.X509Certificates;

namespace TestBenchApplication
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
            Application.Run(new LoadForm());
            //StateMachinesTestForm gui = new StateMachinesTestForm();
            //Application.Run(gui);

        }
    }
}
