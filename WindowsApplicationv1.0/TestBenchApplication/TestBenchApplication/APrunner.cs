using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AudioPrecision.API;


namespace TestBenchApplication
{
    public class APrunner
    {
        APx500 APx = new APx500();
        public void SetupAP()
        {
            APx.Visible = true;
        }
        public void OpenAPproject(string fileName)
        {
            APx.OpenProject(fileName);
        }
        public int RunAPproject()
        {
            int signalPathCount = APx.Sequence.Count;
            int measurementCount;
            string signalPathName;
            Console.WriteLine("There are {0} Signal Paths in this project", signalPathCount);
            for (int i=0;i<signalPathCount;i++){
                measurementCount = APx.Sequence.GetSignalPath(i).Count;
                signalPathName = APx.Sequence.GetSignalPath(i).Name;
                Console.WriteLine("There are {0} Measurements in the {1} signal path", measurementCount, signalPathName);
                if (APx.Sequence.GetSignalPath(i).Checked)
                {
                    APx.Sequence.GetSignalPath(i).Run();
                }

            }
            
            return 1;
        }
    }
    
}
