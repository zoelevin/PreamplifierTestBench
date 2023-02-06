using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public enum AutoTransitions {Start=13, Generated , PacketSentVolt, PacketSentNoVolt,VoltageFail, uCnoResponse, VoltageSuccess,uCconfirmNoMess,uCconfirmMess,DelayDone,APnoResponse,APdoneNoTest,APdoneTest}
    public enum AutoState { IDLE=1,Generating , Transmitting, AwaitingVoltage, AwaitingConfirmation, Delay, Testing, }
    public class AutomaticSM
    {
        private AutoState autoState = AutoState.IDLE;
        public AutoState CurrentAutoState { get { return autoState; } }  //returns current state
        public void ChangeStates(AutoTransitions transition)
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
                    if (autoState == (AutoState.AwaitingConfirmation | AutoState.AwaitingVoltage ))
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




            }
        }
    }
}
