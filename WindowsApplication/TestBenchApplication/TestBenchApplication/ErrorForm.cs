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
        public static Form parentForm; 
        public ErrorForm(Form pForm, int error_code)
        {
            parentForm= pForm;
            error_message = new Label();
            //int error_code = (int)ErrorCode.uCnotVisible;
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
                            "Please plug the microcontroller in";
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
                        error_message.Text = "The Audio Precision software is not opening.";
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
            error_message.AutoSize = true;
            error_message.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            error_message.Location = new System.Drawing.Point(12, 9);
            error_message.Name = "error_message";
            error_message.Size = new System.Drawing.Size(117, 40);
            error_message.TabIndex = 0;
            Controls.Add(error_message);
            InitializeComponent();
            if (error_code == (int)ErrorCode.VoltageFail)
            {
                this.close_Btn.Hide();
                this.restart.Show();
            }
         
        }

        //Restart button
        private void restart_Click(object sender, EventArgs e)
        {
            
            Form form = new ProductSelect();
            form.Show();
            this.Close();
            parentForm.Hide();
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "ProductSelect")
                {
                    Application.OpenForms[i].Hide();
                }
            }

        }


        

        private void ErrorForm_Load(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
