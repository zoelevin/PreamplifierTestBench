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
        public int APattemptCounter;
        public int numberOfMeasuements;
        public int currentMeasurementNumber;
        

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

        public void UpdateMeasurementCounters()
        {
            int numberOfCheckedSignalPaths = 0;
            numberOfMeasuements = 0;
            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            for (int i = 0; i < signalPathCount; i++)
            {
                if (APx.Sequence.GetSignalPath(i).Checked)
                {
                    numberOfCheckedSignalPaths++;
                    numberOfMeasuements += APx.Sequence.GetSignalPath(i).Count; ;
                }
            }
            Console.WriteLine("There are {0} checked signal paths and {1} measurements in all the checked signal paths", numberOfCheckedSignalPaths, numberOfMeasuements);
        }
        public int RunAPproject()  // runs the current project only for checked signal paths
        {
            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            int measurementCount;
            string signalPathName;
            string measurementName;
            currentMeasurementNumber = 0;
           // Console.WriteLine("There are {0} Signal Paths in this project", signalPathCount);  //used for debug
            for (int i=0;i<signalPathCount;i++){
               // Console.WriteLine("There are {0} Measurements in the {1} signal path", measurementCount, signalPathName);  //used for debug
                if (APx.Sequence.GetSignalPath(i).Checked) //run only checked signal paths
                {
                    signalPathName = APx.Sequence.GetSignalPath(i).Name;
                    measurementCount = APx.Sequence.GetSignalPath(i).Count;
                    APx.Sequence.GetSignalPath(i).Run(); //run signal path
                    //for (int j = 0; i < measurementCount; j++)
                   // {
                      //  currentMeasurementNumber++;
                        //current measuement name = ......
                     //   APx.Sequence.GetSignalPath(i).GetMeasurement(j).Run();
                        
                    // }
                }
                //generate event for sending info to gui
                //send (Signal path name, measurements, passed failed)
                //array [number of signal paths][3]

            }
            return 1;
        }
    }
    
}
