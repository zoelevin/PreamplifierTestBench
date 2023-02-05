using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestBenchApplication
{
    public partial class StateMachinesTestForm : Form
    {
        public event EventHandler ButtonClicked;
        public StateMachinesTestForm()
        {
            InitializeComponent();
            this.TopMost = true;
        }
        private void StateMachinesTestForm_Load(object sender, EventArgs e)
        {
            var topSM = new TopLevelStateMachine();
        }
        private void button_Click(object sender, EventArgs e)
        {
            intButtonClick(sender);

        }
        public int intButtonClick(object sender)
        {
            if (sender == button1)
            {
                return 1;
            }
            else if (sender == button2)
            {
                return 2;
            }
            else if (sender == button3)
            {
                return 3;
            }
            else if (sender == button4)
            {
                return 4;
            }
            else if (sender == button5)
            {
                return 5;
            }
            else if (sender == button6)
            {
                return 6;
            }
            else if (sender == button9)
            {
                return 12;
            }
            else if (sender == button10)
            {
                return 11;
            }
            else if (sender == button11)
            {
                return 10;
            }
            else if (sender == button12)
            {
                return 9;
            }
            else if (sender == button13)
            {
                return 8;
            }
            else if (sender == button14)
            {
                return 7;
            }else
            {
                return 0;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }
    }
}
