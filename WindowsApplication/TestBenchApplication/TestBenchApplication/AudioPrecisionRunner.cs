using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AudioPrecision.API;


namespace TestBenchApplication
{
    //this class will be used for controlling the Audio Precision with the API and retrieivng and storing information
    //The GUI will retrive information from this class

    public class AudioPrecisionRunner
    {
        //PRIVATE VARIABLES AND OBJECTS
        private APx500 APx;
        private bool aPexists;
        private int measurementsInSingal;
        private int measurementInSignalIndex;
        private int measurementCount;
        private Dictionary<string, bool> tempDict = new Dictionary<string, bool>();


        //PUBLIC VARIABLES AND OBJECTS
        public int TotalMeasurementNumber;  //toal measurements for the test
        public int CurrentMeasurementNumber;  //used to the gui where we currently are in the measurement process
        public Dictionary<string, Dictionary<string,bool>> APISequenceReport = new Dictionary<string, Dictionary<string, bool>>();  //dictiorary for results in the form of signal path name, measuremnt name, pass/fail
        public int CurrentSignalPathNumber;


        //CONSTRUCTOR
        private AudioPrecisionRunner()
        {
            //INITIALIZING 
            APx = new APx500(APxOperatingMode.SequenceMode);
            aPexists = true;
            measurementInSignalIndex = 0;
            CurrentSignalPathNumber = 0;
            TotalMeasurementNumber = 0;
            CurrentMeasurementNumber = 0;
        }
        private static AudioPrecisionRunner _instance = new AudioPrecisionRunner();   //creating single instance for the program
        public static AudioPrecisionRunner Instance
        {
            get
            {
                return _instance;
            }
        }



        //PUBLIC METHODS
        public bool IsOpen()  //checks for AP opened sucessfully
        {
            APException aPException = APx.LastException;
            if (aPException == null && APx.IsDemoMode == true)
            {  //checks for no eorrors when opening
                APx.Visible = true;
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
            if (aPexists == false)
            {
                APx= new APx500(APxOperatingMode.SequenceMode);
                aPexists = true;
            }
        }
        //method used to make the APx measurement software not visible
        public void CloseAP() 
        {
            APx.Exit();
            APx = null;
            aPexists = false;
            Thread.Sleep(3000);
        }
        //method used for opening the specified project file 
        public void OpenAudioPrecisionProject(string fileName)
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
                    TotalMeasurementNumber += APx.Sequence.GetSignalPath(i).Count;
                }
            }
            return TotalMeasurementNumber;
        }
        public void RunAPProjectOneMeas() //need to be able to run project signal path by signal path not all at once
        { 
            string signalPathName;   //gui will need signal path name
            string measurementName;  //gui will need measurement name
            while ((APx.Sequence.GetSignalPath(CurrentSignalPathNumber).Checked !=true) && (CurrentSignalPathNumber <= APx.Sequence.Count)){   //increments through making sure signal paths are checked and the current index is valid
                CurrentSignalPathNumber++;
                if (CurrentSignalPathNumber == APx.Sequence.Count)  //leave if all signal paths have been gone through
                {
                    return;
                }
            }
            measurementsInSingal = APx.Sequence.GetSignalPath(CurrentSignalPathNumber).Count;  //measurments in the signal path
            while ((APx.Sequence.GetMeasurement(CurrentSignalPathNumber, measurementInSignalIndex).Checked != true) && (measurementInSignalIndex <= measurementsInSingal))
            {   //increments through making sure signal paths are checked and the current index is valid
                measurementInSignalIndex++;
                if ((measurementInSignalIndex) == measurementsInSingal)  //leave if all signal paths have been gone through
                {
                    return;
                }
            }
            measurementName = APx.Sequence.GetMeasurement(CurrentSignalPathNumber, measurementInSignalIndex).Name;   //takes current measurment name
            APx.Sequence.GetSignalPath(CurrentSignalPathNumber).GetMeasurement(measurementInSignalIndex).Run();
            tempDict.Add(measurementName, APx.Sequence.GetMeasurement(CurrentSignalPathNumber, measurementInSignalIndex).SequenceResults.PassedLimitChecks);    //add measurement and result to dictionary
            CurrentMeasurementNumber++;  //increment current measurement number for gui
            measurementInSignalIndex++;
            if (measurementInSignalIndex == measurementsInSingal)
            {
                signalPathName = APx.Sequence.GetSignalPath(CurrentSignalPathNumber).Name;   //name of current signal path
                APISequenceReport.Add(signalPathName, tempDict);
                tempDict.Clear();
                CurrentSignalPathNumber++;  //increments
                measurementInSignalIndex = 0;
            }
            
            
        }
        //method to run one signal path at a time
        public void RunAPProjectOnePath() //need to be able to run project signal path by signal path not all at once
        {
            string signalPathName;   //gui will need signal path name
            string measurementName;  //gui will need measurement name
            Dictionary<string, bool> tempDict = new Dictionary<string, bool>();
            while ((APx.Sequence.GetSignalPath(CurrentSignalPathNumber).Checked != true) && (CurrentSignalPathNumber <= APx.Sequence.Count))
            {   //increments through making sure signal paths are checked and the current index is valid
                CurrentSignalPathNumber++;
                if (CurrentSignalPathNumber == APx.Sequence.Count)  //leave if all signal paths have been gone through
                {
                    return;
                }
            }
            measurementCount = APx.Sequence.GetSignalPath(CurrentSignalPathNumber).Count;  //measurments in the signal path
            signalPathName = APx.Sequence.GetSignalPath(CurrentSignalPathNumber).Name;   //name of current signal path
            for (int j = 0; j < measurementCount; j++)
            {
                CurrentMeasurementNumber++;  //increment current measurement number for gui
                //add a retry counter option
                if (APx.Sequence.GetMeasurement(CurrentSignalPathNumber, j).Checked == true) {
                    measurementName = APx.Sequence.GetMeasurement(CurrentSignalPathNumber, j).Name;   //takes current measurment name
                    APx.Sequence.GetSignalPath(CurrentSignalPathNumber).GetMeasurement(j).Run();
                    tempDict.Add(measurementName, APx.Sequence.GetMeasurement(CurrentSignalPathNumber, j).SequenceResults.PassedLimitChecks);    //add measurement and result to dictionary
                }
            }
            APISequenceReport.Add(signalPathName, tempDict);
            CurrentSignalPathNumber++;  //increments

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
            CurrentMeasurementNumber = 0;
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
                        CurrentMeasurementNumber++;  //increment current measurement number for gui
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
