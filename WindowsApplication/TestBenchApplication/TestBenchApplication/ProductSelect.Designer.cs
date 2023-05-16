using System.Windows.Forms;
using System;

namespace UA_GUI
{
    partial class ProductSelect
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
            MdiClient ctlMDI;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductSelect));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.NextBtn1 = new System.Windows.Forms.Button();
            this.CloseBtn1 = new System.Windows.Forms.Button();
            this.Serial_Error = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "610B Preamplifier Board"});
            this.comboBox1.Location = new System.Drawing.Point(198, 63);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(293, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Product:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(356, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "Welcome to the set up wizard and GUI for the Universal Audio test bench.\r\nPlease " +
    "select the UA product you are testing";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // NextBtn1
            // 
            this.NextBtn1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.NextBtn1.AutoSize = true;
            this.NextBtn1.Location = new System.Drawing.Point(421, 91);
            this.NextBtn1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.NextBtn1.Name = "NextBtn1";
            this.NextBtn1.Size = new System.Drawing.Size(50, 23);
            this.NextBtn1.TabIndex = 0;
            this.NextBtn1.Text = "Next";
            this.NextBtn1.UseVisualStyleBackColor = true;
            this.NextBtn1.Click += new System.EventHandler(this.nextBtn1_Click);
            // 
            // CloseBtn1
            // 
            this.CloseBtn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBtn1.AutoSize = true;
            this.CloseBtn1.Location = new System.Drawing.Point(475, 90);
            this.CloseBtn1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CloseBtn1.Name = "CloseBtn1";
            this.CloseBtn1.Size = new System.Drawing.Size(50, 23);
            this.CloseBtn1.TabIndex = 7;
            this.CloseBtn1.Text = "Close";
            this.CloseBtn1.UseVisualStyleBackColor = true;
            this.CloseBtn1.Click += new System.EventHandler(this.closeBtn1_Click);
            // 
            // Serial_Error
            // 
            this.Serial_Error.Location = new System.Drawing.Point(0, 0);
            this.Serial_Error.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Serial_Error.Name = "Serial_Error";
            this.Serial_Error.Size = new System.Drawing.Size(67, 15);
            this.Serial_Error.TabIndex = 0;
            // 
            // ProductSelect
            // 
            this.AcceptButton = this.NextBtn1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(533, 121);
            this.Controls.Add(this.Serial_Error);
            this.Controls.Add(this.CloseBtn1);
            this.Controls.Add(this.NextBtn1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    // Attempt to cast the control to type MdiClient.
                    ctlMDI = (MdiClient)ctl;
                    // Set the BackColor of the MdiClient control.
                    ctlMDI.BackColor = this.BackColor;
                }
                catch (InvalidCastException exc)
                {
                    // Catch and ignore the error if casting failed.
                }
            }

            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ProductSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UA Testbench Set Up Wizard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Closing);
            this.Load += new System.EventHandler(this.form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Serial_Error;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button NextBtn1;
        private System.Windows.Forms.Button CloseBtn1;
    }
}

