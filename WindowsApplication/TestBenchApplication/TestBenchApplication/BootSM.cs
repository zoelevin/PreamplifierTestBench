﻿using AudioPrecision.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    
    public enum BootState { IDLE = 1, CheckAP, CloseAP, Transmitting, AwaitingConfirmation, D_Errors, OpeningGui, } //all boot state states
    //class used to handle all of the boot testing state machine transitions and getting info from the state machine, along with running the states
    public class BootSM
    {   
        private BootState bootState = BootState.CheckAP; //initial boot state
        public BootState CurrentBootState { get { return bootState; } }  //returns current state

        //handler for entering the check AP state
        public void HandleCheckAP()  //check AP state handler
        {
            APrunner.Instance.SetupAP();
            APrunner.Instance.APattemptCounter++;   //increment attemp of opening AP counter
            if (APrunner.Instance.IsOpen() == false)  //if not open transition accodingly
            {
                ChangeStates(ProgramTransitions.APtimeout);
                Console.WriteLine("Open failed");  //used for debugging
            }
            else if (APrunner.Instance.IsOpen() == true)  //if open transition accrodingly
            {
                ChangeStates(ProgramTransitions.APopen);
                Console.WriteLine("Open success");  //used for debugging
            }
        }
        //Handler for the close AP state
        public void HandleCloseAP()   //close AP state Handler
        {
            APrunner.Instance.CloseAP();
            if (APrunner.Instance.APattemptCounter <= 2)  //if less than or equal to 2 try to open again
            {
                ChangeStates(ProgramTransitions.DelayDoneCountLow);
            }
            else   //if not open dont try to open AP again go to uC check
            {
                ChangeStates(ProgramTransitions.DelayDoneCountHigh);
            }
            
        }
        public void HandleTransmitting()
        {

        }
        public void HandleAwaitingConfirmation()
        {

        }
        public void HandleErrors()
        {

        }
        public void HandleOpeningGui()
        {

        }
        public void ChangeStates(ProgramTransitions transition)
        {  //handles state transitions, ran when event happens
            switch (transition)
            {
                case ProgramTransitions.APtimeout:
                    if (bootState == BootState.CheckAP)
                    {
                        bootState = BootState.CloseAP;
                        HandleCloseAP();
                    }
                    break;
                case ProgramTransitions.DelayDoneCountLow:
                    if (bootState == BootState.CloseAP)
                    {
                        bootState = BootState.CheckAP;
                        HandleCheckAP();
                    }
                    break;
                case ProgramTransitions.DelayDoneCountHigh:
                    if (bootState == BootState.CloseAP)
                    {
                        bootState = BootState.Transmitting;
                    }
                    break;
                case ProgramTransitions.APopen:
                    if (bootState == BootState.CheckAP)
                    {
                        bootState = BootState.Transmitting;
                    }
                    break;
                case ProgramTransitions.PacketSent:
                    if (bootState == BootState.Transmitting)
                    {
                        bootState = BootState.AwaitingConfirmation;
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
                        bootState = BootState.D_Errors;
                    }
                    break;
                case ProgramTransitions.uCconfirmAPpass:
                    if (bootState == BootState.AwaitingConfirmation)
                    {
                        bootState = BootState.OpeningGui;
                    }
                    break;
                case ProgramTransitions.Reboot:
                    if (bootState == BootState.D_Errors | bootState == BootState.IDLE)
                    {
                        bootState = BootState.CheckAP;
                        HandleCheckAP();
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