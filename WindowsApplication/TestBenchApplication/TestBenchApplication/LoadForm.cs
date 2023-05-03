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
            initializeComponent();
            
           
        }

        //PRIVATE METHODS
        private void loadShow(object sender, EventArgs e)
        {
            if (i == 0)
            {
                programSM.Instance.ChangeStates(ProgramTransitions.Reboot);
            }
            i++;
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
