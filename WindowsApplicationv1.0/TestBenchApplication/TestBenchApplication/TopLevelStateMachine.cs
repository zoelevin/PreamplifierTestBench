using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Resources;

namespace TestBenchApplication
{
    public class TopLevelStateMachine
    {
        public enum TopState{Boot=11,D_BenchChecks,ProductSelection,Transmitting,AwaitingConfirmation,ProductConfirmed,Automatic,Results,VoltageErrors,Reconnection}   //enum of states starting at 1
        public enum TopTransitions {BootDone=1,TechConfirm, ProductSelectedValid, PacketSent, uCconfirm, uCnoResponse,  Start,  DoneAutomatic, APnoResponse, VoltageFail, NewTest, Reconnected,}  //enum of transitions starting at 1
        
        private TopState topState = TopState.Boot;    //default state = boot
        
        public TopState CurrentState { get { return topState; } }  //returns current state
        
        public void ChangeStates(TopTransitions transition){  //handles state transitions, ran when event happens
            switch (transition)   //all cases are state transition conditions, based on an event (transition)
            {
                case (TopTransitions.BootDone):
                    if (topState == TopState.Boot)
                    {
                        topState = TopState.D_BenchChecks;
                    }
                    break;
                case (TopTransitions.TechConfirm):
                    if (topState == TopState.D_BenchChecks)
                    {
                        topState = TopState.ProductSelection;
                    }
                    break;
                case (TopTransitions.ProductSelectedValid):
                    if (topState == TopState.ProductSelection)
                    {
                        topState = TopState.Transmitting;
                    }
                    break;
                case (TopTransitions.PacketSent):
                    if (topState == TopState.Transmitting)
                    {
                        topState = TopState.AwaitingConfirmation;
                    }
                    break;
                case (TopTransitions.uCconfirm):
                    if (topState == TopState.AwaitingConfirmation)
                    {
                        topState = TopState.ProductConfirmed;
                    }
                    break;

                case (TopTransitions.uCnoResponse):
                    if (topState == TopState.AwaitingConfirmation)
                    {
                        topState = TopState.Reconnection; 
                    }
                    break;
                case (TopTransitions.Start):
                    if (topState == TopState.ProductConfirmed)
                    {
                        topState = TopState.Automatic;
                    }
                    break;
                case (TopTransitions.DoneAutomatic):
                    if (topState == TopState.Automatic)
                    {
                        topState = TopState.Results;
                    }
                    break;
                case (TopTransitions.VoltageFail):
                    if (topState == TopState.Automatic)
                    {
                        topState = TopState.VoltageErrors;
                    }
                    break;
                case (TopTransitions.APnoResponse):
                    if (topState == TopState.Automatic)
                    {
                        topState = TopState.Reconnection;
                    }
                    break;
                case (TopTransitions.NewTest):
                    if (topState == (TopState.Results | TopState.VoltageErrors))
                    {
                        topState = TopState.ProductSelection;
                    }
                    break;
                case (TopTransitions.Reconnected):
                    if (topState == TopState.Reconnection)
                    {
                        topState = TopState.ProductSelection;
                    }
                    break;
                default:
                    break;

            
            }

        }
    }
}
