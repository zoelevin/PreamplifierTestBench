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
    //this class will be used for controlling the Audio Precision with the API and retrieivng and storing information
    //The GUI will retrive information from this class

    public class APrunner
    {
        public int APattemptCounter;
        public int numberOfMeasuements;  //used to tell the GUI the total number of measurments in the test
        public int currentMeasurementNumber;  //used to the gui where we currently are in the measurement process
        public Dictionary<Tuple<string, string>, bool> APISequenceReport = new Dictionary<Tuple<string, string>, bool>();
        APx500 APx = new APx500();
        //creating asingle instance that can be accsessed by the whole program
        private APrunner()
        {
            
        }
        private static APrunner _instance = new APrunner();
        public static APrunner Instance
        {
            get
            {
                return _instance;
            }
        }
        public bool IsOpen()
        {
            APException aPException = APx.LastException;
            if (aPException == null)
            {  //checks for no eorrors when opening
                return true;
            }
            else
            {
                return false;
            }
        }
        //method used to make AP visible
        public void SetupAP()  //making the AP visible
        {
            APx.Visible = true;  //show AP
        }

        //method used to close the APx measurement software
        public void CloseAP()  //closes theAP
        {
            APx.Exit();
        }

        //method used for opening the specified project file 
        public void OpenAPproject(string fileName)
        {
            APx.OpenProject(fileName);  //opens approjx file
        }

        //used to update the variable for total measurement count
        public void UpdateMeasurementCounters()
        {
            int numberOfCheckedSignalPaths = 0;
            numberOfMeasuements = 0;  //initialize to 0
            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            for (int i = 0; i < signalPathCount; i++)
            {
                if (APx.Sequence.GetSignalPath(i).Checked)  //only counts measurements if the signal path is checked
                {
                    numberOfCheckedSignalPaths++;
                    numberOfMeasuements += APx.Sequence.GetSignalPath(i).Count; ;
                }
            }
            Console.WriteLine("There are {0} checked signal paths and {1} measurements in all the checked signal paths", numberOfCheckedSignalPaths, numberOfMeasuements);  //used for debugging
        }

        //method used to run all the checked signal paths inside of a project
        public int RunAPproject()  // runs the current project only for checked signal paths
        {

            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            int measurementCount;    //measurement count isnide of a signal path
            string signalPathName;   //gui will need signal path name
            string measurementName;  //gui will need measurement name

            currentMeasurementNumber = 0;
            // Console.WriteLine("There are {0} Signal Paths in this project", signalPathCount);  //used for debug
            for (int i = 0; i < signalPathCount; i++)
            {
                //send micro setup message
                //wait for reception
                // Console.WriteLine("There are {0} Measurements in the {1} signal path", measurementCount, signalPathName);  //used for debug
                if (APx.Sequence.GetSignalPath(i).Checked) //run only checked signal paths
                {
                    signalPathName = APx.Sequence.GetSignalPath(i).Name;
                    measurementCount = APx.Sequence.GetSignalPath(i).Count;
                    for (int j = 0; j < measurementCount; j++)
                    {
                        currentMeasurementNumber++;  //increment current measurement number for gui
                        measurementName = APx.Sequence.GetMeasurement(i, j).Name;   //takes current measurment name
                        APx.Sequence.GetSignalPath(i).GetMeasurement(j).Run();
                        var tempTuple = Tuple.Create(signalPathName, measurementName);
                        APISequenceReport.Add(tempTuple, APx.Sequence.GetMeasurement(i, j).SequenceResults.PassedLimitChecks);    //add measurement and result to dictionary

                    }
                }
                //generate event for sending info to gui
                //send (Signal path name, measurements, passed failed)
                //array [number of signal paths][3]

            }
            return 1;
        }

        /*
        APx500 APx = new APx500();
        public int APattemptCounter;   //used to document the amount of times the AP has been attempted to open
        //things for GUI
        public int numberOfMeasuements;  //used to tell the GUI the total number of measurments in the test
        public int currentMeasurementNumber;  //used to the gui where we currently are in the measurement process
        public Dictionary<Tuple<string, string>, bool> APISequenceReport = new Dictionary<Tuple<string, string>, bool>();


        //method used to check if the AP id open and not in demo mode
        public bool IsOpen()
        {
            APException aPException = APx.LastException;
            if (aPException == null) {  //checks for no eorrors when opening
                return true;
            }
            else
            {
                return false;
            }
        }  
        
        //method used to make AP visible
        public void SetupAP()  //making the AP visible
        {
            APx.Visible = true;  //show AP
        }

        //method used to close the APx measurement software
        public void CloseAP()  //closes theAP
        {
            APx.Exit();
        }

        //method used for opening the specified project file 
        public void OpenAPproject(string fileName)
        {
            APx.OpenProject(fileName);  //opens approjx file
        }

        //used to update the variable for total measurement count
        public void UpdateMeasurementCounters()
        {
            int numberOfCheckedSignalPaths = 0;
            numberOfMeasuements = 0;  //initialize to 0
            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            for (int i = 0; i < signalPathCount; i++)
            {
                if (APx.Sequence.GetSignalPath(i).Checked)  //only counts measurements if the signal path is checked
                {
                    numberOfCheckedSignalPaths++;
                    numberOfMeasuements += APx.Sequence.GetSignalPath(i).Count; ;
                }
            }
            Console.WriteLine("There are {0} checked signal paths and {1} measurements in all the checked signal paths", numberOfCheckedSignalPaths, numberOfMeasuements);  //used for debugging
        }

        //method used to run all the checked signal paths inside of a project
        public int RunAPproject()  // runs the current project only for checked signal paths
        {

            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            int measurementCount;    //measurement count isnide of a signal path
            string signalPathName;   //gui will need signal path name
            string measurementName;  //gui will need measurement name
            
            currentMeasurementNumber = 0;
           // Console.WriteLine("There are {0} Signal Paths in this project", signalPathCount);  //used for debug
            for (int i=0;i<signalPathCount;i++){
                //send micro setup message
                //wait for reception
                // Console.WriteLine("There are {0} Measurements in the {1} signal path", measurementCount, signalPathName);  //used for debug
                if (APx.Sequence.GetSignalPath(i).Checked) //run only checked signal paths
                {
                    signalPathName = APx.Sequence.GetSignalPath(i).Name;
                    measurementCount = APx.Sequence.GetSignalPath(i).Count;
                    for (int j = 0; j < measurementCount; j++)
                    {
                         currentMeasurementNumber++;  //increment current measurement number for gui
                         measurementName = APx.Sequence.GetMeasurement(i, j).Name;   //takes current measurment name
                         APx.Sequence.GetSignalPath(i).GetMeasurement(j).Run();
                        var tempTuple = Tuple.Create(signalPathName, measurementName);
                        APISequenceReport.Add(tempTuple, APx.Sequence.GetMeasurement(i,j).SequenceResults.PassedLimitChecks);    //add measurement and result to dictionary
                         
                     }
                }
                //generate event for sending info to gui
                //send (Signal path name, measurements, passed failed)
                //array [number of signal paths][3]

            }
            return 1;
        }
        */
    }
    
}
