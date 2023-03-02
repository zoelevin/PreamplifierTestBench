using System;
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
    //Error codes from windows app
    enum ErrorCode : int
    {
        uCnotVisible,
        uCnotConnected,
        uCnotResponding,
        APnotOpening,
        APnotResponding,
        VoltageFail,
        InvalidProduct
    }

    /*This form shows the error message based on the enum*/
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();
            //temporary until we get a way to get these from software
            int error_code = (int)ErrorCode.uCnotVisible;
            switch (error_code)
            {
                case (int)ErrorCode.uCnotVisible:
                    {
                        error_message.Text = "The microcontroller you are using is not visible.\r\n" +
                            "Please check your settings.";
                        break;
                    }
                case (int)ErrorCode.uCnotConnected:
                    {
                        error_message.Text = "The microcontroller you are using is not plugged in.\r\n" +
                            "Please plug the microcontroller i.n";
                        break;
                    }
                case (int)ErrorCode.uCnotResponding:
                    {
                        error_message.Text = "Microcontroller not responding\r\n" +
                            "Please check microcontroller.";
                        break;
                    }
                    case (int)ErrorCode.APnotOpening: 
                    {
                        error_message.Text = "The Audio Precision software is not opening." ;
                        break;
                    }
                case (int)ErrorCode.APnotResponding:
                    {
                        error_message.Text = "The Audio Precision software is not responding";
                        break;
                    }
                case (int)ErrorCode.VoltageFail:
                    {
                        error_message.Text = "The voltage checks failed.\r\n" +
                            "Please check the voltages on the UA board.";
                        break;
                    }
                case (int)ErrorCode.InvalidProduct:
                    {
                        error_message.Text = "The product you selected is not supported.\r\n" +
                            "Please select another product.";
                        break;
                    }
                    default: 
                    {
                        break;
                    }
               

            }

        }

        //Restart button
        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new ProductSelect();
            form.Show();
            this.Close();
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
