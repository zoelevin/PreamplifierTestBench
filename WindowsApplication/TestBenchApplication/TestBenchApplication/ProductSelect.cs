using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TestBenchApplication;
using TestBenchApplication.Properties;

namespace UA_GUI
{
    /* This form is where they select the UA product
     * and enter the serial number */
    public partial class ProductSelect : Form
    {

        public static string productName = "";

        public ProductSelect()
        {

            InitializeComponent();
            programSM.Instance.ChangeStates(ProgramTransitions.TechConfirm);
            EventTest timerTest = new EventTest(this);

    }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(comboBox1.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CloseBtn1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void NextBtn1_Click(object sender, EventArgs e)
        {
            var product = new Product();
            product.Name = comboBox1.Text;
            productName = product.Name;
            // if (product.Name == "6176")
            //{
            foreach (var item in this.OwnedForms)
            {
                Console.WriteLine(item.Name);
            }
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

         
            var form2 = new StartForm(product);
            switch (productName)
            {
                case ("610B Preamplifier Board"):
                    programSM.Instance.ChangeStates(ProgramTransitions.ProductSelectedValid);
                    AudioPrecisionRunner.Instance.OpenAudioPrecisionProject(path + "\\610B_UCSCeditionRev1.3.approjx");
                    programSM.Instance.AutoSM.allMessages.AddToMessages(Products.SixTenB);
                    form2.Show();
                    break;
            }
            this.Hide();    
        }
    }
}
