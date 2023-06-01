using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UA_GUI;

namespace TestBenchApplication
{
    public partial class SetUp4b : Form
    {
        public SetUp4b()
        {
            InitializeComponent();
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
            Form form = new SetUp5();
            form.Show();
            this.Hide();
        }

        private void skip_Click(object sender, EventArgs e)
        {
            Form form = new LoadForm();
            form.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
