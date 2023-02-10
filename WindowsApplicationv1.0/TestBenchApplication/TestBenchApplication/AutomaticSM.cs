using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public enum AutoTransitions {Cancel=0,Start=13, Generated , PacketSentVolt, PacketSentNoVolt,VoltageFail, uCnoResponse, VoltageSuccess,uCconfirmNoMess,uCconfirmMess,DelayDone,APnoResponse,APdoneNoTest,APdoneTest}  //Declaring all things that can change the automatic state machine
    public enum AutoState { IDLE=1,Generating , Transmitting, AwaitingVoltage, AwaitingConfirmation, Delay, Testing, } // all automatic states
    public class AutomaticSM
    {
        private AutoState autoState = AutoState.IDLE;  //setting intitial state
        public AutoState CurrentAutoState { get { return autoState; } }  //returns current state
        public void ChangeStates(AutoTransitions transition)  //all transition events
        {  //handles state transitions, ran when event happens
            switch (transition)
            {
                case (AutoTransitions.Start):
                    if (autoState == AutoState.IDLE)
                    {
                        autoState = AutoState.Generating;
                    }
                    break;
                case (AutoTransitions.Generated):
                    if (autoState == AutoState.Generating)
                    {
                        autoState = AutoState.Transmitting;
                    }
                    break;
                case (AutoTransitions.PacketSentNoVolt):
                    if (autoState == AutoState.Transmitting)
                    {
                        autoState = AutoState.AwaitingConfirmation;
                    }
                    break;
                case (AutoTransitions.PacketSentVolt):
                    if (autoState == AutoState.Transmitting)
                    {
                        autoState = AutoState.AwaitingVoltage;
                    }
                    break;
                case (AutoTransitions.uCnoResponse):
                    if (autoState == AutoState.AwaitingConfirmation | autoState == AutoState.AwaitingVoltage )
                    {
                        autoState = AutoState.IDLE;
                    }
                    break;
                case (AutoTransitions.VoltageFail):
                    if (autoState == AutoState.AwaitingVoltage)
                    {
                        autoState = AutoState.IDLE;
                    }
                    break;
                case (AutoTransitions.VoltageSuccess):
                    if (autoState == AutoState.AwaitingVoltage)
                    {
                        autoState = AutoState.Transmitting;
                    }
                    break;
                case (AutoTransitions.uCconfirmMess):
                    if (autoState == AutoState.AwaitingConfirmation)
                    {
                        autoState = AutoState.Transmitting;
                    }
                    break;
                case (AutoTransitions.uCconfirmNoMess):
                    if (autoState == AutoState.AwaitingConfirmation)
                    {
                        autoState = AutoState.Delay;
                    }
                    break;
                case (AutoTransitions.DelayDone):
                    if (autoState == AutoState.Delay)
                    {
                        autoState = AutoState.Testing;
                    }
                    break;
                case (AutoTransitions.APnoResponse):
                    if (autoState == AutoState.Testing)
                    {
                        autoState = AutoState.IDLE;
                    }
                    break;
                case (AutoTransitions.APdoneTest):
                    if (autoState == AutoState.Testing)
                    {
                        autoState = AutoState.Generating;
                    }
                    break;
                case (AutoTransitions.APdoneNoTest):
                    if (autoState == AutoState.Testing)
                    {
                        autoState = AutoState.IDLE;
                    }
                    break;
                case (AutoTransitions.Cancel):
                    autoState = AutoState.IDLE;
                    break;




            }
        }
    }
}
