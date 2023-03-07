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
    public partial class BootErrorForm : Form
    {
        public BootErrorForm()
        {
            InitializeComponent();
        }
        private void BootErrorForm_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 1700;
            textBox1.Text = "No Error";
            textBox2.Text = "No Error";
            textBox3.Text = "No Error";
            textBox4.Text = "No Error";
            UpdateErrors();
        }
        public void UpdateErrors()
        {
            if (ProgramSM.Instance.APnoPassFlag == true)
            {
                textBox1.Text = "Audio Precision measurement software was unable to be opened or was opened in Demo Mode";
            }
            if (ProgramSM.Instance.uCcantConnectFlag == true)
            {
                textBox2.Text = "The Arduino is visible on the serial ports, but cannot be connected to. Is there another program connected to the Arduino?";
            }
            if (ProgramSM.Instance.uCcantFindFlag == true)
            {
                textBox3.Text = "The Arduino is not visible on the serial ports, check and make sure it is plugged into the computer";
            }
            if (ProgramSM.Instance.uCnoRespFlag == true)
            {
                textBox4.Text = "The Arduino did not respond, or did not respond properly to a message";
            }
            return;
        }

        private void Reboot_Click(object sender, EventArgs e)
        {
            this.Close();
            ProgramSM.Instance.ChangeStates(ProgramTransitions.Reboot);
        }
    }
}
