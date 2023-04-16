using System;
using System.IO.Ports;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;
using TestBenchApplication;
using System.Timers;
using System.Xml.Linq;

namespace UA_GUI
{

    public partial class ProgressForm : Form
    {
        public static Dictionary<string, Dictionary<string, bool>> SignalPath;
        public static System.Timers.Timer workerTimer = new System.Timers.Timer();
        public delegate void RunBGDel(object sender, ElapsedEventArgs e);
        private static int update = 0;
        //static BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public ProgressForm()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);
        }



        //This all happens before the form loads
        private void Form3_Load(object sender, EventArgs e)
        {
            /*    workerTimer.Elapsed += RunBG;
                workerTimer.Interval = 1000;
                workerTimer.Enabled = true;*/
            progressBar1.Value = 0;

            try
            {
                //Console.WriteLine("yes");
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                 Console.WriteLine(ex.ToString());
            }
        }

        private void RunBG(object sender, ElapsedEventArgs e)
        {
            /* progressBar1.Value = update;
                 try
                 {
                     //Console.WriteLine("yes");
                     backgroundWorker1.RunWorkerAsync();
                 }
                 catch (Exception ex)
                 {
                    // Console.WriteLine(ex.ToString());
                 }*/

        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();

            // Disable the Cancel button.
            CancelBtn.Enabled = false;
            programSM.Instance.ChangeStates(ProgramTransitions.Cancel);
            Form form = new ProductSelect();
            form.Show();
            this.Close();
        }

        private void SeeReport_Click(object sender, EventArgs e)
        {
             SignalPath = new Dictionary<string, Dictionary<string, bool>>()
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
            BackgroundWorker worker = sender as BackgroundWorker;
            int percentDone = 0;
            int percentDoneTmp = -1;
            int totalSignals = AudioPrecisionRunner.Instance.UpdateMeasurementCounters();
            int signalCount = AudioPrecisionRunner.NumberOfRanSignals;//this might be where we run into trouble
            while (percentDone < 100)
            {
                //totalSignals = AudioPrecisionRunner.Instance.UpdateMeasurementCounters();
                signalCount = AudioPrecisionRunner.NumberOfRanSignals;
                percentDone = (signalCount * 100) / totalSignals;
                if (percentDone != percentDoneTmp)
                {
                    try
                    {
                        worker.ReportProgress(percentDone);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                percentDoneTmp = percentDone;
            }
            try
            {
                worker.ReportProgress(percentDone);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            e.Result = percentDone;
        }

        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            Console.Write(e.ProgressPercentage.ToString());
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBar1.Value = e.ProgressPercentage;
            update = e.ProgressPercentage;
            // Set the text.
            Percentage.Text = e.ProgressPercentage.ToString() + "%";
            if (e.ProgressPercentage >= 100)
            {
                //backgroundWorker_RunWorkerCompleted();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AudioPrecisionRunner.NumberOfRanSignals = 0;
            Console.WriteLine("done");
            //workerTimer.Enabled = false;
            SeeReport.Visible = true;
        }




        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progresslbl = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SeeReport = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Percentage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(94, 59);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(390, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // progresslbl
            // 
            this.progresslbl.AutoSize = true;
            this.progresslbl.Location = new System.Drawing.Point(12, 59);
            this.progresslbl.Name = "progresslbl";
            this.progresslbl.Size = new System.Drawing.Size(76, 20);
            this.progresslbl.TabIndex = 1;
            this.progresslbl.Text = "Progress:";
            this.progresslbl.Click += new System.EventHandler(this.label1_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.AutoSize = true;
            this.CancelBtn.Location = new System.Drawing.Point(469, 95);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 33);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SeeReport
            // 
            this.SeeReport.AutoSize = true;
            this.SeeReport.BackColor = System.Drawing.SystemColors.Control;
            this.SeeReport.Location = new System.Drawing.Point(362, 95);
            this.SeeReport.Name = "SeeReport";
            this.SeeReport.Size = new System.Drawing.Size(101, 33);
            this.SeeReport.TabIndex = 3;
            this.SeeReport.Text = "See Report";
            this.SeeReport.UseVisualStyleBackColor = false;
            this.SeeReport.Visible = false;
            this.SeeReport.Click += new System.EventHandler(this.SeeReport_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            /*            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
                        this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
                        this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);*/
            // 
            // Percentage
            // 
            this.Percentage.AutoSize = true;
            this.Percentage.Location = new System.Drawing.Point(490, 62);
            this.Percentage.Name = "Percentage";
            this.Percentage.Size = new System.Drawing.Size(0, 20);
            this.Percentage.TabIndex = 4;
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 140);
            this.Controls.Add(this.Percentage);
            this.Controls.Add(this.SeeReport);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.progresslbl);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UA Testbench";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progresslbl;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SeeReport;
        private System.Windows.Forms.Label Percentage;

    }
}
