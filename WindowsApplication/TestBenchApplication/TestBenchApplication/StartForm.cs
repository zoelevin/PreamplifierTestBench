﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UA_GUI
{
    public partial class StartForm : Form
    {
        public Product Product { get; set; }

        public StartForm(Product product)
        {
            InitializeComponent();
            Product = product;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Console.WriteLine(Product.Name);
            Console.WriteLine(Product.SerialNumber);



        }


        private void cancelBtn_Click(object sender, EventArgs e)
        {
            var form1 = new ProductSelect();
            form1.Show();
            this.Hide();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            var form3 = new ProgressForm();

            form3.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
