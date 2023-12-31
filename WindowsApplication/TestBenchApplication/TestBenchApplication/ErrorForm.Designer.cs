﻿namespace UA_GUI
{
    partial class ErrorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorForm));
            this.restart = new System.Windows.Forms.Button();
            this.close_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // restart
            // 
            this.restart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.restart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.restart.ForeColor = System.Drawing.SystemColors.ControlText;
            this.restart.Location = new System.Drawing.Point(1033, 159);
            this.restart.Margin = new System.Windows.Forms.Padding(2);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(50, 21);
            this.restart.TabIndex = 0;
            this.restart.Text = "Restart";
            this.restart.UseVisualStyleBackColor = true;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // close_Btn
            // 
            this.close_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.close_Btn.Location = new System.Drawing.Point(979, 159);
            this.close_Btn.Margin = new System.Windows.Forms.Padding(2);
            this.close_Btn.Name = "close_Btn";
            this.close_Btn.Size = new System.Drawing.Size(50, 21);
            this.close_Btn.TabIndex = 2;
            this.close_Btn.Text = "Close";
            this.close_Btn.UseVisualStyleBackColor = true;
            this.close_Btn.Click += new System.EventHandler(this.close_Click);
            // 
            // ErrorForm
            // 
            this.AcceptButton = this.restart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 184);
            this.Controls.Add(this.close_Btn);
            this.Controls.Add(this.restart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ErrorForm";
            this.Text = "Error Form";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.errorForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label error_message;
        private System.Windows.Forms.Button restart;
        private System.Windows.Forms.Button close_Btn;
    }
}