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
        APx500 APx = new APx500();

        //PUBLIC VARIABLES AND OBJECTS
        public int totalMeasurements;  //toal measurements for the test
        public int currentMeasurementNumber;  //used to the gui where we currently are in the measurement process
        public Dictionary<string, Dictionary<string,bool>> APISequenceReport = new Dictionary<string, Dictionary<string, bool>>();  //dictiorary for results in the form of signal path name, measuremnt name, pass/fail
        public int currentSignalPathNumber;
        //PRIVATE VARIABLES AND OBJECTS


        //FUNCTIONS AND CONSTRUCTORS
        private APrunner()
        {
            currentSignalPathNumber = 0;
            totalMeasurements = 0;
            currentMeasurementNumber = 0;
        }
        private static APrunner _instance = new APrunner();   //creating single instance for the program
        public static APrunner Instance
        {
            get
            {
                return _instance;
            }
        }
        public bool IsOpen()  //checks for AP opened sucessfully
        {
            APException aPException = APx.LastException;
            if (aPException == null ) //will also add demo mode check
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
        public void CloseAP()  //closes theAP, neeeds AP to be opened though
        {
            if (APx.IsDemoMode == true){ 
            APx.Exit();
            }
            else
            {
                return;
            }
        }

        //method used for opening the specified project file 
        public void OpenAPproject(string fileName)
        {
            APx.OpenProject(fileName);  //opens approjx file
        }

       
        public int UpdateMeasurementCounters()  //used to update the variable for total measurement count
        {
            //int numberOfCheckedSignalPaths = 0;  //used for debug
            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            for (int i = 0; i < signalPathCount; i++)
            {
                if (APx.Sequence.GetSignalPath(i).Checked)  //only counts measurements if the signal path is checked
                {
                    //numberOfCheckedSignalPaths++;  //used for debug
                    totalMeasurements += APx.Sequence.GetSignalPath(i).Count; 
                }
            }
            //Console.WriteLine("There are {0} checked signal paths and {1} measurements in all the checked signal paths", numberOfCheckedSignalPaths, totalMeasuements);  //used for debugging
            return totalMeasurements;
        }

        

        public void RunAPProjectOnePath() //need to be able to run project signal path by signal path not all at once
        {
            int measurementCount=0;    //measurement count isnide of a signal path
            string signalPathName;   //gui will need signal path name
            string measurementName;  //gui will need measurement name
            Dictionary<string,bool> tempDict = new Dictionary<string, bool>();
            while ((APx.Sequence.GetSignalPath(currentSignalPathNumber).Checked !=true) & (currentSignalPathNumber <= APx.Sequence.Count)){   //increments through making sure signal paths are checked and the current index is valid
                currentSignalPathNumber++;
                if (currentSignalPathNumber == APx.Sequence.Count)  //leave if all signal paths have been gone through
                {
                    return;
                }
            }
            measurementCount = APx.Sequence.GetSignalPath(currentSignalPathNumber).Count;  //measurments in the signal path
            signalPathName = APx.Sequence.GetSignalPath(currentSignalPathNumber).Name;   //name of current signal path
            for (int j = 0; j < measurementCount; j++) 
            {
                currentMeasurementNumber++;  //increment current measurement number for gui
                measurementName = APx.Sequence.GetMeasurement(currentSignalPathNumber, j).Name;   //takes current measurment name
                APx.Sequence.GetSignalPath(currentSignalPathNumber).GetMeasurement(j).Run();
                //add a retry counter option
                tempDict.Add(measurementName, APx.Sequence.GetMeasurement(currentSignalPathNumber, j).SequenceResults.PassedLimitChecks);    //add measurement and result to dictionary

            }
            APISequenceReport.Add(signalPathName, tempDict);
            currentSignalPathNumber++;  //increments
            
        }

        //method used to run all the checked signal paths inside of a project
        //not used by program was used for debugging
        public int RunAPprojectWhole()  // runs the current project only for checked signal paths
        {

            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            int measurementCount;    //measurement count isnide of a signal path
            string signalPathName;   //gui will need signal path name
            string measurementName;  //gui will need measurement name
            Dictionary<string, bool> tempDict = new Dictionary<string, bool>();
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
                        tempDict.Add(measurementName, APx.Sequence.GetMeasurement(i, j).SequenceResults.PassedLimitChecks);    //add measurement and result to dictionary

                    }
                    APISequenceReport.Add(signalPathName, tempDict);
                }
            }
            return 1;
        }
    
    }
    
}
