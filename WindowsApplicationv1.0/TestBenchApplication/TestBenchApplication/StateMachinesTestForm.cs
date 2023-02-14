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
        ProgramSM programSM  = new ProgramSM(); //creating instnce of all SMs
        

        public StateMachinesTestForm()
        {
            InitializeComponent();
            this.TopMost = true;
        }
        private void StateMachinesTestForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = programSM.topSM.CurrentState.ToString();
            textBox2.Text = programSM.autoSM.CurrentAutoState.ToString();
            textBox3.Text = programSM.bootSM.CurrentBootState.ToString();
            //programSM.Init();

        }
        private void button_Click(object sender, EventArgs e)
        {
            if (sender != Update)
            {
                int var = intButtonClick(sender);   //any button click causes state transitions calculations for all states
                ProgramTransitions transition = (ProgramTransitions)var;
                programSM.ChangeStates(transition);
            }
            textBox1.Text = programSM.topSM.CurrentState.ToString();
            textBox2.Text = programSM.autoSM.CurrentAutoState.ToString();
            textBox3.Text = programSM.bootSM.CurrentBootState.ToString();
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
