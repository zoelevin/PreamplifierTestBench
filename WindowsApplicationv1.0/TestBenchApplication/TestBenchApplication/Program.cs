using AudioPrecision.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //StateMachinesTestForm gui = new StateMachinesTestForm();
            //Application.Run(gui);
            APrunner runner = new APrunner();
            runner.SetupAP();
            runner.OpenAPproject("C:\\Users\\macke\\OneDrive\\Desktop\\6176\\Copy these files to C drive under sub directory_Universal Audio MFG\\6176.R6.approjx");
            runner.RunAPproject();


        }
        
    }
}
