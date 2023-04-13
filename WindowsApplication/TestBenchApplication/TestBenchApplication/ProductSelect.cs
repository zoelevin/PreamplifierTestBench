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

        //Checks serial number and adds error label if there's a non-integer in it
/*        private void SerialIn_TextChanged(object sender, EventArgs e)
        {
   
            
            if (SerialIn.Text != null)
            {
                if (SerialIn.Text.Any(Char.IsLetter))
                {
                    
                    Serial_Error.Location = new System.Drawing.Point(46, 110);
                    Serial_Error.Text = "Invalid serial number";
                    Serial_Error.Size = new System.Drawing.Size(437, 26);
                    Serial_Error.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                    this.Controls.Add(Serial_Error);
             

                }
                //Why doesnt this reset the text
                else 
                {
                    Serial_Error.Text = null;
                    this.Controls.Remove(Serial_Error);

                }



            }
        }*/

        //Next Button will take us to the next form based on dropdown box selection
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
                    AudioPrecisionRunner.Instance.OpenAudioPrecisionProject("C:\\Users\\mvinsonh\\Desktop\\GroupProject\\WindowsApplication\\TestBenchApplication\\610B_UCSCeditionRev1.3.approjx");
                    form2.Show();
                    break;
            }
        
            

                        //}

        }
    }
}
