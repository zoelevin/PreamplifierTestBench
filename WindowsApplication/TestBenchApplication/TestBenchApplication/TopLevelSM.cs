using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Resources;
using static System.Windows.Forms.AxHost;
using WindowsFormsApp1;

namespace TestBenchApplication
{
    //ENUMS
    public enum TopState { Boot = 1, D_BenchChecks, ProductSelection, Transmitting, AwaitingConfirmation, ProductConfirmed, Automatic, Results, VoltageErrors, Reconnection }   //enum of states starting at 1
    public class TopLevelSM
    {

        //PRIVATE OBJECTS AND VARS
        private TopState topState = TopState.Boot;    //default state = boot
        
        //PUBLIC OBJECTS AND VARS
        public TopState CurrentState { get { return topState; } }  //returns current state

        //PUBLIC METHODS
        public void RunTopStateMachine(TopState aState)
        {
            switch (aState)
            {
                case TopState.Boot:   //handled in other SM
                    break;  //
                case TopState.D_BenchChecks:    // just open form
                    //open correct GUI form
                    break;
                case TopState.ProductSelection:    //just open form
                    //open correct GUI form
                    break;
                case TopState.Transmitting:     
                    if (ArduinoComms.AutodetectArduinoPort() == null)    //checks if uC is disconnected
                    {
                        programSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);
                        ArduinoComms.IsConnected = false;
                        break;
                    }else
                    {
                        if (ArduinoComms.IsConnected == false)  //if came from reconnection state
                        {
                            if (ArduinoComms.TryConnect() != 1)
                            {
                                programSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);
                                break;
                            }
                        }
                        byte[] testMessage = { 0b00000010 };  //sending a connected ID
                        ArduinoComms.SendPacket(testMessage, 1);
                        programSM.Instance.currentOutMessage.Type = 0b00000010;  //to be compared to message that comes in
                        programSM.Instance.ChangeStates(ProgramTransitions.PacketSent);   //transition with packet sent
                        break;
                    }
                    
                case TopState.ProductConfirmed:
                    //show start button
                    break;
                case TopState.AwaitingConfirmation:
                    programSM.Instance.UcTimeoutTimer.Start();  //gives uC a certain amount of time to respond
                    programSM.Instance.UcMessagePollTimer.Start(); //polls the queue
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
                        //will have a switch case here with different products
                        AudioPrecisionRunner.Instance.OpenAudioPrecisionProject("C:\\Users\\mvinsonh\\Desktop\\GroupProject\\WindowsApplication\\TestBenchApplication\\610B_UCSCeditionRev1.1.approjx");
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
                        topState = TopState.ProductConfirmed;
                        RunTopStateMachine(topState);
                    }
                    break;
                        
                case (ProgramTransitions.uCnoResponse):
                    if (topState == TopState.AwaitingConfirmation | topState == TopState.Transmitting | topState == TopState.Automatic)
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
