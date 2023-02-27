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
    //ENUMS
    public enum TopState { Boot = 1, D_BenchChecks, ProductSelection, Transmitting, AwaitingConfirmation, ProductConfirmed, Automatic, Results, VoltageErrors, Reconnection }   //enum of states starting at 1
    public class TopLevelStateMachine
    {

        =
        private TopState topState = TopState.Boot;    //default state = boot
        
        //FUNCTIONS
        public TopState CurrentState { get { return topState; } }  //returns current state

        public void HandleProductConfirmed()
        {
            //if product == 6176
            APrunner.Instance.OpenAPproject("C:\\Users\\macke\\GroupProject\\WindowsApplication\\TestBenchApplication\\6176.R6 (1).approjx");
        }

        public void ChangeStates(ProgramTransitions transition){  //handles state transitions, ran when event happens
            switch (transition)   //all cases are state transition conditions, based on an event (transition)
            {
                case (ProgramTransitions.BootDone):
                    if (topState == TopState.Boot)
                    {
                        topState = TopState.D_BenchChecks;
                        //if event tech confirm
                        //
                    }
                    break;
                case (ProgramTransitions.TechConfirm):
                    if (topState == TopState.D_BenchChecks)
                    {
                        topState = TopState.ProductSelection;
                    }
                    break;
                case (ProgramTransitions.ProductSelectedValid):
                    if (topState == TopState.ProductSelection)
                    {
                        topState = TopState.Transmitting;
                    }
                    break;
                case (ProgramTransitions.PacketSent):
                    if (topState == TopState.Transmitting)
                    {
                        topState = TopState.AwaitingConfirmation;
                    }
                    break;
                case (ProgramTransitions.uCconfirm):
                    if (topState == TopState.AwaitingConfirmation)
                    {
                        ProgramSM.Instance.uCtimeoutTimer.Stop();
                        topState = TopState.ProductConfirmed;
                    }
                    break;
                        
                case (ProgramTransitions.uCnoResponse):
                    if (topState == TopState.AwaitingConfirmation)
                    {
                        topState = TopState.Reconnection; 
                    }
                    break;
                case (ProgramTransitions.Start):
                    if (topState == TopState.ProductConfirmed)
                    {
                        topState = TopState.Automatic;
                    }
                    break;
                case (ProgramTransitions.APdoneNoTest):
                    if (topState == TopState.Automatic)
                    {
                        topState = TopState.Results;
                    }
                    break;
                case (ProgramTransitions.VoltageFail):
                    if (topState == TopState.Automatic)
                    {
                        topState = TopState.VoltageErrors;
                    }
                    break;
                case (ProgramTransitions.APnoResponse):
                    if (topState == TopState.Automatic)
                    {
                        topState = TopState.Reconnection;
                    }
                    break;
                case (ProgramTransitions.NewTest):
                    if (topState == TopState.Results | topState == TopState.VoltageErrors)
                    {
                        topState = TopState.ProductSelection;
                    }
                    break;
                case (ProgramTransitions.Reconnected):
                    if (topState == TopState.Reconnection)
                    {
                        topState = TopState.ProductSelection;
                    }
                    break;
                case (ProgramTransitions.Cancel):
                    topState = TopState.ProductSelection;
                    break;
                default:
                    break;

            
            }

        }
    }
}
