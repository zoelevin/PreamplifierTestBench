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
        public int APattemptCounter;  //will be used for checking times AP has been attempted to open






        public bool IsOpen()
        {
            APException aPException = APx.LastException;
            if (aPException == null) {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void SetupAP()  //making the AP visible
        {
            APx.Visible = true;  //show AP
        }

        public void CloseAP()  //closes theAP
        {
            APx.Exit();
        }

        public void OpenAPproject(string fileName)
        {
            APx.OpenProject(fileName);  //opens approjx file
        }

        public int RunAPproject()  // runs the current project only for checked signal paths
        {
            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            int measurementCount;
            string signalPathName;
           // Console.WriteLine("There are {0} Signal Paths in this project", signalPathCount);  //used for debug
            for (int i=0;i<signalPathCount;i++){
                measurementCount = APx.Sequence.GetSignalPath(i).Count;
                signalPathName = APx.Sequence.GetSignalPath(i).Name;
               // Console.WriteLine("There are {0} Measurements in the {1} signal path", measurementCount, signalPathName);  //used for debug
                if (APx.Sequence.GetSignalPath(i).Checked) //run only checked signal paths
                {
                    //wait for micro confirm with timeout
                    APx.Sequence.GetSignalPath(i).Run(); //run signal path
                    
                }

            }
            return 1;
        }
    }
    
}
