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

        public StateMachinesTestForm()
        {
            InitializeComponent();
            
            ProgramSM.Instance.StateChangeEvent += OnStateChangeEvent; //subscribing to event
            this.TopMost = true;
        }
        private void StateMachinesTestForm_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            textBox1.Text = ProgramSM.Instance.topSM.CurrentState.ToString();
            textBox2.Text = ProgramSM.Instance.autoSM.CurrentAutoState.ToString();
            textBox3.Text = ProgramSM.Instance.bootSM.CurrentBootState.ToString();
            ProgramSM.Instance.ChangeStates(ProgramTransitions.Reboot);
        }
        public void OnStateChangeEvent(object sender, EventArgs e) //what to do when the event happens
        {
            textBox1.Invoke(new Action(() => textBox1.Text = ProgramSM.Instance.topSM.CurrentState.ToString()));
            textBox2.Invoke(new Action(() => textBox2.Text = ProgramSM.Instance.autoSM.CurrentAutoState.ToString()));
            textBox3.Invoke(new Action(() => textBox3.Text = ProgramSM.Instance.bootSM.CurrentBootState.ToString()));
        }
        private void button_Click(object sender, EventArgs e)
        {
            int var = intButtonClick(sender);   //any button click causes state transitions calculations for all states
            ProgramTransitions transition = (ProgramTransitions)var;
            ProgramSM.Instance.ChangeStates(transition);
        }
        public int intButtonClick(object sender)  //certain buttons return different values for test harness
        {
            if (sender == TechConfirm)
            {
                return 1;
            }
            else if (sender == ProductValid)
            {
                return 2;
            }
            else if (sender == PacketSent)
            {
                return 3;
            }
            else if (sender == uConfirm)
            {
                return 4;
            }
            else if (sender == uCnoResp)
            {
                return 5;
            }
            else if (sender == Start)
            {
                return 6;
            }            
            else if (sender == NewTest)
            {
                return 7;
            }
            else if (sender == Reconnected)
            {
                return 8;
            }          
            else if (sender == MesssagesGenerated)
            {
                return 9;
            }
            else if (sender == PacketVoltage)
            {
                return 10;
            }
            else if (sender == PacketNotVoltage)
            {
                return 11;
            }
            else if (sender == VoltFail)
            {
                return 12;
            }          
            else if (sender == VoltSuccess)
            {
                return 13;
            }
            else if (sender == uCconfirmNoMessageAvailable)
            {
                return 14;
            }
            else if (sender == ucConfirmMessageAvailable)
            {
                return 15;
            }
            else if (sender == DelayDone)
            {
                return 16;
            }
            else if (sender == APnoResp)
            {
                return 17;
            }
            else if (sender == APdoneNoTestsAvailable)
            {
                return 18;
            }
            else if (sender == APdoneTestsAvailable)
            {
                return 19;
            }
            else if (sender == APtimeout)
            {
                return 20;
            }
            else if (sender == DelayLowCount)
            {
                return 21;
            }
            else if (sender == DelayHighCount)
            {
                return 22;
            }
            else if (sender == APopen)
            {
                return 23;
            }            
            else if (sender == uCtimeoutLow)
            {
                return 24;
            }
            else if (sender == uCtimeoutHigh)
            {
                return 25;
            }
            else if (sender == uCpassAPfail)
            {
                return 26;
            }
            else if (sender == bothPass)
            {
                return 27;
            }
            else if (sender == Reboot)
            {
                return 28;
            }
            else if (sender == BootDone)
            {
                return 29;
            }
            else if (sender == uCcantConnect)
            {
                return 31;
            }
            else if (sender == uCcantFind)
            {
                return 30;
            }
            else if (sender == Cancel)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
