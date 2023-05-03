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
            this.StartPosition = FormStartPosition.CenterScreen;
            initializeComponent();
        }

        //PRIVATE METHODS
        private void form1_Load(object sender, EventArgs e)
        {
            //makes sure all forms other than current is closed
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "SetUpForm")
                {
                    Application.OpenForms[i].Hide();
                }
            }

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
        private void Closing(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void next_Click(object sender, EventArgs e)
        {
            Form form = new ProductSelect();
            form.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
