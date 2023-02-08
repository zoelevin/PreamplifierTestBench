using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestBenchApplication
{
    public partial class StateMachinesTestForm : Form
    {
        BootSM bootSM = new BootSM();  //make instance of boot state machine
        AutomaticSM autoSM = new AutomaticSM(); //make instance of auto state machine
        TopLevelStateMachine topSM = new TopLevelStateMachine(); //make instance of top state machine
        public StateMachinesTestForm()
        {
            InitializeComponent();
            this.TopMost = true;
        }
        private void StateMachinesTestForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = topSM.CurrentState.ToString();  //initializing text boxes to current state values
            textBox2.Text = autoSM.CurrentAutoState.ToString();
            textBox3.Text = bootSM.CurrentBootState.ToString();
        }
        private void button_Click(object sender, EventArgs e)
        {
            int var = intButtonClick(sender);   //any button click causes state transitions calculations for all states
            TopTransitions currentTopTransition = (TopTransitions)var;
            AutoTransitions currentAutoTransition = (AutoTransitions)var;
            BootTransitions currentBootTransition = (BootTransitions)var;
            topSM.ChangeStates(currentTopTransition);  //
            if (topSM.CurrentState == TopState.Automatic)  //only change auto states if top level state is automatic
            {
                autoSM.ChangeStates(currentAutoTransition);  
                if (currentAutoTransition == AutoTransitions.uCnoResponse | currentAutoTransition == AutoTransitions.APnoResponse)  //if uC or AP not responding change to lvel state to reconnection
                {
                    topSM.ChangeStates(TopTransitions.uCnoResponse);
                }else if (currentAutoTransition == AutoTransitions.APdoneNoTest)   //if Ap tests are done, change top level state with done automatic transition
                {
                    topSM.ChangeStates(TopTransitions.DoneAutomatic);
                }
                else if (currentAutoTransition == AutoTransitions.VoltageFail) //if votage fails change top level state to voltage fail
                {
                    topSM.ChangeStates(TopTransitions.VoltageFail);
                }
                else if (currentTopTransition == TopTransitions.Start)
                {
                    autoSM.ChangeStates(AutoTransitions.Start);
                }
            }
            else if (topSM.CurrentState == TopState.Boot)  // only change boot states if current top state is boot
            {
                bootSM.ChangeStates(currentBootTransition);  // if boot is done change top level state with boot transition
                if (currentBootTransition == BootTransitions.BootDone)
                {
                    topSM.ChangeStates(TopTransitions.BootDone);
                }
            }
            
            textBox1.Text = topSM.CurrentState.ToString();  //update text boxes with current states
            textBox2.Text = autoSM.CurrentAutoState.ToString();
            textBox3.Text = bootSM.CurrentBootState.ToString();
        }
        public int intButtonClick(object sender)  //certain buttons return different values for test harness
        {
            if (sender == BootDone)
            {
                return 1;
            }
            else if (sender == TechConfirm)
            {
                return 2;
            }
            else if (sender == ProductValid)
            {
                return 3;
            }
            else if (sender == PacketSent)
            {
                return 4;
            }
            else if (sender == uConfirm1)
            {
                return 5;
            }
            else if (sender == uCnoResp1)
            {
                return 6;
            }
            else if (sender == Start1)
            {
                return 7;
            }
            else if (sender == DoneAuto)
            {
                return 8;
            }
            else if (sender == APnoResp1)
            {
                return 9;
            }
            else if (sender == VoltFail1)
            {
                return 10;
            }
            else if (sender == NewTest)
            {
                return 11;
            }
            else if (sender == Reconnected)
            {
                return 12;
            }
            else if (sender == Start2)
            {
                return 13;
            }
            else if (sender == MesssagesGenerated)
            {
                return 14;
            }
            else if (sender == PacketVoltage)
            {
                return 15;
            }
            else if (sender == PacketNotVoltage)
            {
                return 16;
            }
            else if (sender == VoltFail2)
            {
                return 17;
            }
            else if (sender == uCnoResp2)
            {
                return 18;
            }
            else if (sender == VoltSuccess)
            {
                return 19;
            }
            else if (sender == uCconfirmNoMessageAvailable)
            {
                return 20;
            }
            else if (sender == ucConfirmMessageAvailable)
            {
                return 21;
            }
            else if (sender == DelayDone)
            {
                return 22;
            }
            else if (sender == APnoResp2)
            {
                return 23;
            }
            else if (sender == APdoneNoTestsAvailable)
            {
                return 24;
            }
            else if (sender == APdoneTestsAvailable)
            {
                return 25;
            }
            else if (sender == APtimeout)
            {
                return 26;
            }
            else if (sender == DelayLowCount)
            {
                return 27;
            }
            else if (sender == DelayHighCount)
            {
                return 28;
            }
            else if (sender == APopen)
            {
                return 29;
            }
            else if (sender == PacketSent3)
            {
                return 30;
            }
            else if (sender == uCtimeoutLow)
            {
                return 31;
            }
            else if (sender == uCtimeoutHigh)
            {
                return 32;
            }
            else if (sender == uCpassAPfail)
            {
                return 33;
            }
            else if (sender == bothPass)
            {
                return 34;
            }
            else if (sender == Reboot)
            {
                return 35;
            }
            else if (sender == BootDone2)
            {
                return 36;
            }
            else
            {
                return 0;
            }
        }


    }
}
