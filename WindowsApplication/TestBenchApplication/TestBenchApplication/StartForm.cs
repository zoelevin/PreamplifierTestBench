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
    public partial class StartForm : Form
    {
        //PUBLIC OBJECTS AND VARS
        public Product Product { get; set; }

        public StartForm(Product product)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            Product = product;
        }

        //PRIVATE METHODS
        private void form2_Load(object sender, EventArgs e)
        {
        
        }
        private void Closing(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            programSM.Instance.ChangeStates(ProgramTransitions.Start);
            this.Hide();
            if (programSM.Instance.TopSM.CurrentState != TopState.Reconnection)
            {
                var form3 = new ProgressForm();
                form3.Show();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            var form1 = new ProductSelect();
            form1.Show();
            this.Hide();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
