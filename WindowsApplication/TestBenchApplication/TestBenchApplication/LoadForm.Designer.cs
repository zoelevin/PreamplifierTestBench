namespace UA_GUI
{
    partial class LoadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadForm));
            this.label1 = new System.Windows.Forms.Label();
            this.close = new System.Windows.Forms.Button();
            this.SuspendLayout();
           
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 10);
            /*this.Controls.Add(this.close);
            this.Controls.Add(this.label1);*/
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoadForm";
            this.Text = "Loading... ";
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Activated += new System.EventHandler(this.LoadShow);


        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button close;
    }
}