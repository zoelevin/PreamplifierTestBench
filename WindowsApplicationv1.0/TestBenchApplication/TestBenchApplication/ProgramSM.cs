using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public enum ProgramTransitions { Cancel, TechConfirm, ProductSelectedValid, PacketSent, uCconfirm, uCnoResponse, Start, NewTest, Reconnected,
         Generated, PacketSentVolt, PacketSentNoVolt, VoltageFail, VoltageSuccess, uCconfirmNoMess, uCconfirmMess, DelayDone, APnoResponse, APdoneNoTest, APdoneTest,
        APtimeout, DelayDoneCountLow, DelayDoneCountHigh, APopen, NoConfirmCountLow, NoConfirmCountHigh, uCconfirmAPfail, uCconfirmAPpass, Reboot, BootDone,
    }
    public class ProgramSM
    {
        public BootSM bootSM = new BootSM();  //make instance of boot state machine
        public AutomaticSM autoSM = new AutomaticSM(); //make instance of auto state machine
        public TopLevelStateMachine topSM = new TopLevelStateMachine(); //make instance of top state machine
        public void ChangeStates(ProgramTransitions transition)
        {
            topSM.ChangeStates(transition);
            if (topSM.CurrentState == TopState.Automatic)  //only change auto states if top level state is automatic
            {
                autoSM.ChangeStates(transition);
            }else if (topSM.CurrentState == TopState.Boot)  // only change boot states if current top state is boot
            {
                bootSM.ChangeStates(transition);  // if boot is done change top level state with boot transition
            }else if (transition == ProgramTransitions.Cancel)
            {
                autoSM.ChangeStates(transition);
                bootSM.ChangeStates(transition);
            }
        }
    }
}
