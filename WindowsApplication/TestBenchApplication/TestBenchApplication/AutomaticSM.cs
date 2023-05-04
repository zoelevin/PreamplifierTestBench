using AudioPrecision.API;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WindowsFormsApp1;

namespace TestBenchApplication
{
    //ENUMS
    public enum AutoState { IDLE=1,Generating , Transmitting, AwaitingVoltage, AwaitingConfirmation, Delay, Testing, } // all automatic states
    public class AutomaticSM //class used to handle all of the automatic testing state machine ransitions and getting info from the state machine
    {

        //PRIVATE OBJECTS AND VARS
        public Messages allMessages = new Messages();   //creates instance of messages
        private int messageIndex = 0;     //init
        private Queue<MessageNoIndex> messageQueue = new Queue<MessageNoIndex>();     //Queue of messages with index which represents signal path number
        private AutoState autoState = AutoState.IDLE;  //setting intitial state

        //PUBLIC OBJECTS AND VARS
        public static string FinalReportName = "";

        //PUBLIC METHODS
        public AutoState CurrentAutoState { get { return autoState; } }  //returns current state

        //Performs the state behavior of all states
        public void RunAutoStateMachine(AutoState aState)  
        {
            switch (aState)
            {
                case AutoState.IDLE:
                    allMessages.ProductMessages.Clear();
                    break;  //don nothing in IDLE
                case AutoState.Generating:
                    while ((allMessages.ProductMessages.Count>0) && (allMessages.ProductMessages.Peek().ListIndex == messageIndex))  //peeaking at the list index of all the messages to see if its in the current index we want to send
                    {
                        Messages.MessageWithIndex temp = allMessages.ProductMessages.Dequeue(); 
                        messageQueue.Enqueue(new MessageNoIndex(temp.length, temp.Payload));  //transfer message with no index now, as we know index was correct
                    }
                    messageIndex++;
                    programSM.Instance.ChangeStates(ProgramTransitions.Generated);   //when all messages of that index loaded, change states
                    break;
                case AutoState.Transmitting:
                    if (ArduinoComms.AutodetectArduinoPort() == null) //arduino became disconnected
                    {
                        programSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);
                        ArduinoComms.IsConnected = false;
                        break;
                    }
                    else
                    { 
                        if (ArduinoComms.IsConnected == false)  //if coming from reconnection state
                        {
                            if (ArduinoComms.TryConnect() != 1)  //try to conncect again
                            {
                                programSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);
                                break;
                            }
                        }
                        MessageNoIndex tempMess = messageQueue.Dequeue();   
                        ArduinoComms.SendPacket(tempMess.Payload, tempMess.length);   //send packet top of queue
                        programSM.Instance.currentOutMessage.Type = tempMess.Payload[0];  //update current out message to be compared with message sent back
                        if (tempMess.Payload[0] != 0b00001111)  //this is the voltage check ID
                        {
                            programSM.Instance.ChangeStates(ProgramTransitions.PacketSentNoVolt);  
                        }
                        else
                        {
                            programSM.Instance.ChangeStates(ProgramTransitions.PacketSentVolt);
                        }
                        break;

                    }
                case AutoState.AwaitingVoltage:  
                    programSM.Instance.UcTimeoutTimer.Start();         //starts the timer for the uC to timeout if no resposne
                    programSM.Instance.UcMessagePollTimer.Start();     //transitions handled in timer events
                    break;
                case AutoState.AwaitingConfirmation:
                    programSM.Instance.UcTimeoutTimer.Start();           //starts the timer for the uC to timeout if no resposne
                    programSM.Instance.UcMessagePollTimer.Start();       //transitions handled in timer events
                    break;
                case AutoState.Delay:                    //delays for relay switching
                    programSM.Instance.RelayDelayTimer.Start();
                    break;
                case AutoState.Testing:
                   
