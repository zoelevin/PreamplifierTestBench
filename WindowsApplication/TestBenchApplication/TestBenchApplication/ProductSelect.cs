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
        //PUBLIC OBJECTS AND VARS
        public static string productName = "";

        public ProductSelect()
        {
            
            InitializeComponent();
            programSM.Instance.ChangeStates(ProgramTransitions.BootDone);
            programSM.Instance.ChangeStates(ProgramTransitions.TechConfirm);
            NextBtn1.Hide();

    }
        //PRIVATE METHODS
        private void form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem= comboBox1.Items[0];
        }
        private void Closing(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                NextBtn1.Show();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void closeBtn1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void nextBtn1_Click(object sender, EventArgs e)
        {
            var product = new Product();
            product.Name = comboBox1.Text;
            productName = product.Name;
            foreach (var item in this.OwnedForms)
            {
                Console.WriteLine(item.Name);
            }
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            var form2 = new StartForm(product);
            AudioPrecisionRunner.Instance.ProductName=productName;
            switch (productName)
            {
                case ("610B Preamplifier Board"):
                    programSM.Instance.ChangeStates(ProgramTransitions.ProductSelectedValid);
                    AudioPrecisionRunner.Instance.OpenAudioPrecisionProject(path + "\\ApprojxFiles\\610B_UCSCeditionRev1.3.approjx");
                    programSM.Instance.AutoSM.allMessages.AddToMessages(Products.SixTenB);
                    if (programSM.Instance.TopSM.CurrentState != TopState.Reconnection)
                    {
                        form2.Show();
                    }
                    break;
            }
            this.Hide();    
        }
    }
}
