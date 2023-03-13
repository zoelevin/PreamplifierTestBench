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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            StateMachinesTestForm gui = new StateMachinesTestForm();
            //Application.Run(gui);
            AudioPrecisionRunner.Instance.SetupAP();
            AudioPrecisionRunner.Instance.IsOpen();
            AudioPrecisionRunner.Instance.OpenAudioPrecisionProject("C:\\Users\\macke\\GroupProject\\WindowsApplication\\TestBenchApplication\\6176.R6.approjx");
            for (int i = 0; i < 14; i++)
            {
                AudioPrecisionRunner.Instance.RunAPProjectOneMeas();
            }
            int x = 0;
        }
    }
}
