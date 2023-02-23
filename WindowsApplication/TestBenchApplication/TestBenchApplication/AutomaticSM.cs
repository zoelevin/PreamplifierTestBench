using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace TestBenchApplication
{
    //enum for states inside of the automatic testing state machine
    public enum AutoState { IDLE=1,Generating , Transmitting, AwaitingVoltage, AwaitingConfirmation, Delay, Testing, } // all automatic states
                                                                                                                       //class used to handle all of the automatic testing state machine ransitions and getting info from the state machine

    public class AutomaticSM
    {
        private AutoState autoState = AutoState.IDLE;  //setting intitial state
        public AutoState CurrentAutoState { get { return autoState; } }  //returns current state
       
        public void HandleTransmitting()
        {
            //send message list
        }
        public void HandleDelay()
        {
            ProgramSM.Instance.relayDelayTimer.Start();  //starts the timer to transition out of the delay state
        }
        public void HandleAwaiting()
        {
            ProgramSM.Instance.uCtimeoutTimer.Start();  //starts the timeout timer for the Uc to respond
        }
        public void HandleNothing()  //does nothing, makes SM look cleaner
        {
            return;
        }
        public void HandleTesting()   //handles transition into the testing state
        {
            APrunner.Instance.RunAPProjectOnePath();
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
                        HandleAwaiting();
                    }
                    break;
                case (ProgramTransitions.PacketSentVolt):
                    if (autoState == AutoState.Transmitting)
                    {
                        autoState = AutoState.AwaitingVoltage;
                        HandleAwaiting();
                    }
                    break;
                case (ProgramTransitions.uCnoResponse):
                    if (autoState == AutoState.AwaitingConfirmation | autoState == AutoState.AwaitingVoltage )
                    {
                        autoState = AutoState.IDLE;
                        HandleNothing();
                    }
                    break;
                case (ProgramTransitions.VoltageFail):
                    if (autoState == AutoState.AwaitingVoltage)
                    {
                        autoState = AutoState.IDLE;
                        HandleNothing();
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
                        ProgramSM.Instance.uCtimeoutTimer.Stop();
                        autoState = AutoState.Transmitting;
                        //this will send the msasages from the buffer one by one
                    }
                    break;
                case (ProgramTransitions.uCconfirmNoMess):
                    if (autoState == AutoState.AwaitingConfirmation)
                    {
                        ProgramSM.Instance.uCtimeoutTimer.Stop();
                        autoState = AutoState.Delay;
                        HandleDelay();
                    }
                    break;
                case (ProgramTransitions.DelayDone):
                    if (autoState == AutoState.Delay)
                    {
                        autoState = AutoState.Testing;
                        HandleTesting();
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
    }
}
