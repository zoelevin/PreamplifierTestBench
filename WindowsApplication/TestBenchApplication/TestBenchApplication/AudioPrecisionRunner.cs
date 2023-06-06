using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AudioPrecision.API;


namespace TestBenchApplication
{
    //this class will be used for controlling the Audio Precision with the API and retrieivng and storing information
    //The GUI will retrive information from this class

    public class AudioPrecisionRunner
    {
        //  PRIVATE VARIABLES AND OBJECTS  
        public APx500 APx;    
        private bool aPexists;  //used for closing and opening new AP instance if opened incorrectly
        private int measurementsInSingal;
        private int measurementInSignalIndex;  //where we are currently in testing process
        private int measurementCount;  //used for running multiple measurements in a path
        private Dictionary<string, bool> tempDict = new Dictionary<string, bool>();   

        //PUBLIC VARIABLES AND OBJECTS
        public int TotalSignalNumber;  //toal measurements for the test
        public string CurrentSignalName; //used to display current test above progress bar
        public int NumberOfRanSignals;  //used to update progress bar
        public Dictionary<string, Dictionary<string,bool>> APISequenceReport = new Dictionary<string, Dictionary<string, bool>>();  //dictiorary for results in the form of signal path name, measuremnt name, pass/fail
        public int CurrentSignalPathNumber;
        public string ProductName;    //name of the file for PDF
        public string SavedReportTime;  //time the report is saved for looking up with GUI


        //CONSTRUCTOR
        private AudioPrecisionRunner()
        {
            //INITIALIZING 
            APx = new APx500(APxOperatingMode.SequenceMode);
            aPexists = true;
            measurementInSignalIndex = 0;   //initializing variables
            CurrentSignalPathNumber = 0;
            TotalSignalNumber = 0;
            NumberOfRanSignals = 0;
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
        
        //checks if the AP was apened without no exceptions and not in demo mode
        public bool IsOpen() 
        {
            APException aPException;
                
                aPException = APx.LastException;
          
                if (aPException == null && APx.IsDemoMode == false)
                {  //checks for no eorrors when opening
                    APx.Top = 0;  //sets position of AP
                    APx.Left = 0;
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
                APx= new APx500(APxOperatingMode.SequenceMode);   //setting mode
                aPexists = true; 
            }
        }

        //method used to destroy the current (wrong) instance of the APx500 (wrong because in demo mode or exception)
        public void CloseAP() 
        {
            APx.Exit();
            APx = null;
            aPexists = false;
            Thread.Sleep(2000);  //need this to destroy current instance before opening another
        }

        //method used for opening the specified project file 
        public void OpenAudioPrecisionProject(string fileName)
        {
            if (APx.ProjectFileName == fileName)
            {
                return;
            }
            APx.OpenProject(fileName);  //opens approjx file
        }

        //method used to count the number of signal paths will be ran, used forupdating progreess bar
        public int UpdateMeasurementCounters()  
        {
            int numberOfCheckedSignalPaths=0;
            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            for (int i = 0; i < signalPathCount; i++)
            {
                if (APx.Sequence.GetSignalPath(i).Checked)  //only counts measurements if the signal path is checked
                {
                    numberOfCheckedSignalPaths++; 
                }
            }
            return numberOfCheckedSignalPaths;
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
            CurrentSignalName = APx.Sequence.GetSignalPath(CurrentSignalPathNumber).Name;   //name of current signal path
            for (int j = 0; j < measurementCount; j++)
            {
                //add a retry counter option
                if (APx.Sequence.GetMeasurement(CurrentSignalPathNumber, j).Checked == true) {
                    measurementName = APx.Sequence.GetMeasurement(CurrentSignalPathNumber, j).Name;   //takes current measurment name
                    APx.Sequence.GetSignalPath(CurrentSignalPathNumber).GetMeasurement(j).Run();
                    if (measurementName != "Signal Path Setup" )   //dont add this to report if signal path setup
                    {
                        tempDict.Add(measurementName, APx.Sequence.GetMeasurement(CurrentSignalPathNumber, j).SequenceResults.PassedLimitChecks);    //add measurement and result to dictionary
                    }
                }
            }
            
            APISequenceReport.Add(signalPathName, tempDict);
            CurrentSignalPathNumber++;  //increments
            NumberOfRanSignals++;

        }


        //method used to tun the project one measurement at a time
        //NOT USED
        public void RunAPProjectOneMeas() //need to be able to run project measurement by measurement
        {
            string signalPathName;   //gui will need signal path name
            string measurementName;  //gui will need measurement name
            while ((APx.Sequence.GetSignalPath(CurrentSignalPathNumber).Checked != true) && (CurrentSignalPathNumber <= APx.Sequence.Count))
            {   //increments through making sure signal paths are checked and the current index is valid
                CurrentSignalPathNumber++;
                if (CurrentSignalPathNumber == APx.Sequence.Count)  //leave if all signal paths have been gone through
                {
                    return;
                }
            }
            signalPathName = APx.Sequence.GetSignalPath(CurrentSignalPathNumber).Name;   //name of current signal path
            measurementsInSingal = APx.Sequence.GetSignalPath(CurrentSignalPathNumber).Count;  //measurments in the signal path
            while ((APx.Sequence.GetMeasurement(CurrentSignalPathNumber, measurementInSignalIndex).Checked != true) && (measurementInSignalIndex <= measurementsInSingal))
            {   //increments through making sure signal paths are checked and the current index is valid
                measurementInSignalIndex++;
                if ((measurementInSignalIndex) == measurementsInSingal)  //leave if all signal paths have been gone through
                {
                    APISequenceReport.Add(signalPathName, new Dictionary<string, bool>(tempDict)); //need new dict so clearing does clear the report
                    tempDict.Clear();
                    measurementInSignalIndex = 0;
                    CurrentSignalPathNumber++;
                    return;
                }

            }
            measurementName = APx.Sequence.GetMeasurement(CurrentSignalPathNumber, measurementInSignalIndex).Name;   //takes current measurment name
            APx.Sequence.GetSignalPath(CurrentSignalPathNumber).GetMeasurement(measurementInSignalIndex).Run();
            tempDict.Add(measurementName, APx.Sequence.GetMeasurement(CurrentSignalPathNumber, measurementInSignalIndex).SequenceResults.PassedLimitChecks);    //add measurement and result to dictionary
            measurementInSignalIndex++;
            if (measurementInSignalIndex == measurementsInSingal)
            {
                APISequenceReport.Add(signalPathName, new Dictionary<string, bool>(tempDict)); //need new dict so clearing does clear the report
                tempDict.Clear(); //clears the dictionary for storage on next function call
                CurrentSignalPathNumber++;  //increments
                measurementInSignalIndex = 0;
            }
        }

        //method used to run all the checked signal paths inside of a project
        //NOT USED
        public int RunAPprojectWhole()  // runs the current project only for checked signal paths
        {

            int signalPathCount = APx.Sequence.Count;  //where signal paths count will be held
            int measurementCount;    //measurement count isnide of a signal path
            string signalPathName;   //gui will need signal path name
            string measurementName;  //gui will need measurement name
            Dictionary<string, bool> tempDict = new Dictionary<string, bool>();
            
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
