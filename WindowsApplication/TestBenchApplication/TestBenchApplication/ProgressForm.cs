using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows.Forms;
using TestBenchApplication;

namespace UA_GUI
{

    public partial class ProgressForm : Form
    {
        //PUBLIC OBJECTS AND VARS
        public static Dictionary<string, Dictionary<string, bool>> SignalPath;
        public static System.Timers.Timer workerTimer = new System.Timers.Timer();
        public delegate void RunBGDel(object sender, ElapsedEventArgs e);

        //PRIVATE OBJECTS AND VARS
        private static int update = 0;
        //static BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public ProgressForm()
        {
            initializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            initializeBackgroundWorker();
        }

        //PRIVATE METHODS
        private void initializeBackgroundWorker()
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
        private void form3_Load(object sender, EventArgs e)
        {

            progressBar1.Value = 0;

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

        private void cancel_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();

            // Disable the Cancel button.
            cancelBtn.Enabled = false;
            programSM.Instance.ChangeStates(ProgramTransitions.Cancel);
            Form form = new ProductSelect();
            form.Show();
            this.Close();
        }

        private void seeReport_Click(object sender, EventArgs e)
        {
     

            this.Hide();
            Form resultform = new FullResultForm();
            resultform.Show();
        }

        /* The background workers allow events to effect the progress bar */
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string signalPathName = AudioPrecisionRunner.Instance.CurrentSignalName;
            string signalPathTmp = "";
            int percentDone = 0;
            int totalSignals = AudioPrecisionRunner.Instance.UpdateMeasurementCounters();
            int signalCount = AudioPrecisionRunner.Instance.NumberOfRanSignals;//this might be where we run into trouble
            while (percentDone < 100)
            {
                signalPathName = AudioPrecisionRunner.Instance.CurrentSignalName;
                //totalSignals = AudioPrecisionRunner.Instance.UpdateMeasurementCounters();
                signalCount = AudioPrecisionRunner.Instance.NumberOfRanSignals;
                percentDone = (signalCount * 100) / totalSignals;
                if (signalPathName != signalPathTmp)
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
                signalPathTmp = signalPathName;
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
            percentage.Text = e.ProgressPercentage.ToString() + "%";
            if (AudioPrecisionRunner.Instance.CurrentSignalName != null)
            {
                label1.Text = "Currently testing: " + AudioPrecisionRunner.Instance.CurrentSignalName;
            }
         
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = "Testing finished";
            AudioPrecisionRunner.Instance.NumberOfRanSignals = 0;
            //workerTimer.Enabled = false;
            seeReport.Visible = true;
        }




        private void initializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progresslbl = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.seeReport = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.percentage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(63, 38);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(260, 15);
            this.progressBar1.TabIndex = 0;
            // 
            // progresslbl
            // 
            this.progresslbl.AutoSize = true;
            this.progresslbl.Location = new System.Drawing.Point(8, 38);
            this.progresslbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.progresslbl.Name = "progresslbl";
            this.progresslbl.Size = new System.Drawing.Size(51, 13);
            this.progresslbl.TabIndex = 1;
            this.progresslbl.Text = "Progress:";
            this.progresslbl.Click += new System.EventHandler(this.label1_Click);
            // 
            // CancelBtn
            // 
            this.cancelBtn.AutoSize = true;
            this.cancelBtn.Location = new System.Drawing.Point(316, 83);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(2);
            this.cancelBtn.Name = "CancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(50, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancel_Click);
            // 
            // SeeReport
            // 
            this.seeReport.AutoSize = true;
            this.seeReport.BackColor = System.Drawing.SystemColors.Control;
            this.seeReport.Location = new System.Drawing.Point(241, 83);
            this.seeReport.Margin = new System.Windows.Forms.Padding(2);
            this.seeReport.Name = "SeeReport";
            this.seeReport.Size = new System.Drawing.Size(71, 23);
            this.seeReport.TabIndex = 3;
            this.seeReport.Text = "See Report";
            this.seeReport.UseVisualStyleBackColor = false;
            this.seeReport.Visible = false;
            this.seeReport.Click += new System.EventHandler(this.seeReport_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // Percentage
            // 
            this.percentage.AutoSize = true;
            this.percentage.Location = new System.Drawing.Point(327, 40);
            this.percentage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.percentage.Name = "Percentage";
            this.percentage.Size = new System.Drawing.Size(0, 13);
            this.percentage.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(60, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.Text = "Starting tests...";
            this.label1.TabIndex = 5;
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 117);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.percentage);
            this.Controls.Add(this.seeReport);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.progresslbl);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UA Testbench";
            this.Load += new System.EventHandler(this.form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progresslbl;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button seeReport;
        private System.Windows.Forms.Label percentage;

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
