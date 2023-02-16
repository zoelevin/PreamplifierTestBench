namespace WindowsFormsApp1
{
    partial class Form1
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
            this.listboxMessageID = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textboxParam1 = new System.Windows.Forms.TextBox();
            this.textboxParam2 = new System.Windows.Forms.TextBox();
            this.textboxParam3 = new System.Windows.Forms.TextBox();
            this.textboxParam4 = new System.Windows.Forms.TextBox();
            this.checkboxParam1 = new System.Windows.Forms.CheckBox();
            this.checkboxParam2 = new System.Windows.Forms.CheckBox();
            this.checkboxParam3 = new System.Windows.Forms.CheckBox();
            this.checkboxParam4 = new System.Windows.Forms.CheckBox();
            this.buttonSendMessage = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.labelOutput = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelInput = new System.Windows.Forms.Label();
            this.buttonRefreshRx = new System.Windows.Forms.Button();
            this.labelConnected = new System.Windows.Forms.Label();
            this.buttonClearRx = new System.Windows.Forms.Button();
            this.textboxParam5 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkboxParam5 = new System.Windows.Forms.CheckBox();
            this.buttonAutoconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listboxMessageID
            // 
            this.listboxMessageID.FormattingEnabled = true;
            this.listboxMessageID.Items.AddRange(new object[] {
            "Connected",
            "Reset",
            "Config_Pot",
            "Config_Switch_2T",
            "Config_Switch_3T",
            "Config_Switch_5T",
            "Config_Switch_7T",
            "Config_Switch_11T",
            "Config_Voltage",
            "Set_Pot",
            "Set_Switch_2T",
            "Set_Switch_3T",
            "Set_Switch_5T",
            "Set_Switch_7T",
            "Set_Switch_11T",
            "Read_Voltage"});
            this.listboxMessageID.Location = new System.Drawing.Point(47, 42);
            this.listboxMessageID.Name = "listboxMessageID";
            this.listboxMessageID.Size = new System.Drawing.Size(102, 212);
            this.listboxMessageID.TabIndex = 0;
            this.listboxMessageID.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Message ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Input Param 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Input Param 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Input Param 3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(393, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Input Param 4";
            // 
            // textboxParam1
            // 
            this.textboxParam1.Location = new System.Drawing.Point(158, 42);
            this.textboxParam1.Name = "textboxParam1";
            this.textboxParam1.Size = new System.Drawing.Size(70, 20);
            this.textboxParam1.TabIndex = 6;
            this.textboxParam1.Text = "0";
            this.textboxParam1.TextChanged += new System.EventHandler(this.textboxParam1_TextChanged);
            // 
            // textboxParam2
            // 
            this.textboxParam2.Location = new System.Drawing.Point(237, 42);
            this.textboxParam2.Name = "textboxParam2";
            this.textboxParam2.Size = new System.Drawing.Size(70, 20);
            this.textboxParam2.TabIndex = 7;
            this.textboxParam2.Text = "0";
            this.textboxParam2.TextChanged += new System.EventHandler(this.textboxParam2_TextChanged);
            // 
            // textboxParam3
            // 
            this.textboxParam3.Location = new System.Drawing.Point(316, 42);
            this.textboxParam3.Name = "textboxParam3";
            this.textboxParam3.Size = new System.Drawing.Size(70, 20);
            this.textboxParam3.TabIndex = 8;
            this.textboxParam3.Text = "0";
            this.textboxParam3.TextChanged += new System.EventHandler(this.textboxParam3_TextChanged);
            // 
            // textboxParam4
            // 
            this.textboxParam4.Location = new System.Drawing.Point(396, 42);
            this.textboxParam4.Name = "textboxParam4";
            this.textboxParam4.Size = new System.Drawing.Size(70, 20);
            this.textboxParam4.TabIndex = 9;
            this.textboxParam4.Text = "0";
            this.textboxParam4.TextChanged += new System.EventHandler(this.textboxParam4_TextChanged);
            // 
            // checkboxParam1
            // 
            this.checkboxParam1.AutoSize = true;
            this.checkboxParam1.Location = new System.Drawing.Point(158, 68);
            this.checkboxParam1.Name = "checkboxParam1";
            this.checkboxParam1.Size = new System.Drawing.Size(59, 17);
            this.checkboxParam1.TabIndex = 10;
            this.checkboxParam1.Text = "Enable";
            this.checkboxParam1.UseVisualStyleBackColor = true;
            this.checkboxParam1.CheckedChanged += new System.EventHandler(this.checkboxParam1_CheckedChanged);
            // 
            // checkboxParam2
            // 
            this.checkboxParam2.AutoSize = true;
            this.checkboxParam2.Location = new System.Drawing.Point(237, 68);
            this.checkboxParam2.Name = "checkboxParam2";
            this.checkboxParam2.Size = new System.Drawing.Size(59, 17);
            this.checkboxParam2.TabIndex = 11;
            this.checkboxParam2.Text = "Enable";
            this.checkboxParam2.UseVisualStyleBackColor = true;
            this.checkboxParam2.CheckedChanged += new System.EventHandler(this.checkboxParam2_CheckedChanged);
            // 
            // checkboxParam3
            // 
            this.checkboxParam3.AutoSize = true;
            this.checkboxParam3.Location = new System.Drawing.Point(316, 68);
            this.checkboxParam3.Name = "checkboxParam3";
            this.checkboxParam3.Size = new System.Drawing.Size(59, 17);
            this.checkboxParam3.TabIndex = 12;
            this.checkboxParam3.Text = "Enable";
            this.checkboxParam3.UseVisualStyleBackColor = true;
            this.checkboxParam3.CheckedChanged += new System.EventHandler(this.checkboxParam3_CheckedChanged);
            // 
            // checkboxParam4
            // 
            this.checkboxParam4.AutoSize = true;
            this.checkboxParam4.Location = new System.Drawing.Point(396, 68);
            this.checkboxParam4.Name = "checkboxParam4";
            this.checkboxParam4.Size = new System.Drawing.Size(59, 17);
            this.checkboxParam4.TabIndex = 13;
            this.checkboxParam4.Text = "Enable";
            this.checkboxParam4.UseVisualStyleBackColor = true;
            this.checkboxParam4.CheckedChanged += new System.EventHandler(this.checkboxParam4_CheckedChanged);
            // 
            // buttonSendMessage
            // 
            this.buttonSendMessage.Location = new System.Drawing.Point(584, 40);
            this.buttonSendMessage.Name = "buttonSendMessage";
            this.buttonSendMessage.Size = new System.Drawing.Size(104, 23);
            this.buttonSendMessage.TabIndex = 14;
            this.buttonSendMessage.Text = "Send Packet";
            this.buttonSendMessage.UseVisualStyleBackColor = true;
            this.buttonSendMessage.Click += new System.EventHandler(this.buttonSendMessage_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Message Sent";
            // 
            // labelOutput
            // 
            this.labelOutput.BackColor = System.Drawing.SystemColors.Window;
            this.labelOutput.Location = new System.Drawing.Point(185, 116);
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(162, 276);
            this.labelOutput.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(393, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Incoming Messages";
            // 
            // labelInput
            // 
            this.labelInput.BackColor = System.Drawing.SystemColors.Window;
            this.labelInput.Location = new System.Drawing.Point(393, 116);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(376, 762);
            this.labelInput.TabIndex = 18;
            // 
            // buttonRefreshRx
            // 
            this.buttonRefreshRx.Location = new System.Drawing.Point(613, 88);
            this.buttonRefreshRx.Name = "buttonRefreshRx";
            this.buttonRefreshRx.Size = new System.Drawing.Size(75, 23);
            this.buttonRefreshRx.TabIndex = 19;
            this.buttonRefreshRx.Text = "Refresh";
            this.buttonRefreshRx.UseVisualStyleBackColor = true;
            this.buttonRefreshRx.Click += new System.EventHandler(this.buttonRefreshRx_Click);
            // 
            // labelConnected
            // 
            this.labelConnected.AutoSize = true;
            this.labelConnected.Location = new System.Drawing.Point(44, 865);
            this.labelConnected.Name = "labelConnected";
            this.labelConnected.Size = new System.Drawing.Size(81, 13);
            this.labelConnected.TabIndex = 20;
            this.labelConnected.Text = "labelConnected";
            // 
            // buttonClearRx
            // 
            this.buttonClearRx.Location = new System.Drawing.Point(694, 88);
            this.buttonClearRx.Name = "buttonClearRx";
            this.buttonClearRx.Size = new System.Drawing.Size(75, 23);
            this.buttonClearRx.TabIndex = 21;
            this.buttonClearRx.Text = "Clear";
            this.buttonClearRx.UseVisualStyleBackColor = true;
            this.buttonClearRx.Click += new System.EventHandler(this.buttonClearRx_Click);
            // 
            // textboxParam5
            // 
            this.textboxParam5.Location = new System.Drawing.Point(472, 42);
            this.textboxParam5.Name = "textboxParam5";
            this.textboxParam5.Size = new System.Drawing.Size(70, 20);
            this.textboxParam5.TabIndex = 22;
            this.textboxParam5.Text = "0";
            this.textboxParam5.TextChanged += new System.EventHandler(this.textboxParam5_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(469, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Input Param 5";
            // 
            // checkboxParam5
            // 
            this.checkboxParam5.AutoSize = true;
            this.checkboxParam5.Location = new System.Drawing.Point(472, 68);
            this.checkboxParam5.Name = "checkboxParam5";
            this.checkboxParam5.Size = new System.Drawing.Size(59, 17);
            this.checkboxParam5.TabIndex = 24;
            this.checkboxParam5.Text = "Enable";
            this.checkboxParam5.UseVisualStyleBackColor = true;
            this.checkboxParam5.CheckedChanged += new System.EventHandler(this.checkboxParam5_CheckedChanged);
            // 
            // buttonAutoconnect
            // 
            this.buttonAutoconnect.Location = new System.Drawing.Point(47, 839);
            this.buttonAutoconnect.Name = "buttonAutoconnect";
            this.buttonAutoconnect.Size = new System.Drawing.Size(104, 23);
            this.buttonAutoconnect.TabIndex = 25;
            this.buttonAutoconnect.Text = "Autoconnect";
            this.buttonAutoconnect.UseVisualStyleBackColor = true;
            this.buttonAutoconnect.Click += new System.EventHandler(this.buttonAutoconnect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 925);
            this.Controls.Add(this.buttonAutoconnect);
            this.Controls.Add(this.checkboxParam5);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textboxParam5);
            this.Controls.Add(this.buttonClearRx);
            this.Controls.Add(this.labelConnected);
            this.Controls.Add(this.buttonRefreshRx);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonSendMessage);
            this.Controls.Add(this.checkboxParam4);
            this.Controls.Add(this.checkboxParam3);
            this.Controls.Add(this.checkboxParam2);
            this.Controls.Add(this.checkboxParam1);
            this.Controls.Add(this.textboxParam4);
            this.Controls.Add(this.textboxParam3);
            this.Controls.Add(this.textboxParam2);
            this.Controls.Add(this.textboxParam1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listboxMessageID);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listboxMessageID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textboxParam1;
        private System.Windows.Forms.TextBox textboxParam2;
        private System.Windows.Forms.TextBox textboxParam3;
        private System.Windows.Forms.TextBox textboxParam4;
        private System.Windows.Forms.CheckBox checkboxParam1;
        private System.Windows.Forms.CheckBox checkboxParam2;
        private System.Windows.Forms.CheckBox checkboxParam3;
        private System.Windows.Forms.CheckBox checkboxParam4;
        private System.Windows.Forms.Button buttonSendMessage;
        private System.Windows.Forms.Label labelOutput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.Button buttonRefreshRx;
        private System.Windows.Forms.Label labelConnected;
        private System.Windows.Forms.Button buttonClearRx;
        private System.Windows.Forms.TextBox textboxParam5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkboxParam5;
        private System.Windows.Forms.Button buttonAutoconnect;
    }
}

