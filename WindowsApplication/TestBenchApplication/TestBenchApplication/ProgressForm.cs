using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;
using TestBenchApplication;
using System.Timers;

namespace UA_GUI
{

    public partial class ProgressForm : Form
    {
        public static Dictionary<string, Dictionary<string, bool>> SignalPath;
        public static System.Timers.Timer workerTimer = new System.Timers.Timer();
        public delegate void RunBGDel(object sender, ElapsedEventArgs e);
        private static int update = 0;
        //static BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        public ProgressForm()
        {                        
            InitializeComponent();
        }

        //This all happens before the form loads
        private void Form3_Load(object sender, EventArgs e)
        {
            workerTimer.Elapsed += RunBG;
            workerTimer.Interval = 1000;
            workerTimer.Enabled = true;
        }

        private void RunBG(object sender, ElapsedEventArgs e) {
            progressBar1.Value = update;
                try
                {
                    //Console.WriteLine("yes");
                    backgroundWorker1.RunWorkerAsync();
                }
                catch (Exception ex)
                {
                   // Console.WriteLine(ex.ToString());
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
            Form resultform = new FullResultForm();
            resultform.Show();
        }

        /* The background workers allow events to effect the progress bar */
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int percentDone = 0;
            int totalSignals = AudioPrecisionRunner.Instance.UpdateMeasurementCounters();
            int signalCount = AudioPrecisionRunner.Instance.NumberOfRanSignals;
                percentDone = (signalCount * 100) / totalSignals;
                try
                {
                    backgroundWorker1.ReportProgress(percentDone);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
        }

        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
            {
            Console.Write(e.ProgressPercentage.ToString());
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            //progressBar1.Value = e.ProgressPercentage;
            update = e.ProgressPercentage;
            // Set the text.
            //Percentage.Text = e.ProgressPercentage.ToString() + "%";
            if (e.ProgressPercentage >= 100)
            {
                backgroundWorker_RunWorkerCompleted();
            }

        }

        private void backgroundWorker_RunWorkerCompleted()
        {
            Console.WriteLine("done");
            workerTimer.Enabled = false;
            SeeReport.Visible = true;
        }
    }
}