                    if (allMessages.ProductMessages.Count == 0)  //if no more messages to be generated ie no more tests to be ran
                    {
                        
                        AudioPrecisionRunner.Instance.RunAPProjectOnePath();      //runs signal path for the setup test
                        string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName; //relative path
                        AudioPrecisionRunner.Instance.APx.Sequence.Report.AutoSaveReportFileLocation = (path + "\\TestingReports");  //where reports will be saved
                        AudioPrecisionRunner.Instance.APx.Sequence.Report.AutoSaveReportFileNameType = AutoSaveReportFileNameType.CustomPrefix;   
                        AudioPrecisionRunner.Instance.SavedReportTime = DateTime.Now.ToString("HH-mm");   //time report is saved
                        FinalReportName = "Testing Report for " + AudioPrecisionRunner.Instance.ProductName + " at " + AudioPrecisionRunner.Instance.SavedReportTime + " on " + DateTime.Now.ToString("MM-dd-yyyy");  //name of file saved
                        AudioPrecisionRunner.Instance.APx.Sequence.Report.AutoSaveReportFileNamePrefix = FinalReportName;
                        AudioPrecisionRunner.Instance.APx.Sequence.Report.AutoSaveReport = true;   //only want this on the last signal path
                        AudioPrecisionRunner.Instance.RunAPProjectOnePath();      //runs signal path for the setup test
                        AudioPrecisionRunner.Instance.APx.Sequence.Report.AutoSaveReport = false;
                        AudioPrecisionRunner.Instance.APx.Sequence.Report.Reset();  //reset for next board to be tested
                        programSM.Instance.ChangeStates(ProgramTransitions.APdoneNoTest);
                    }
                    else
                    {
                        AudioPrecisionRunner.Instance.RunAPProjectOnePath();      //runs signal path for the setup test
                        programSM.Instance.ChangeStates(ProgramTransitions.APdoneTest);
                    }
                    break;
                default:
                    break;
            }
        }

        //Handles all automatic SM transitions
        public void ChangeStates(ProgramTransitions transition)  
        { 
            switch (transition)
            {
                case (ProgramTransitions.Start):
                    if (autoState == AutoState.IDLE)
                    {
                        messageIndex = 0;  //intiializing things before a test
                        AudioPrecisionRunner.Instance.CurrentSignalPathNumber = 0;
                        AudioPrecisionRunner.Instance.NumberOfRanSignals = 0;
                        AudioPrecisionRunner.Instance.APISequenceReport.Clear();
                        AudioPrecisionRunner.Instance.APx.Sequence.Report.Reset();
                        AudioPrecisionRunner.Instance.APx.Sequence.Report.AutoSaveReport = false;
                        autoState = AutoState.Generating;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.Generated):
                    if (autoState == AutoState.Generating)
                    {
                        autoState = AutoState.Transmitting;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.PacketSentNoVolt):
                    if (autoState == AutoState.Transmitting)
                    {
                        autoState = AutoState.AwaitingConfirmation;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.PacketSentVolt):
                    if (autoState == AutoState.Transmitting)
                    {
                        autoState = AutoState.AwaitingVoltage;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.uCnoResponse):
                    if (autoState == AutoState.AwaitingConfirmation | autoState == AutoState.AwaitingVoltage | autoState == AutoState.Transmitting)
                    {
                        autoState = AutoState.IDLE;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.VoltageFail):
                    if (autoState == AutoState.AwaitingVoltage)
                    {
                        autoState = AutoState.IDLE;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.VoltageSuccess):
                    if (autoState == AutoState.AwaitingVoltage)
                    {
                        autoState = AutoState.Transmitting;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.uCconfirmMess):
                    if (autoState == AutoState.AwaitingConfirmation)
                    {
                        autoState = AutoState.Transmitting;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.uCconfirmNoMess):
                    if (autoState == AutoState.AwaitingConfirmation)
                    {
                        autoState = AutoState.Delay;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.DelayDone):
                    if (autoState == AutoState.Delay)
                    {
                        autoState = AutoState.Testing;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.APnoResponse):
                    if (autoState == AutoState.Testing)
                    {
                        autoState = AutoState.IDLE;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.APdoneTest):
                    if (autoState == AutoState.Testing)
                    {
                        autoState = AutoState.Generating;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.APdoneNoTest):
                    if (autoState == AutoState.Testing)
                    {
                        autoState = AutoState.IDLE;
                        RunAutoStateMachine(autoState);
                    }
                    break;
                case (ProgramTransitions.Cancel):
                    autoState = AutoState.IDLE;
                    RunAutoStateMachine(autoState);
                    break;
                default:
                    break;




            }
        }
        public bool MessagesRemaining()  //for telling the state machine to switch correctly
        {
            if (messageQueue.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        //STRUCTS
        private struct MessageNoIndex    //used for putting messages into send message function
        {
            //messages of the same index added to a queue of this struct
            public byte length;
            public byte[] Payload;
            public MessageNoIndex(byte len, byte[] pLoad)
            {
                length = len;
                Payload = pLoad;
            }
        }
    }
}
