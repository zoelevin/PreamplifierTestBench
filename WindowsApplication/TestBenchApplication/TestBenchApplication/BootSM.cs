using AudioPrecision.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;
using System.Windows.Forms;
using UA_GUI;

namespace TestBenchApplication
{
    //ENUMS
    public enum BootState { IDLE = 1, CheckAP, CloseAP, Transmitting, AwaitingConfirmation, D_Errors, OpeningGui, } //all boot state states
    
    public class BootSM       //class used to handle all of the boot testing state machine transitions and getting info from the state machine, along with running the states
    {
        //PRIVATE OBJECTS AND VARS
        private BootState bootState = BootState.IDLE;            //initial boot state

        //PUBLIC OBJECTS AND VARS
        public BootState CurrentBootState { get { return bootState; } }      //returns current state
       
        //PUBLIC METHODS

        //Handles behavior of all boot states
        public void RunBootStateMachine(BootState aState)
        {
            
            switch (aState)
            {
                case BootState.IDLE:
                    break;  //do nothing if IDLE
                case BootState.CheckAP:
                    AudioPrecisionRunner.Instance.SetupAP();  //make AP visible
                    programSM.Instance.APattemptCounter++;         //increment attemp of opening AP counter
                    if (AudioPrecisionRunner.Instance.IsOpen() == false)       //if not open transition accodingly
                    {
                        ChangeStates(ProgramTransitions.APtimeout);
                    }
                    else if (AudioPrecisionRunner.Instance.IsOpen() == true)   //if open transition accrodingly
                    {
                        ErrorFlags.Instance.APnoPassFlag = false;
                        programSM.Instance.APattemptCounter = 0;  //reset attemot counter if opens
                        ChangeStates(ProgramTransitions.APopen);
                    }
                    break;
                case BootState.CloseAP:
                    AudioPrecisionRunner.Instance.CloseAP();
                    if (programSM.Instance.APattemptCounter <= 2)    //if less than or equal to 2 try to open again
                    {
                        ChangeStates(ProgramTransitions.DelayDoneCountLow);
                    }
                    else     //if not open dont try to open AP again go to uC check
                    {
                        ErrorFlags.Instance.APnoPassFlag = true;   //AP did not pass, will be used to show errors
                        ChangeStates(ProgramTransitions.DelayDoneCountHigh);  //no go try to contact micro
                    }
                    break;
                case BootState.Transmitting:
                    if (ArduinoComms.AutodetectArduinoPort() == null) //arduino became disconnected
                    {
                        ErrorFlags.Instance.UcCantFindFlag = true;
                        ArduinoComms.IsConnected = false;
                        programSM.Instance.ChangeStates(ProgramTransitions.uCcantFind);
                        break;
                    }
                    else
                    {
                        if (ArduinoComms.IsConnected == false)  //if coming from reconnection state
                        {
                            if (ArduinoComms.TryConnect() != 1)  //try to conncect again
                            {
                                ErrorFlags.Instance.UcCantConnectFlag = true;
                                programSM.Instance.ChangeStates(ProgramTransitions.uCcantConnect);
                                break;
                            }
                        }
                        programSM.Instance.UcattemptCounter++;     //increment attempts that uC has been contacted
                        byte[] testMessage = { 0b00000001 };      //sending a connected ID
                        ArduinoComms.SendPacket(testMessage, 1);
                        programSM.Instance.currentOutMessage.Type = 0b00000001;   //to be compared with message sent back for confirmation
                        programSM.Instance.ChangeStates(ProgramTransitions.PacketSent); //transition with packet sent
                        break;
                    }
                case BootState.AwaitingConfirmation:
                    programSM.Instance.UcTimeoutTimer.Start();        //starts the timer for the uC to timeout if no resposne
                    programSM.Instance.UcMessagePollTimer.Start();     //transitions handled in timer events
                    break;
                case BootState.D_Errors:
                    int[] errors = new int[] { 0};  //0 = error in boot sequence
                    Form form = Application.OpenForms[0];
                    EventTest errorOpen = new EventTest(form);
                    errorOpen.ProcessResult(errors);
                    break;
                case BootState.OpeningGui:
                    foreach (Form frm in Application.OpenForms)
                    {
                        if (frm.Name == "LoadForm")   //close the load form, open next form
                        if (frm.Name == "LoadForm")   //close the load form, open next form
                        {

                            EventTest ET = new EventTest(frm);
                            ET.ProcessResult();
                            break;
                        }
                    }                   
                    break;
                default:
                    break;
            }
        }

        //Handles transitions of all boot states
        public void ChangeStates(ProgramTransitions transition)
        { 
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
                        programSM.Instance.UcTimeoutTimer.Stop();
                        bootState = BootState.D_Errors;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.uCconfirmAPpass:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        programSM.Instance.UcTimeoutTimer.Stop();
                        bootState = BootState.OpeningGui;
                        RunBootStateMachine(bootState);
                    }
                    break;
                case ProgramTransitions.Reboot:
                    if (bootState == BootState.IDLE)
                    {
                        bootState = BootState.CheckAP;
                        RunBootStateMachine(bootState);
                    }else if (bootState == BootState.D_Errors)
                    {
                        //errorDisplay.Hide();
                        bootState = BootState.CheckAP;
                        programSM.Instance.UcattemptCounter = 0;   //reset attempt counters when reboot is hit
                        programSM.Instance.APattemptCounter = 0;
                        ErrorFlags.Instance.APnoPassFlag = false;
                        ErrorFlags.Instance.UcCantConnectFlag = false;
                        ErrorFlags.Instance.UcCantFindFlag = false;
                        ErrorFlags.Instance.UcNoRespFlag = false;
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
