using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace TestBenchApplication
{
    //ENUMS
    public enum AutoState { IDLE=1,Generating , Transmitting, AwaitingVoltage, AwaitingConfirmation, Delay, Testing, } // all automatic states
                                                                                                                       //class used to handle all of the automatic testing state machine ransitions and getting info from the state machine

    public class AutomaticSM
    {
        private int messageIndex = 0;
        private Queue<MessageToBeSent> MessageQueue = new Queue<MessageToBeSent>();
        private AutoState autoState = AutoState.IDLE;  //setting intitial state
        public AutoState CurrentAutoState { get { return autoState; } }  //returns current state
        //FUNCTIONS
        public void RunAutoStateMachine(AutoState aState)
        {
            switch (aState)
            {
                case AutoState.IDLE:
                    break;
                case AutoState.Generating:
                    messageIndex++;

                    break;
                case AutoState.Transmitting:
                    //need to write
                    break;
                case AutoState.AwaitingVoltage:
                    ProgramSM.Instance.uCtimeoutTimer.Start();
                    break;
                case AutoState.AwaitingConfirmation:
                    ProgramSM.Instance.uCtimeoutTimer.Start();
                    break;
                case AutoState.Delay:
                    ProgramSM.Instance.relayDelayTimer.Start();
                    break;
                case AutoState.Testing:
                    APrunner.Instance.RunAPProjectOnePath();
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
                        autoState = AutoState.Generating;
                        //this will load ahrdcoded messages into a buffer based on the needed test
                    }
                    break;
                case (ProgramTransitions.Generated):
                    if (autoState == AutoState.Generating)
                    {
                        autoState = AutoState.Transmitting;
                        //this will send the msasages from the buffer one by one
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
                    if (autoState == AutoState.AwaitingConfirmation | autoState == AutoState.AwaitingVoltage)
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
                        //this will send the msasages from the buffer one by one
                    }
                    break;
                case (ProgramTransitions.uCconfirmMess):
                    if (autoState == AutoState.AwaitingConfirmation)
                    {
                        autoState = AutoState.Transmitting;
                        //this will send the msasages from the buffer one by one
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
                    }
                    break;
                case (ProgramTransitions.APdoneTest):
                    if (autoState == AutoState.Testing)
                    {
                        autoState = AutoState.Generating;
                    }
                    break;
                case (ProgramTransitions.APdoneNoTest):
                    if (autoState == AutoState.Testing)
                    {
                        messageIndex = 0;
                        autoState = AutoState.IDLE;
                    }
                    break;
                case (ProgramTransitions.Cancel):
                    autoState = AutoState.IDLE;
                    break;
                default:
                    break;




            }
        }
        private struct MessageToBeSent    //used for putting messages into send message function
        {
            public byte Type;
            public byte length;
            public byte[] Payload;
            public MessageToBeSent(byte t, byte len, byte[] pLoad)
            {
                Type = t;
                length = len;
                Payload = pLoad;
            }
        }
    }
}
