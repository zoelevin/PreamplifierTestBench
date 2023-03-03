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

        //error form if the boot sequence fails
        private BootErrorForm ErrorDisplay = new BootErrorForm();
        //FUNCTIONS

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
                        ProgramSM.Instance.APpassFlag = true;
                        ProgramSM.Instance.APattemptCounter = 0;
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
                        ProgramSM.Instance.APpassFlag = false;                     //AP did not pass
                        ChangeStates(ProgramTransitions.DelayDoneCountHigh);

                    }
                    break;
                case BootState.Transmitting:
                    if (ArduinoComms.TryConnect() == 1) {
                        byte[] testMessage = { 0b00000001 };  //sending a connected ID
                        ArduinoComms.SendPacket(testMessage,1);
                        ProgramSM.Instance.currentOutMessage.Type = 0b00000001;
                        ProgramSM.Instance.UcattemptCounter++;                            //increment attempts that uC has been contacted
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.PacketSent);   //transition with packet sent
                        break;
                    }else if (ArduinoComms.TryConnect() == 0)
                    {
                        ProgramSM.Instance.uCcantConnectFlag = true;
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.uCcantConnect);
                        break;
                    }
                    else
                    {
                        ProgramSM.Instance.uCcantFindFlag = true;
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.uCcantFind);
                        break;
                    }
                case BootState.AwaitingConfirmation:
                    ProgramSM.Instance.uCtimeoutTimer.Start();  //starts the timer for the uC to timeout if no resposne
                    ProgramSM.Instance.uCMessagePollTimer.Start();
                    break;
                case BootState.D_Errors:
                    ErrorDisplay.Show();
                    break;
                case BootState.OpeningGui:
                    //do this for GUI form
                    //StateMachinesTestForm gui = new StateMachinesTestForm();
                    // Application.Run(gui);
                    ProgramSM.Instance.ChangeStates(ProgramTransitions.BootDone;
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
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.NoConfirmCountHigh:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        bootState = BootState.D_Errors;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.uCconfirmAPfail:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        ProgramSM.Instance.uCtimeoutTimer.Stop();
                        bootState = BootState.D_Errors;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.uCconfirmAPpass:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        ProgramSM.Instance.uCtimeoutTimer.Stop();
                        bootState = BootState.OpeningGui;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.Reboot:
                    if (bootState == BootState.D_Errors | bootState == BootState.IDLE)
                    {
                        bootState = BootState.CheckAP;
                        ErrorDisplay.Hide();                       //hide error form
                        ProgramSM.Instance.UcattemptCounter = 0;   //reset attempt counters
                        ProgramSM.Instance.APattemptCounter = 0;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.BootDone:
                    if (bootState == BootState.OpeningGui)
                    {
                        bootState = BootState.IDLE;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.Cancel:
                        bootState = BootState.IDLE;
                        break;
                case ProgramTransitions.uCcantConnect:
                    if (bootState == BootState.Transmitting)
                    {
                        bootState = BootState.D_Errors;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.uCcantFind:
                    if (bootState == BootState.Transmitting)
                    {
                        bootState = BootState.D_Errors;
                        RunBootStateMachine(bootState);
                    }
                    break;
                default:
                    break;

            }
        }
    }
}
