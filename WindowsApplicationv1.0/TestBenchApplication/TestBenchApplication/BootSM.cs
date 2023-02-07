using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public enum BootTransitions { APtimeout = 26, DelayDoneCountLow, DelayDoneCountHigh, APopen, PacketSent, NoConfirmCountLow, NoConfirmCountHigh, uCconfirmAPfail, uCconfirmAPpass, Reboot, BootDone, }  //all boot state transitions
    public enum BootState { IDLE = 1, CheckAP, CloseAP, Transmitting, AwaitingConfirmation, D_Errors, OpeningGui, } //all boot state states
    public class BootSM
    {
        private BootState bootState = BootState.CheckAP; //initial boot state
        public BootState CurrentBootState { get { return bootState; } }  //returns current state
        public void ChangeStates(BootTransitions transition)
        {  //handles state transitions, ran when event happens
            switch (transition)
            {
                case BootTransitions.APtimeout:
                    if (bootState == BootState.CheckAP)
                    {
                        bootState = BootState.CloseAP;
                    }
                    break;
                case BootTransitions.DelayDoneCountLow:
                    if (bootState == BootState.CloseAP)
                    {
                        bootState = BootState.CheckAP;
                    }
                    break;
                case BootTransitions.DelayDoneCountHigh:
                    if (bootState == BootState.CloseAP)
                    {
                        bootState = BootState.Transmitting;
                    }
                    break;
                case BootTransitions.APopen:
                    if (bootState == BootState.CheckAP)
                    {
                        bootState = BootState.Transmitting;
                    }
                    break;
                case BootTransitions.PacketSent:
                    if (bootState == BootState.Transmitting)
                    {
                        bootState = BootState.AwaitingConfirmation;
                    }
                    break;
                case BootTransitions.NoConfirmCountLow:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        bootState = BootState.Transmitting;
                    }
                    break;
                case BootTransitions.NoConfirmCountHigh:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        bootState = BootState.D_Errors;
                    }
                    break;
                case BootTransitions.uCconfirmAPfail:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        bootState = BootState.D_Errors;
                    }
                    break;
                case BootTransitions.uCconfirmAPpass:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        bootState = BootState.OpeningGui;
                    }
                    break;
                case BootTransitions.Reboot:
                    if (bootState == BootState.D_Errors | bootState == BootState.IDLE)
                    {
                        bootState = BootState.CheckAP;
                    }
                    break;
                case BootTransitions.BootDone:
                    if (bootState == BootState.OpeningGui)
                    {
                        bootState = BootState.IDLE;
                    }
                    break;

            }
        }
    }
}
