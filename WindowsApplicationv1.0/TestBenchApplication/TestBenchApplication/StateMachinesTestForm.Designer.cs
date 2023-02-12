namespace TestBenchApplication
{
    partial class StateMachinesTestForm
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
            this.TechConfirm = new System.Windows.Forms.Button();
            this.ProductValid = new System.Windows.Forms.Button();
            this.PacketSent = new System.Windows.Forms.Button();
            this.uConfirm = new System.Windows.Forms.Button();
            this.uCnoResp = new System.Windows.Forms.Button();
            this.Reconnected = new System.Windows.Forms.Button();
            this.NewTest = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MesssagesGenerated = new System.Windows.Forms.Button();
            this.PacketNotVoltage = new System.Windows.Forms.Button();
            this.uCconfirmNoMessageAvailable = new System.Windows.Forms.Button();
            this.VoltFail = new System.Windows.Forms.Button();
            this.APnoResp = new System.Windows.Forms.Button();
            this.DelayDone = new System.Windows.Forms.Button();
            this.PacketVoltage = new System.Windows.Forms.Button();
            this.ucConfirmMessageAvailable = new System.Windows.Forms.Button();
            this.VoltSuccess = new System.Windows.Forms.Button();
            this.APdoneTestsAvailable = new System.Windows.Forms.Button();
            this.APdoneNoTestsAvailable = new System.Windows.Forms.Button();
            this.APopen = new System.Windows.Forms.Button();
            this.APtimeout = new System.Windows.Forms.Button();
            this.DelayLowCount = new System.Windows.Forms.Button();
            this.DelayHighCount = new System.Windows.Forms.Button();
            this.uCtimeoutLow = new System.Windows.Forms.Button();
            this.uCtimeoutHigh = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.uCpassAPfail = new System.Windows.Forms.Button();
            this.bothPass = new System.Windows.Forms.Button();
            this.BootDone = new System.Windows.Forms.Button();
            this.Reboot = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TechConfirm
            // 
            this.TechConfirm.Location = new System.Drawing.Point(86, 60);
            this.TechConfirm.Name = "TechConfirm";
            this.TechConfirm.Size = new System.Drawing.Size(75, 23);
            this.TechConfirm.TabIndex = 1;
            this.TechConfirm.Text = "TechConfirm";
            this.TechConfirm.UseVisualStyleBackColor = true;
            this.TechConfirm.Click += new System.EventHandler(this.button_Click);
            // 
            // ProductValid
            // 
            this.ProductValid.Location = new System.Drawing.Point(37, 89);
            this.ProductValid.Name = "ProductValid";
            this.ProductValid.Size = new System.Drawing.Size(124, 23);
            this.ProductValid.TabIndex = 3;
            this.ProductValid.Text = "ProductSelectedValid";
            this.ProductValid.UseVisualStyleBackColor = true;
            this.ProductValid.Click += new System.EventHandler(this.button_Click);
            // 
            // PacketSent
            // 
            this.PacketSent.Location = new System.Drawing.Point(86, 118);
            this.PacketSent.Name = "PacketSent";
            this.PacketSent.Size = new System.Drawing.Size(75, 23);
            this.PacketSent.TabIndex = 4;
            this.PacketSent.Text = "PacketSent";
            this.PacketSent.UseVisualStyleBackColor = true;
            this.PacketSent.Click += new System.EventHandler(this.button_Click);
            // 
            // uConfirm
            // 
            this.uConfirm.Location = new System.Drawing.Point(167, 60);
            this.uConfirm.Name = "uConfirm";
            this.uConfirm.Size = new System.Drawing.Size(75, 23);
            this.uConfirm.TabIndex = 5;
            this.uConfirm.Text = "uCconfirm";
            this.uConfirm.UseVisualStyleBackColor = true;
            this.uConfirm.Click += new System.EventHandler(this.button_Click);
            // 
            // uCnoResp
            // 
            this.uCnoResp.Location = new System.Drawing.Point(167, 89);
            this.uCnoResp.Name = "uCnoResp";
            this.uCnoResp.Size = new System.Drawing.Size(75, 23);
            this.uCnoResp.TabIndex = 6;
            this.uCnoResp.Text = "uCnoResp";
            this.uCnoResp.UseVisualStyleBackColor = true;
            this.uCnoResp.Click += new System.EventHandler(this.button_Click);
            // 
            // Reconnected
            // 
            this.Reconnected.Location = new System.Drawing.Point(248, 89);
            this.Reconnected.Name = "Reconnected";
            this.Reconnected.Size = new System.Drawing.Size(89, 23);
            this.Reconnected.TabIndex = 11;
            this.Reconnected.Text = "Reconnected";
            this.Reconnected.UseVisualStyleBackColor = true;
            this.Reconnected.Click += new System.EventHandler(this.button_Click);
            // 
            // NewTest
            // 
            this.NewTest.Location = new System.Drawing.Point(248, 60);
            this.NewTest.Name = "NewTest";
            this.NewTest.Size = new System.Drawing.Size(75, 23);
            this.NewTest.TabIndex = 10;
            this.NewTest.Text = "NewTest";
            this.NewTest.UseVisualStyleBackColor = true;
            this.NewTest.Click += new System.EventHandler(this.button_Click);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(167, 118);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 12;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(563, 257);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Auto Current State (Sub)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(161, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "Main SM Controls";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(131, 257);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 25;
            this.label4.Text = "Current State (Top)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(556, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 15);
            this.label5.TabIndex = 26;
            this.label5.Text = "Automatic SM Controls";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(1001, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 15);
            this.label6.TabIndex = 27;
            this.label6.Text = "Boot SM Controls";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(86, 275);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(203, 20);
            this.textBox1.TabIndex = 28;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(519, 275);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(203, 20);
            this.textBox2.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label3.Location = new System.Drawing.Point(178, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Top Level";
            // 
            // MesssagesGenerated
            // 
            this.MesssagesGenerated.Location = new System.Drawing.Point(448, 65);
            this.MesssagesGenerated.Name = "MesssagesGenerated";
            this.MesssagesGenerated.Size = new System.Drawing.Size(87, 23);
            this.MesssagesGenerated.TabIndex = 32;
            this.MesssagesGenerated.Text = "MessagesGen";
            this.MesssagesGenerated.UseVisualStyleBackColor = true;
            this.MesssagesGenerated.Click += new System.EventHandler(this.button_Click);
            // 
            // PacketNotVoltage
            // 
            this.PacketNotVoltage.Location = new System.Drawing.Point(540, 36);
            this.PacketNotVoltage.Name = "PacketNotVoltage";
            this.PacketNotVoltage.Size = new System.Drawing.Size(122, 23);
            this.PacketNotVoltage.TabIndex = 33;
            this.PacketNotVoltage.Text = "PacketSent+!Volt";
            this.PacketNotVoltage.UseVisualStyleBackColor = true;
            this.PacketNotVoltage.Click += new System.EventHandler(this.button_Click);
            // 
            // uCconfirmNoMessageAvailable
            // 
            this.uCconfirmNoMessageAvailable.Location = new System.Drawing.Point(435, 147);
            this.uCconfirmNoMessageAvailable.Name = "uCconfirmNoMessageAvailable";
            this.uCconfirmNoMessageAvailable.Size = new System.Drawing.Size(122, 23);
            this.uCconfirmNoMessageAvailable.TabIndex = 34;
            this.uCconfirmNoMessageAvailable.Text = "uCconfirm+MessEmpt";
            this.uCconfirmNoMessageAvailable.UseVisualStyleBackColor = true;
            this.uCconfirmNoMessageAvailable.Click += new System.EventHandler(this.button_Click);
            // 
            // VoltFail
            // 
            this.VoltFail.Location = new System.Drawing.Point(541, 94);
            this.VoltFail.Name = "VoltFail";
            this.VoltFail.Size = new System.Drawing.Size(121, 23);
            this.VoltFail.TabIndex = 36;
            this.VoltFail.Text = "VoltageFail";
            this.VoltFail.UseVisualStyleBackColor = true;
            this.VoltFail.Click += new System.EventHandler(this.button_Click);
            // 
            // APnoResp
            // 
            this.APnoResp.Location = new System.Drawing.Point(563, 147);
            this.APnoResp.Name = "APnoResp";
            this.APnoResp.Size = new System.Drawing.Size(87, 23);
            this.APnoResp.TabIndex = 38;
            this.APnoResp.Text = "APnoResp";
            this.APnoResp.UseVisualStyleBackColor = true;
            this.APnoResp.Click += new System.EventHandler(this.button_Click);
            // 
            // DelayDone
            // 
            this.DelayDone.Location = new System.Drawing.Point(563, 118);
            this.DelayDone.Name = "DelayDone";
            this.DelayDone.Size = new System.Drawing.Size(87, 23);
            this.DelayDone.TabIndex = 39;
            this.DelayDone.Text = "DelayDone";
            this.DelayDone.UseVisualStyleBackColor = true;
            this.DelayDone.Click += new System.EventHandler(this.button_Click);
            // 
            // PacketVoltage
            // 
            this.PacketVoltage.Location = new System.Drawing.Point(541, 65);
            this.PacketVoltage.Name = "PacketVoltage";
            this.PacketVoltage.Size = new System.Drawing.Size(122, 23);
            this.PacketVoltage.TabIndex = 40;
            this.PacketVoltage.Text = "PacketSent+Volt";
            this.PacketVoltage.UseVisualStyleBackColor = true;
            this.PacketVoltage.Click += new System.EventHandler(this.button_Click);
            // 
            // ucConfirmMessageAvailable
            // 
            this.ucConfirmMessageAvailable.Location = new System.Drawing.Point(435, 118);
            this.ucConfirmMessageAvailable.Name = "ucConfirmMessageAvailable";
            this.ucConfirmMessageAvailable.Size = new System.Drawing.Size(122, 23);
            this.ucConfirmMessageAvailable.TabIndex = 41;
            this.ucConfirmMessageAvailable.Text = "uCconfirm+!MessEmpt";
            this.ucConfirmMessageAvailable.UseVisualStyleBackColor = true;
            this.ucConfirmMessageAvailable.Click += new System.EventHandler(this.button_Click);
            // 
            // VoltSuccess
            // 
            this.VoltSuccess.Location = new System.Drawing.Point(669, 65);
            this.VoltSuccess.Name = "VoltSuccess";
            this.VoltSuccess.Size = new System.Drawing.Size(108, 23);
            this.VoltSuccess.TabIndex = 42;
            this.VoltSuccess.Text = "VoltageSucess";
            this.VoltSuccess.UseVisualStyleBackColor = true;
            this.VoltSuccess.Click += new System.EventHandler(this.button_Click);
            // 
            // APdoneTestsAvailable
            // 
            this.APdoneTestsAvailable.Location = new System.Drawing.Point(656, 118);
            this.APdoneTestsAvailable.Name = "APdoneTestsAvailable";
            this.APdoneTestsAvailable.Size = new System.Drawing.Size(122, 23);
            this.APdoneTestsAvailable.TabIndex = 44;
            this.APdoneTestsAvailable.Text = "APdone+!TestEmp";
            this.APdoneTestsAvailable.UseVisualStyleBackColor = true;
            this.APdoneTestsAvailable.Click += new System.EventHandler(this.button_Click);
            // 
            // APdoneNoTestsAvailable
            // 
            this.APdoneNoTestsAvailable.Location = new System.Drawing.Point(656, 147);
            this.APdoneNoTestsAvailable.Name = "APdoneNoTestsAvailable";
            this.APdoneNoTestsAvailable.Size = new System.Drawing.Size(122, 23);
            this.APdoneNoTestsAvailable.TabIndex = 43;
            this.APdoneNoTestsAvailable.Text = "APdne+TestEmpt";
            this.APdoneNoTestsAvailable.UseVisualStyleBackColor = true;
            this.APdoneNoTestsAvailable.Click += new System.EventHandler(this.button_Click);
            // 
            // APopen
            // 
            this.APopen.Location = new System.Drawing.Point(1042, 60);
            this.APopen.Name = "APopen";
            this.APopen.Size = new System.Drawing.Size(87, 23);
            this.APopen.TabIndex = 45;
            this.APopen.Text = "APopen";
            this.APopen.UseVisualStyleBackColor = true;
            this.APopen.Click += new System.EventHandler(this.button_Click);
            // 
            // APtimeout
            // 
            this.APtimeout.Location = new System.Drawing.Point(876, 31);
            this.APtimeout.Name = "APtimeout";
            this.APtimeout.Size = new System.Drawing.Size(87, 23);
            this.APtimeout.TabIndex = 46;
            this.APtimeout.Text = "APtimeout";
            this.APtimeout.UseVisualStyleBackColor = true;
            this.APtimeout.Click += new System.EventHandler(this.button_Click);
            // 
            // DelayLowCount
            // 
            this.DelayLowCount.Location = new System.Drawing.Point(873, 60);
            this.DelayLowCount.Name = "DelayLowCount";
            this.DelayLowCount.Size = new System.Drawing.Size(163, 23);
            this.DelayLowCount.TabIndex = 47;
            this.DelayLowCount.Text = "CloseDelay+Count<2";
            this.DelayLowCount.UseVisualStyleBackColor = true;
            this.DelayLowCount.Click += new System.EventHandler(this.button_Click);
            // 
            // DelayHighCount
            // 
            this.DelayHighCount.Location = new System.Drawing.Point(873, 89);
            this.DelayHighCount.Name = "DelayHighCount";
            this.DelayHighCount.Size = new System.Drawing.Size(163, 23);
            this.DelayHighCount.TabIndex = 48;
            this.DelayHighCount.Text = "CloseDelay+Count=2";
            this.DelayHighCount.UseVisualStyleBackColor = true;
            this.DelayHighCount.Click += new System.EventHandler(this.button_Click);
            // 
            // uCtimeoutLow
            // 
            this.uCtimeoutLow.Location = new System.Drawing.Point(873, 118);
            this.uCtimeoutLow.Name = "uCtimeoutLow";
            this.uCtimeoutLow.Size = new System.Drawing.Size(129, 23);
            this.uCtimeoutLow.TabIndex = 53;
            this.uCtimeoutLow.Text = "uCtimeout+count<2";
            this.uCtimeoutLow.UseVisualStyleBackColor = true;
            this.uCtimeoutLow.Click += new System.EventHandler(this.button_Click);
            // 
            // uCtimeoutHigh
            // 
            this.uCtimeoutHigh.Location = new System.Drawing.Point(873, 147);
            this.uCtimeoutHigh.Name = "uCtimeoutHigh";
            this.uCtimeoutHigh.Size = new System.Drawing.Size(129, 23);
            this.uCtimeoutHigh.TabIndex = 55;
            this.uCtimeoutHigh.Text = "uCtimeout+count=2";
            this.uCtimeoutHigh.UseVisualStyleBackColor = true;
            this.uCtimeoutHigh.Click += new System.EventHandler(this.button_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(989, 257);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 15);
            this.label8.TabIndex = 57;
            this.label8.Text = "Boot Current State (Sub)";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(944, 275);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(203, 20);
            this.textBox3.TabIndex = 58;
            // 
            // uCpassAPfail
            // 
            this.uCpassAPfail.Location = new System.Drawing.Point(1042, 89);
            this.uCpassAPfail.Name = "uCpassAPfail";
            this.uCpassAPfail.Size = new System.Drawing.Size(87, 23);
            this.uCpassAPfail.TabIndex = 60;
            this.uCpassAPfail.Text = "uCyesAPno";
            this.uCpassAPfail.UseVisualStyleBackColor = true;
            this.uCpassAPfail.Click += new System.EventHandler(this.button_Click);
            // 
            // bothPass
            // 
            this.bothPass.Location = new System.Drawing.Point(1042, 118);
            this.bothPass.Name = "bothPass";
            this.bothPass.Size = new System.Drawing.Size(87, 23);
            this.bothPass.TabIndex = 61;
            this.bothPass.Text = "uCyesAPyes";
            this.bothPass.UseVisualStyleBackColor = true;
            this.bothPass.Click += new System.EventHandler(this.button_Click);
            // 
            // BootDone
            // 
            this.BootDone.Location = new System.Drawing.Point(1135, 89);
            this.BootDone.Name = "BootDone";
            this.BootDone.Size = new System.Drawing.Size(87, 23);
            this.BootDone.TabIndex = 63;
            this.BootDone.Text = "BootDone";
            this.BootDone.UseVisualStyleBackColor = true;
            this.BootDone.Click += new System.EventHandler(this.button_Click);
            // 
            // Reboot
            // 
            this.Reboot.Location = new System.Drawing.Point(1135, 60);
            this.Reboot.Name = "Reboot";
            this.Reboot.Size = new System.Drawing.Size(87, 23);
            this.Reboot.TabIndex = 62;
            this.Reboot.Text = "Reboot";
            this.Reboot.UseVisualStyleBackColor = true;
            this.Reboot.Click += new System.EventHandler(this.button_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(519, 342);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(203, 73);
            this.Cancel.TabIndex = 64;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.button_Click);
            // 
            // StateMachinesTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 450);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.BootDone);
            this.Controls.Add(this.Reboot);
            this.Controls.Add(this.bothPass);
            this.Controls.Add(this.uCpassAPfail);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.uCtimeoutHigh);
            this.Controls.Add(this.uCtimeoutLow);
            this.Controls.Add(this.DelayHighCount);
            this.Controls.Add(this.DelayLowCount);
            this.Controls.Add(this.APtimeout);
            this.Controls.Add(this.APopen);
            this.Controls.Add(this.APdoneTestsAvailable);
            this.Controls.Add(this.APdoneNoTestsAvailable);
            this.Controls.Add(this.VoltSuccess);
            this.Controls.Add(this.ucConfirmMessageAvailable);
            this.Controls.Add(this.PacketVoltage);
            this.Controls.Add(this.DelayDone);
            this.Controls.Add(this.APnoResp);
            this.Controls.Add(this.VoltFail);
            this.Controls.Add(this.uCconfirmNoMessageAvailable);
            this.Controls.Add(this.PacketNotVoltage);
            this.Controls.Add(this.MesssagesGenerated);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Reconnected);
            this.Controls.Add(this.NewTest);
            this.Controls.Add(this.uCnoResp);
            this.Controls.Add(this.uConfirm);
            this.Controls.Add(this.PacketSent);
            this.Controls.Add(this.ProductValid);
            this.Controls.Add(this.TechConfirm);
            this.Name = "StateMachinesTestForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.StateMachinesTestForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button TechConfirm;
        private System.Windows.Forms.Button ProductValid;
        private System.Windows.Forms.Button PacketSent;
        private System.Windows.Forms.Button uConfirm;
        private System.Windows.Forms.Button uCnoResp;
        private System.Windows.Forms.Button Reconnected;
        private System.Windows.Forms.Button NewTest;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button MesssagesGenerated;
        private System.Windows.Forms.Button PacketNotVoltage;
        private System.Windows.Forms.Button uCconfirmNoMessageAvailable;
        private System.Windows.Forms.Button VoltFail;
        private System.Windows.Forms.Button APnoResp;
        private System.Windows.Forms.Button DelayDone;
        private System.Windows.Forms.Button PacketVoltage;
        private System.Windows.Forms.Button ucConfirmMessageAvailable;
        private System.Windows.Forms.Button VoltSuccess;
        private System.Windows.Forms.Button APdoneTestsAvailable;
        private System.Windows.Forms.Button APdoneNoTestsAvailable;
        private System.Windows.Forms.Button APopen;
        private System.Windows.Forms.Button APtimeout;
        private System.Windows.Forms.Button DelayLowCount;
        private System.Windows.Forms.Button DelayHighCount;
        private System.Windows.Forms.Button uCtimeoutLow;
        private System.Windows.Forms.Button uCtimeoutHigh;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button uCpassAPfail;
        private System.Windows.Forms.Button bothPass;
        private System.Windows.Forms.Button BootDone;
        private System.Windows.Forms.Button Reboot;
        private System.Windows.Forms.Button Cancel;
    }
}

