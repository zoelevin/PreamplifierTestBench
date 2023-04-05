using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;


namespace UA_GUI
{
    
    public partial class ProgressForm : Form
    {

        public static Dictionary<string, Dictionary<string, bool>> SignalPath;
        public ProgressForm()
        {
            InitializeComponent();
        }

        //This all happens before the form loads
        private void Form3_Load(object sender, EventArgs e)
        {
            
            try
            {
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
      
        }
    


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Form form = new ProductSelect();
            form.Show();
            this.Close();
        }

        private void SeeReport_Click(object sender, EventArgs e)
        {
            SignalPath =
                new Dictionary<string, Dictionary<string, bool>>()
             {
                {
                    "SignalPath1", new Dictionary<string, bool>()
                        {
                            {"meas1", true },
                            {"meas2", true },
                            {"meas3", false },
                            {"meas4", false }

                        }
                },

                {
                    "SignalPath2", new Dictionary<string, bool>()
                        {
                            {"meas1", true },
                            {"meas2", false },
                            {"meas3", true },
                            {"meas4", false },
                            {"meas5", false },
                            {"meas6", false },
                            {"meas7", true },
                            {"meas8", true },
                            {"meas9", true },
                            {"meas10", false },
                            {"meas11", true },
                            {"meas12", false },
                            {"meas13", false },
                            {"meas14", false },
                            {"meas15", true },
                            {"mea16", true }

                        }
                },
                {
                    "SignalPath3", new Dictionary<string, bool>()
                        {
                            {"meas1", true },
                            {"meas2", true },
                            {"meas3", true },
                            {"meas4", true }


                        }
                },
                {
                    "SignalPath4", new Dictionary<string, bool>()
                        {
                            {"meas1", true },
                            {"meas2", true },
                            {"meas3", true },
                            {"meas4", true }


                        }
                },
                {
                    "SignalPath5", new Dictionary<string, bool>()
                        {
                            {"meas1", true },
                            {"meas2", true },
                            {"meas3", true },
                            {"meas4", true }


                        }
                },
                {
                    "SignalPath6", new Dictionary<string, bool>()
                        {
                            {"meas1", true },
                            {"meas2", true },
                            {"meas3", true },
                            {"meas4", true }


                        }
                },
                             {
                    "SignalPath7", new Dictionary<string, bool>()
                        {
                            {"meas1", true },
                            {"meas2", true },
                            {"meas3", true },
                            {"meas4", true }


                        }
                },
                {
                    "SignalPath8", new Dictionary<string, bool>()
                        {
                            {"meas1", true },
                            {"meas2", true },
                            {"meas3", true },
                            {"meas4", true }


                        }
                },
                                {
                    "SignalPath9", new Dictionary<string, bool>()
                        {
                            {"meas1", true },
                            {"meas2", true },
                            {"meas3", false },
                            {"meas4", true }


                        }
                },


             };

            this.Hide();
            Form resultform= new FullResultForm();
            resultform.Show();
        }

        /* The background workers allow events to effect the progress bar */
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                // Wait 100 milliseconds.
                //try
                    //toplevelsm, do progressbar code here
                //except
                   //errorform
                Thread.Sleep(10); //Go to next state
                // Report progress.
                //if (state machine incremented){
                try
                {
                    backgroundWorker1.ReportProgress(i);
                }catch(Exception ex) {
                    Console.WriteLine(ex.ToString()); 
                }              
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBar1.Value = e.ProgressPercentage;
            // Set the text.
            Percentage.Text = e.ProgressPercentage.ToString() + "%";
   
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SeeReport.Visible = true;
        }


    }
}
