using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestBenchApplication;

namespace UA_GUI
{
    public partial class LoadForm : Form
    {
        //PUBLIC OBJECTS AND VARS
        static int i = 0;
        public LoadForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            label1.Hide();
           /* if (i == 0)
            {

                programSM.Instance.ChangeStates(ProgramTransitions.Reboot);

                i++;


            }*/

        }

        //PRIVATE METHODS
        private void label2_show(object sender, EventArgs e)
        {
            //unfortunately this is necessary to get
            //the load form to show up and start the
            //state machine without lag


            label1.Show();

        }
        private void Closing(object sender, EventArgs e)
        {
            
        }
        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LoadForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_show(object sender, EventArgs e)
        {
            //Explanation: The form will not be opened at the moment
            //the time the form is activated so there will be no load 
            //form to load the error forms off of. At the time the form is shown
            //the labels are not made yet and the state machine will block/lag the labels showing up.
            //So I hide a dummy label and dont "paint" it until the actual label is painted, and then
            //I put the state machine on the dummy's "paint" event
            
            if (i == 0)
            {

                programSM.Instance.ChangeStates(ProgramTransitions.Reboot);

                i++;


            }
            
        }

    }
}
