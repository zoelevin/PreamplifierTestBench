using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
        private Messages AllMessages = new Messages();
        private int messageIndex = 0;
        private Queue<MessageNoIndex> MessageQueue = new Queue<MessageNoIndex>();
        private AutoState autoState = AutoState.IDLE;  //setting intitial state
        //PUBLIC OBJECTS AND VARS
        public AutoState CurrentAutoState { get { return autoState; } }  //returns current state
        //FUNCTIONS
        public void RunAutoStateMachine(AutoState aState)  //performs the behavior of the automatic testing state machine
        {
            switch (aState)
            {
                case AutoState.IDLE:
                    break;  //don nothing in IDLE
                case AutoState.Generating:
                    messageIndex++;
                    while ((AllMessages.SixTenBmessages.Count>0) && (AllMessages.SixTenBmessages.Peek().ListIndex == messageIndex))  //peeaking at the list index of all the messages to see if its in the current index we want to send
                    {
                        Messages.MessageWithIndex temp = AllMessages.SixTenBmessages.Dequeue(); 
                        MessageQueue.Enqueue(new MessageNoIndex(temp.length, temp.Payload));  //transfer message with no index now, as we know index was correct
                    }
                    ProgramSM.Instance.ChangeStates(ProgramTransitions.Generated);   //when all messages of that index loaded, change states
                    break;
                case AutoState.Transmitting:
                    if (ArduinoComms.AutodetectArduinoPort() == null) //arduino became disconnected
                    {
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);
                        ArduinoComms.IsConnected = false;
                        break;
                    }
                    else
                    { 
                        if (ArduinoComms.IsConnected == false)  //if coming from reconnection state
                        {
                            if (ArduinoComms.TryConnect() != 1)  //try to conncect again
                            {
                                ProgramSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);
                                break;
                            }
                        }
                        MessageNoIndex tempMess = MessageQueue.Dequeue();   
                        ArduinoComms.SendPacket(tempMess.Payload, tempMess.length);   //send packet top of queue
                        ProgramSM.Instance.currentOutMessage.Type = tempMess.Payload[0];  //update current out message to be compared with message sent back
                        if (tempMess.Payload[0] != 0b00010000)  //this is the voltage check ID
                        {
                            ProgramSM.Instance.ChangeStates(ProgramTransitions.PacketSentNoVolt);  
                        }
                        else
                        {
                            ProgramSM.Instance.ChangeStates(ProgramTransitions.PacketSentVolt);
                        }
                        break;

                    }
                case AutoState.AwaitingVoltage:  
                    ProgramSM.Instance.uCtimeoutTimer.Start();         //starts the timer for the uC to timeout if no resposne
                    ProgramSM.Instance.uCMessagePollTimer.Start();     //transitions handled in timer events
                    break;
                case AutoState.AwaitingConfirmation:
                    ProgramSM.Instance.uCtimeoutTimer.Start();           //starts the timer for the uC to timeout if no resposne
                    ProgramSM.Instance.uCMessagePollTimer.Start();       //transitions handled in timer events
                    break;
                case AutoState.Delay:                    //delays for relay switching
                    ProgramSM.Instance.relayDelayTimer.Start();
                    break;
                case AutoState.Testing:
                    APrunner.Instance.RunAPProjectOnePath();       //runs signal path for the setup test
                    if (AllMessages.SixTenBmessages.Count == 0)  //if no more messages to be generated ie no more tests to be ran
                    {
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.APdoneNoTest);
                    }
                    else
                    {
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.APdoneTest);
                    }
                    break;
                default:
                    break;
            }
        }
        public void ChangeStates(ProgramTransitions transition)  //all transition events
        {  //handles state transitions, ran when event happens
            switch (transition)
            {
                case (ProgramTransitions.Start):
                    if (autoState == AutoState.IDLE)
                    {
                        messageIndex = 0;  //intiializing things before a test
                        APrunner.Instance.currentSignalPathNumber = 0;
                        APrunner.Instance.APISequenceReport.Clear();
                        AllMessages.SixTenBmessages.Clear();
                        AllMessages.AddToMessages(Products.SixTenB);
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
            if (MessageQueue.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private struct MessageNoIndex    //used for putting messages into send message function
        {
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
