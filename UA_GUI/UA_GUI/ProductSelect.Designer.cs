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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductSelect));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SerialIn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            "LA610MK2",
            "6176",
            "LA610b"});
            this.comboBox1.Location = new System.Drawing.Point(297, 97);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(437, 28);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // SerialIn
            // 
            this.SerialIn.Location = new System.Drawing.Point(297, 189);
            this.SerialIn.Name = "SerialIn";
            this.SerialIn.Size = new System.Drawing.Size(437, 26);
            this.SerialIn.TabIndex = 1;
            this.SerialIn.TextChanged += new System.EventHandler(this.SerialIn_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Product:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Serial Number:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(529, 40);
            this.label3.TabIndex = 4;
            this.label3.Text = "Welcome to the set up wizard and GUI for the Universal Audio test bench.\r\nPlease " +
    "select the UA product you are testing";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(466, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Please enter the serial number of the UA product you are testing.";
            // 
            // NextBtn1
            // 
            this.NextBtn1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.NextBtn1.AutoSize = true;
            this.NextBtn1.Location = new System.Drawing.Point(632, 235);
            this.NextBtn1.Name = "NextBtn1";
            this.NextBtn1.Size = new System.Drawing.Size(75, 32);
            this.NextBtn1.TabIndex = 6;
            this.NextBtn1.Text = "Next";
            this.NextBtn1.UseVisualStyleBackColor = true;
            this.NextBtn1.Click += new System.EventHandler(this.NextBtn1_Click);
            // 
            // CloseBtn1
            // 
            this.CloseBtn1.AutoSize = true;
            this.CloseBtn1.Location = new System.Drawing.Point(713, 235);
            this.CloseBtn1.Name = "CloseBtn1";
            this.CloseBtn1.Size = new System.Drawing.Size(75, 31);
            this.CloseBtn1.TabIndex = 7;
            this.CloseBtn1.Text = "Close";
            this.CloseBtn1.UseVisualStyleBackColor = true;
            this.CloseBtn1.Click += new System.EventHandler(this.CloseBtn1_Click);
            // 
            // Serial_Error
            // 
            this.Serial_Error.Location = new System.Drawing.Point(0, 0);
            this.Serial_Error.Name = "Serial_Error";
            this.Serial_Error.Size = new System.Drawing.Size(100, 23);
            this.Serial_Error.TabIndex = 0;
            // 
            // ProductSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 278);
            this.Controls.Add(this.Serial_Error);
            this.Controls.Add(this.CloseBtn1);
            this.Controls.Add(this.NextBtn1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SerialIn);
            this.Controls.Add(this.comboBox1);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UA Testbench Set Up Wizard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Serial_Error;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox SerialIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button NextBtn1;
        private System.Windows.Forms.Button CloseBtn1;
    }
}

