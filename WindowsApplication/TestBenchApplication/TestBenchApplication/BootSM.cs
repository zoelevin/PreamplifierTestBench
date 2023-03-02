using AudioPrecision.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;

namespace TestBenchApplication
{
    //ENUMS
    public enum BootState { IDLE = 1, CheckAP, CloseAP, Transmitting, AwaitingConfirmation, D_Errors, OpeningGui, } //all boot state states
    
    public class BootSM   //class used to handle all of the boot testing state machine transitions and getting info from the state machine, along with running the states
    {   
        private BootState bootState = BootState.IDLE; //initial boot state
        public BootState CurrentBootState { get { return bootState; } }  //returns current state

        //FUNCTIONS
        
        //Handler for the close AP state

        public void RunBootStateMachine(BootState aState)
        {
            switch (aState)
            {
                case BootState.IDLE:
                    break;
                case BootState.CheckAP:
                    APrunner.Instance.SetupAP();
                    ProgramSM.Instance.APattemptCounter++;   //increment attemp of opening AP counter
                    if (APrunner.Instance.IsOpen() == false)  //if not open transition accodingly
                    {
                        ChangeStates(ProgramTransitions.APtimeout);
                       // Console.WriteLine("Open failed");  //used for debugging
                    }
                    else if (APrunner.Instance.IsOpen() == true)  //if open transition accrodingly
                    {
                        ChangeStates(ProgramTransitions.APopen);
                        //Console.WriteLine("Open success");  //used for debugging
                    }
                    break;
                case BootState.CloseAP:
                    APrunner.Instance.CloseAP();
                    if (ProgramSM.Instance.APattemptCounter <= 2)  //if less than or equal to 2 try to open again
                    {
                        ChangeStates(ProgramTransitions.DelayDoneCountLow);
                    }
                    else   //if not open dont try to open AP again go to uC check
                    {
                        ChangeStates(ProgramTransitions.DelayDoneCountHigh);
                    }
                    break;
                case BootState.Transmitting:
                    if (ArduinoComms.TryConnect() == 1) {
                        byte[] testMessage = { 0b00000001 };
                        ArduinoComms.SendPacket(testMessage,1);
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.PacketSent);
                        break;
                    }else if (ArduinoComms.TryConnect() == 0)
                    {
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.uCcantConnect);
                        break;
                    }
                    else
                    {
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.uCcantFind);
                        break;
                    }
                case BootState.AwaitingConfirmation:
                    ProgramSM.Instance.uCtimeoutTimer.Start();  //starts the timer for the uC to timeout if no resposne
                    ProgramSM.Instance.uCMessagePollTimer.Start();
                    break;
                case BootState.D_Errors:
                    //need to write
                    //open form 
                    break;
                case BootState.OpeningGui:
                    //do this for GUI form
                    //StateMachinesTestForm gui = new StateMachinesTestForm();
                   // Application.Run(gui);
                    break;
                default:
                    break;
            }
        }
        public void ChangeStates(ProgramTransitions transition)
        {  //handles state transitions, ran when event happens
            switch (transition)
            {
                case ProgramTransitions.APtimeout:
                    if (bootState == BootState.CheckAP)
                    {
                        bootState = BootState.CloseAP;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.DelayDoneCountLow:
                    if (bootState == BootState.CloseAP)
                    {
                        bootState = BootState.CheckAP;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.DelayDoneCountHigh:
                    if (bootState == BootState.CloseAP)
                    {
                        bootState = BootState.Transmitting;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.APopen:
                    if (bootState == BootState.CheckAP)
                    {
                        bootState = BootState.Transmitting;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.PacketSent:
                    if (bootState == BootState.Transmitting)
                    {
                        bootState = BootState.AwaitingConfirmation;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.NoConfirmCountLow:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        bootState = BootState.Transmitting;
                    }
                    break;
                case ProgramTransitions.NoConfirmCountHigh:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        bootState = BootState.D_Errors;
                    }
                    break;
                case ProgramTransitions.uCconfirmAPfail:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        ProgramSM.Instance.uCtimeoutTimer.Stop();
                        bootState = BootState.D_Errors;
                    }
                    break;
                case ProgramTransitions.uCconfirmAPpass:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        ProgramSM.Instance.uCtimeoutTimer.Stop();
                        bootState = BootState.OpeningGui;
                    }
                    break;
                case ProgramTransitions.Reboot:
                    if (bootState == BootState.D_Errors | bootState == BootState.IDLE)
                    {
                        bootState = BootState.CheckAP;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.BootDone:
                    if (bootState == BootState.OpeningGui)
                    {
                        bootState = BootState.IDLE;
                    }
                    break;
                case ProgramTransitions.Cancel:
                        bootState = BootState.IDLE;
                        break;
                case ProgramTransitions.uCcantConnect:
                    if (bootState == BootState.Transmitting)
                    {
                        bootState = BootState.D_Errors;
                    }
                    break;
                case ProgramTransitions.uCcantFind:
                    if (bootState == BootState.Transmitting)
                    {
                        bootState = BootState.D_Errors;
                    }
                    break;
                default:
                    break;

            }
        }
    }
}
