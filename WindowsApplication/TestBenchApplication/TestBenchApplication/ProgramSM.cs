﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public enum ProgramTransitions { Cancel, TechConfirm, ProductSelectedValid, PacketSent, uCconfirm, uCnoResponse, Start, NewTest, Reconnected,
         Generated, PacketSentVolt, PacketSentNoVolt, VoltageFail, VoltageSuccess, uCconfirmNoMess, uCconfirmMess, DelayDone, APnoResponse, APdoneNoTest, APdoneTest,
        APtimeout, DelayDoneCountLow, DelayDoneCountHigh, APopen, NoConfirmCountLow, NoConfirmCountHigh, uCconfirmAPfail, uCconfirmAPpass, Reboot, BootDone,uCcantFind,uCcantConnect
    }
    //class for linking all of the state machines together into a single class
    //handles all the transitions for all the state machines
    public class ProgramSM
    {
        public BootSM bootSM = new BootSM();  //make instance of boot state machine
        public AutomaticSM autoSM = new AutomaticSM(); //make instance of auto state machine
        public TopLevelStateMachine topSM = new TopLevelStateMachine(); //make instance of top state machine
       private ProgramSM()
        {

        }
        private static ProgramSM _instance = new ProgramSM();
        public static ProgramSM Instance
        {
            get
            {
                return _instance;
            }
        }


        public void Init()
        {
            bootSM.HandleCheckAP();
        }
        public void ChangeStates(ProgramTransitions transition)
        {
            
            if (topSM.CurrentState == TopState.Automatic)  //only change auto states if top level state is automatic
            {
                autoSM.ChangeStates(transition);
            }else if (topSM.CurrentState == TopState.Boot)  // only change boot states if current top state is boot
            {
                bootSM.ChangeStates(transition);  // if boot is done change top level state with boot transition
            }else if (topSM.CurrentState == TopState.ProductConfirmed){
                if (transition == ProgramTransitions.Start)
                {
                    autoSM.ChangeStates(transition);
                }
            }else if ((transition == ProgramTransitions.Cancel) & (topSM.CurrentState != TopState.Boot))
            {
                autoSM.ChangeStates(transition);
                bootSM.ChangeStates(transition);
            }
            topSM.ChangeStates(transition);
        }
    }
}