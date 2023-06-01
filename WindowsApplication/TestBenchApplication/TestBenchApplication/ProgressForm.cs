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
            InitializeComponent();
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
            cancelBtn.Visible = false;
            seeReport.Visible = true;
           // seeReport.TabIndex = 0;
        }




        private void InitializeComponent()
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
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(179, 40);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(877, 47);
            this.progressBar1.TabIndex = 5;
            // 
            // progresslbl
            // 
            this.progresslbl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progresslbl.AutoSize = true;
            this.progresslbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progresslbl.Location = new System.Drawing.Point(-3, 40);
            this.progresslbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.progresslbl.Name = "progresslbl";
            this.progresslbl.Size = new System.Drawing.Size(178, 42);
            this.progresslbl.TabIndex = 4;
            this.progresslbl.Text = "Progress:";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.AutoSize = true;
            this.cancelBtn.Location = new System.Drawing.Point(1123, 151);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(2);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(50, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancel_Click);
            // 
            // seeReport
            // 
            this.seeReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.seeReport.AutoSize = true;
            this.seeReport.BackColor = System.Drawing.SystemColors.Control;
            this.seeReport.Location = new System.Drawing.Point(1045, 151);
            this.seeReport.Margin = new System.Windows.Forms.Padding(2);
            this.seeReport.Name = "seeReport";
            this.seeReport.Size = new System.Drawing.Size(71, 23);
            this.seeReport.TabIndex = 2;
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
            // percentage
            // 
            this.percentage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.percentage.AutoSize = true;
            this.percentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percentage.Location = new System.Drawing.Point(1145, 40);
            this.percentage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.percentage.Name = "percentage";
            this.percentage.Size = new System.Drawing.Size(72, 42);
            this.percentage.TabIndex = 1;
            this.percentage.Text = "0%";
            this.percentage.Click += new System.EventHandler(this.percentage_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(173, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Starting tests...";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // ProgressForm
            // 
            this.AcceptButton = this.seeReport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1188, 185);
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
            this.TopMost = true;
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

        private void percentage_Click(object sender, EventArgs e)
        {

        }
    }
}
