using AudioPrecision.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
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
            
            APrunner.Instance.SetupAP();
            APrunner.Instance.OpenAPproject("C:\\Users\\mvinsonh\\Desktop\\GroupProject\\WindowsApplication\\TestBenchApplication\\6176.R6 (1).approjx");  //proof of concept project run
            APrunner.Instance.UpdateMeasurementCounters();
          //  APrunner.Instance.UpdateMeasurementCounters();
          //  APrunner.Instance.RunAPproject();
          //just opens the form and makes instance of the ap runner
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StateMachinesTestForm gui = new StateMachinesTestForm();
            Application.Run(gui);
        }

    }
}
