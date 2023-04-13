using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestBenchApplication;

namespace UA_GUI
{
    public partial class SetUpForm : Form
    {
        public SetUpForm()
        {
            programSM.Instance.ChangeStates(ProgramTransitions.BootDone);
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void next_Click(object sender, EventArgs e)
        {
            Form form = new ProductSelect();
            form.Show();
            this.Hide();
        }
    }
}
