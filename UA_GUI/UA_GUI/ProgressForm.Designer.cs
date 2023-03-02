namespace UA_GUI
{
    partial class ProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
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
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // Percentage
            // 
            this.Percentage.AutoSize = true;
            this.Percentage.Location = new System.Drawing.Point(490, 62);
            this.Percentage.Name = "Percentage";
            this.Percentage.Size = new System.Drawing.Size(0, 20);
            this.Percentage.TabIndex = 4;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 140);
            this.Controls.Add(this.Percentage);
            this.Controls.Add(this.SeeReport);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.progresslbl);
            this.Controls.Add(this.progressBar1);
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UA Testbench";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label progresslbl;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SeeReport;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label Percentage;
    }
}