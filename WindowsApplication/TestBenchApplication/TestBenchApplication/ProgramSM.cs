using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
        //event handlers
        public event EventHandler relayDelayTimerUp;
        //sub SM declarations
        public BootSM bootSM = new BootSM();  //make instance of boot state machine
        public AutomaticSM autoSM = new AutomaticSM(); //make instance of auto state machine
        public TopLevelStateMachine topSM = new TopLevelStateMachine(); //make instance of top state machine


        public Timer relayDelayTimer = new Timer(3000);  //timer used for delaying process for relays to switch, currently 3 second delay
        public Timer uCtimeoutTimer = new Timer(3000);  //gives the micro time to respond, currently 3 second delay
        //public Timer APtmeoutTimerr = new Timer(3000);  //gives the AP time to respond, currently 3 second delay

        private ProgramSM()
        {
            //init timer 1
            relayDelayTimer.Elapsed += RelayDelayTimer_Elapsed;  //adding event handler
            relayDelayTimer.Enabled = true;  //enables events
            relayDelayTimer.AutoReset= false;  //dont want it to restart automatically, only when it eneters the delay state
                                               //init timer 2
            uCtimeoutTimer.Elapsed += RelayDelayTimer_Elapsed;  //adding event handler
            uCtimeoutTimer.Enabled = true;  //enables events
            uCtimeoutTimer.AutoReset = false;  //dont want it to restart automatically, only when it eneters the delay state

        }

        private void RelayDelayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
           
            ProgramSM.Instance.ChangeStates(ProgramTransitions.DelayDone);  //handle delay done event
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
