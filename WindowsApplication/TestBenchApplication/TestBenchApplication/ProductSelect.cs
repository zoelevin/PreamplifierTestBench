using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TestBenchApplication;

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
            
            this.Hide();
            var form2 = new StartForm(product);
            switch (productName)
            {
                case ("LA610b"):
                    programSM.Instance.ChangeStates(ProgramTransitions.ProductSelectedValid);
                    AudioPrecisionRunner.Instance.OpenAudioPrecisionProject("C:\\Users\\mvinsonh\\Desktop\\GroupProject\\WindowsApplication\\TestBenchApplication\\610B_UCSCeditionRev1.3.approjx");
                    form2.Show();
                    break;
            }
        }
    }
}
