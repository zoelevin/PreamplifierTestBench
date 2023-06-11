using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestBenchApplication;
using System.IO;

namespace UA_GUI
{
    //Error codes from windows app
    enum ErrorCode : int
    {
        BootError,
        TestingError,
        VoltageError

    }

    /*This form shows the error message based on the enum*/
    public partial class ErrorForm : Form
    {
        //PUBLIC OBJECTS AND VARS
        public static Form parentForm;

        public ErrorForm(Form pForm, int error_code)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            parentForm = pForm;
            error_message = new Label();
                switch (error_code)
            {
                case (int)ErrorCode.BootError:
                    {
                        if (ErrorFlags.Instance.APnoPassFlag == true)
                        {
                            error_message.Text = "Error in Boot sequence: \r\n" +
                                "The AP software was unable to be opened or was opened in Demo mode. \r\n" +
                                "Make sure the AP is plugged in and turned on.\r\n" +
                                "Allow time for AP to fully turn on before hitting restart";
                        }
                        else if (ErrorFlags.Instance.UcCantFindFlag == true)
                        {
                            error_message.Text = "Error in Boot sequence: \r\n" +
                                  "The microcontroller is not visible on the Serial Port.\r\n" +
                                  "Make sure the arduino is plugged in and the arduino IDE is installed"; 

                        }
                        else if (ErrorFlags.Instance.UcCantConnectFlag == true)
                        {
                            error_message.Text = "Error in Boot sequence: \r\n" +
                                "The microcontroller was not able to be connected to.\r\n" +
                                "Is there another program connected to the microcontroller?";
                        }
                        else
                        {
                            error_message.Text = "Error in Boot sequence: \r\n" +
                                "The microcontroller is not responding";
                        }
                        break;
                    }
                case (int)ErrorCode.TestingError:
                    {
                        error_message.Text = "Error in testing sequence, the microcontroller stopped responding. \r\n" +
                            "Make sure the arduino is plugged in and the arduino IDE is installed.";
                        break;
                    }
                case (int)ErrorCode.VoltageError:
                    {
                        error_message.Text = "One of the voltages was unable to be verified.\r\n" +
                            "The testing process in unable to be continued.\r\n" +
                            "Make sure 48 volt switch is flipped on";
                        break;
                    }
                default:
                    {
                        break;
                    }     
            }
            Log_Errors(error_message.Text);
            error_message.AutoSize = true;
            error_message.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            error_message.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            error_message.Location = new System.Drawing.Point(12, 9);

            error_message.Name = "error_message";
            error_message.Size = new System.Drawing.Size(117, 40);
            error_message.TabIndex = 0;
            Controls.Add(error_message);
            InitializeComponent();
         
        }

        public static void Log_Errors(string error)
        {
            DateTime date1 = DateTime.Now;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int year = DateTime.Now.Year;
            string mdy = month.ToString() + "-" + day.ToString() + "-" + year.ToString();  

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filepath = path + "\\TestingReports\\ErrorLogs\\" + mdy + "_errorlog.txt";

            if (!File.Exists(filepath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine("Error Log");

                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(filepath))
            {
                sw.WriteLine(date1.ToString("F") + ": ");
                sw.WriteLine(error);
        
            }

        }

        //PRIVATE METHODS
        //Restart button
        private void restart_Click(object sender, EventArgs e)
        {

            if (programSM.Instance.TopSM.CurrentState == TopState.Boot)  //if in boot state handle reboot and open load form again
            {
                // Form form = new LoadForm();
                // form.Show();
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "LoadForm")
                    {
                        Application.OpenForms[i].Hide();
                    }
                    else
                    {
                        for (int ctl = Application.OpenForms[i].Controls.Count - 1; ctl >= 0; ctl--) {
                            Application.OpenForms[i].Text = "Restarting, please wait";
                            if (Application.OpenForms[i].Controls[ctl].Name == "label2") {
                                Application.OpenForms[i].Controls[ctl].Size = new System.Drawing.Size(621, 84);
                                Application.OpenForms[i].Controls[ctl].Text = "Restarting Audio Precision application\r\n" +
                                    "Please wait...";
                            }
                        }
                    }
               
                }
                programSM.Instance.ChangeStates(ProgramTransitions.Reboot);
                this.Close();
            }
            else if (programSM.Instance.TopSM.CurrentState == TopState.VoltageErrors)   //if in volateg error state, restart bring back to product select
            {
                programSM.Instance.ChangeStates(ProgramTransitions.NewTest);
                Form form = new ProductSelect();
                form.Show();
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "ProductSelect")
                    {
                        Application.OpenForms[i].Hide();
                    }
                }
            }
            else
            {
                int x = 0;
                programSM.Instance.ChangeStates(ProgramTransitions.Reconnected);//if in disconnection state, restart bring back to product select
                Form form = new ProductSelect();
                form.Show();
                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "ProductSelect")
                    {
                        Application.OpenForms[i].Hide();
                    }
                }
            }

            this.Close();
        }


        

        private void errorForm_Load(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "ErrorForm")
                    {
                       //Application.OpenForms[i].Hide();
                    }
                }

        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

    

        private void ErrorForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
