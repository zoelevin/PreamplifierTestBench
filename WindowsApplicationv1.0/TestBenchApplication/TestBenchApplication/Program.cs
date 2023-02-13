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
            StateMachinesTestForm gui = new StateMachinesTestForm();
            //Application.Run(gui);
            APrunner runner = new APrunner();
            runner.SetupAP();
            runner.OpenAPproject("C:\\Users\\mvinsonh\\Desktop\\GroupProject\\WindowsApplicationv1.0\\TestBenchApplication\\6176.R6 (1).approjx");  //proof of concept project run
            runner.RunAPproject();


        }

    }
}
