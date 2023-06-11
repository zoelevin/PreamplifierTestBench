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
    public partial class SetUp2 : Form
    {
        public SetUp2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
            Form form = new SetUp3();
            form.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void skip_Click(object sender, EventArgs e)
        {
            Form form = new LoadForm();
            form.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            Form form = new SetUpForm();
            form.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
