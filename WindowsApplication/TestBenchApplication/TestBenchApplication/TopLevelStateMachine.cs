using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Resources;
using static System.Windows.Forms.AxHost;

namespace TestBenchApplication
{
    //ENUMS
    public enum TopState { Boot = 1, D_BenchChecks, ProductSelection, Transmitting, AwaitingConfirmation, ProductConfirmed, Automatic, Results, VoltageErrors, Reconnection }   //enum of states starting at 1
    public class TopLevelStateMachine
    {

        
        private TopState topState = TopState.Boot;    //default state = boot
        
        //FUNCTIONS
        public TopState CurrentState { get { return topState; } }  //returns current state

        
        public void RunTopStateMachine(TopState aState)
        {
            switch (aState)
            {
                case TopState.Boot:
                    break;
                case TopState.D_BenchChecks:
                    //open correct GUI form
                    break;
                case TopState.ProductSelection:
                    //open correct GUI form
                    break;
                case TopState.Transmitting:
                    //send clear message
                    break;
                case TopState.ProductConfirmed:
                    APrunner.Instance.OpenAPproject("C:\\Users\\macke\\GroupProject\\WindowsApplication\\TestBenchApplication\\6176.R6 (1).approjx");
                    break;
                case TopState.AwaitingConfirmation:
                    ProgramSM.Instance.uCtimeoutTimer.Start();
                    break;
                case TopState.Automatic:
                    //all handled in lower state machine
                    break;
                case TopState.Results:
                    //open correct GUI form
                    break;
                case TopState.VoltageErrors:
                    //open correct GUI form
                    break;
                case TopState.Reconnection:
                    //open correct GUI form
                    break;
                default:
                    break;
            }
        }

        public void ChangeStates(ProgramTransitions transition){  //handles state transitions, ran when event happens
            switch (transition)   //all cases are state transition conditions, based on an event (transition)
            {
                case (ProgramTransitions.BootDone):
                    if (topState == TopState.Boot)
                    {
                        topState = TopState.D_BenchChecks;
                        RunTopStateMachine(topState);
                        //
                    }
                    break;
                case (ProgramTransitions.TechConfirm):
                    if (topState == TopState.D_BenchChecks)
                    {
                        topState = TopState.ProductSelection;
                        RunTopStateMachine(topState);
                    }
                    break;
                case (ProgramTransitions.ProductSelectedValid):
                    if (topState == TopState.ProductSelection)
                    {
                        topState = TopState.Transmitting;
                        RunTopStateMachine(topState);
                    }
                    break;
                case (ProgramTransitions.PacketSent):
                    if (topState == TopState.Transmitting)
                    {
                        topState = TopState.AwaitingConfirmation;
                        RunTopStateMachine(topState);
                    }
                    break;
                case (ProgramTransitions.uCconfirm):
                    if (topState == TopState.AwaitingConfirmation)
                    {
                        ProgramSM.Instance.uCtimeoutTimer.Stop();
                        topState = TopState.ProductConfirmed;
                        RunTopStateMachine(topState);
                    }
                    break;
                        
                case (ProgramTransitions.uCnoResponse):
                    if (topState == TopState.AwaitingConfirmation)
                    {
                        topState = TopState.Reconnection;
                        RunTopStateMachine(topState);
                    }
                    break;
                case (ProgramTransitions.Start):
                    if (topState == TopState.ProductConfirmed)
                    {
                        topState = TopState.Automatic;
                        RunTopStateMachine(topState);
                    }
                    break;
                case (ProgramTransitions.APdoneNoTest):
                    if (topState == TopState.Automatic)
                    {
                        topState = TopState.Results;
                        RunTopStateMachine(topState);
                    }
                    break;
                case (ProgramTransitions.VoltageFail):
                    if (topState == TopState.Automatic)
                    {
                        topState = TopState.VoltageErrors;
                        RunTopStateMachine(topState);
                    }
                    break;
                case (ProgramTransitions.APnoResponse):
                    if (topState == TopState.Automatic)
                    {
                        topState = TopState.Reconnection;
                        RunTopStateMachine(topState);
                    }
                    break;
                case (ProgramTransitions.NewTest):
                    if (topState == TopState.Results | topState == TopState.VoltageErrors)
                    {
                        topState = TopState.ProductSelection;
                        RunTopStateMachine(topState);
                    }
                    break;
                case (ProgramTransitions.Reconnected):
                    if (topState == TopState.Reconnection)
                    {
                        topState = TopState.ProductSelection;
                        RunTopStateMachine(topState);
                    }
                    break;
                case (ProgramTransitions.Cancel):
                    topState = TopState.ProductSelection;
                    RunTopStateMachine(topState);
                    break;
                default:
                    break;

            
            }

        }
    }
}
