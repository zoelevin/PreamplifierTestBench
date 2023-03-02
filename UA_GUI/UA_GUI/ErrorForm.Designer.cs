namespace UA_GUI
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
            this.error_message = new System.Windows.Forms.Label();
            this.restart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // error_message
            // 
            this.error_message.AutoSize = true;
            this.error_message.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.error_message.Location = new System.Drawing.Point(113, 107);
            this.error_message.Name = "error_message";
            this.error_message.Size = new System.Drawing.Size(51, 20);
            this.error_message.TabIndex = 0;
            this.error_message.Text = "label1";
            // 
            // restart
            // 
            this.restart.Location = new System.Drawing.Point(713, 186);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(75, 32);
            this.restart.TabIndex = 1;
            this.restart.Text = "Restart";
            this.restart.UseVisualStyleBackColor = true;
            this.restart.Click += new System.EventHandler(this.button1_Click);
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 230);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.error_message);
            this.Name = "ErrorForm";
            this.Text = "Error Form";
            this.Load += new System.EventHandler(this.ErrorForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label error_message;
        private System.Windows.Forms.Button restart;
    }
}