﻿using System;
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
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
            InitializeComponent();
            EventTest timerTest = new EventTest(this);
            programSM.Instance.ChangeStates(ProgramTransitions.Reboot);
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
