using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace UA_GUI
{
    public partial class MeasResults : Form
    {
        private static System.Timers.Timer buttonTimer = new System.Timers.Timer();
        public static int nextButtonPressedFlag = 0;
        public static int nextFailedButtonPressedFlag = 0;
        public static int backButtonPressedFlag = 0;

        //iterator keeps track of measurement page index
        static int iterator = 0;
        public static Dictionary<string, bool> Measurements;
        public Button nextfailed = new System.Windows.Forms.Button();
        public Button next = new System.Windows.Forms.Button();
        public Button close = new System.Windows.Forms.Button();
        public Button back = new System.Windows.Forms.Button();
        public static string name;
        public MeasResults(string signalPathName, Dictionary<string, bool> Measured)
        {
            this.Name = name;
            this.Text = name;
            Measurements = Measured;
            name = signalPathName;
            MeasResultsFormat();
            
        }

        private void MeasResults_Load(object sender, EventArgs e)
        {

        }

        /* Formats the results of the measurements sub-dict */
        public void MeasResultsFormat()
        {
            int index_nextFailed;

            Controls.Clear();
            button_Layout();
            //new fxn HideShow_Controls()
            index_nextFailed = findIndex_NextFailed();
            if (index_nextFailed == -1)
            {
                nextfailed.Hide();
            }
            else
            {
                nextfailed.Show();
            }
            if (iterator == 0)
            {
                back.Hide();
            }
            else
            {
                back.Show();
            }
            if (iterator >= Measurements.Count-1)
            {
                next.Hide();
            }
            else
            {
                next.Show();
            }

            KeyValuePair<string, bool> kvp = Measurements.ElementAt(iterator);
            
            Label measName = new System.Windows.Forms.Label();
            measName.Text = kvp.Key;
            measName.Location = new System.Drawing.Point(46, 110);
            measName.Size = new System.Drawing.Size(437, 26);
            Controls.Add(measName);
            if (kvp.Value == false)
            {
                this.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                this.BackColor = System.Drawing.Color.LimeGreen;
            }
            
            InitializeComponent();

        }

        /* Lays out the buttons on the measurement form */
        public void button_Layout()
        {

            //
            // nextfailed
            //
            nextfailed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            nextfailed.Location = new System.Drawing.Point(524, 356);
            nextfailed.Name = "nextfailed";
            nextfailed.Size = new System.Drawing.Size(102, 31);
            nextfailed.TabIndex = 0;
            nextfailed.Text = "Next Failed";
            nextfailed.UseVisualStyleBackColor = true;
            nextfailed.Click += new System.EventHandler(nextfailed_Click);
            // 
            // next
            // 
            next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            next.Location = new System.Drawing.Point(632, 356);
            next.Name = "next";
            next.Size = new System.Drawing.Size(75, 29);
            next.TabIndex = 1;
            next.Text = "Next";
            next.UseVisualStyleBackColor = true;
            next.Click += new System.EventHandler(next_Click);
            // 
            // close
            // 
            close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            close.Location = new System.Drawing.Point(715, 356);
            close.Name = "close";
            close.Size = new System.Drawing.Size(75, 29);
            close.TabIndex = 2;
            close.Text = "Close";
            close.UseVisualStyleBackColor = true;
            close.Click += new System.EventHandler(close_Click);
            // 
            // back
            // 
            back.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            back.Location = new System.Drawing.Point(12, 356);
            back.Name = "back";
            back.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            back.Size = new System.Drawing.Size(75, 29);
            back.TabIndex = 3;
            back.Text = "Back";
            back.UseVisualStyleBackColor = true;
            back.Click += new System.EventHandler(back_Click);



            Controls.Add(back);
            Controls.Add(close);
            Controls.Add(next);
            Controls.Add(nextfailed);
        }
        /* Sets timer to make sure that buttons don't auto-click a bunch in a row */
        private static void SetTimer()
        {
            // Create a timer with a 300 ms interval.
            buttonTimer = new System.Timers.Timer(300);
            // Hook up the Elapsed event for the timer. 
            buttonTimer.Elapsed += OnTimedEvent;
            buttonTimer.AutoReset = false;
            buttonTimer.Enabled = true;
        }

        /* Timer to make sure that buttons don't auto-click a bunch in a row */
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (nextButtonPressedFlag == 1)
            {
                nextButtonPressedFlag = 0;
            }
            if (backButtonPressedFlag == 1)
            {
                backButtonPressedFlag = 0;
            }

            if (nextFailedButtonPressedFlag == 1)
            {
                nextFailedButtonPressedFlag = 0;
            }

        }
        private void nextfailed_Click(object sender, EventArgs e)
        {
            if (nextFailedButtonPressedFlag == 0)
            {
                nextFailedButtonPressedFlag = 1;
                int index_nextFailed = findIndex_NextFailed();

                //find index of next failed
                iterator = index_nextFailed;
                MeasResultsFormat();
                SetTimer();
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            if (backButtonPressedFlag == 0)
            {
                backButtonPressedFlag = 1;
                iterator = iterator - 1;
                MeasResultsFormat();
                SetTimer();
            }

        }

        private void next_Click(object sender, EventArgs e)
        {
            if (nextButtonPressedFlag == 0)
            {
                
                buttonTimer.Stop();
                buttonTimer.Dispose();
                nextButtonPressedFlag = 1;
                iterator = iterator + 1;

                
                MeasResultsFormat();
                SetTimer();
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            iterator = 0;
            this.Close();
        }

        /* Gets the index of the next failed measurement */
        public int findIndex_NextFailed()
        {
            int index_nextFailed = -1;

            for (int i = iterator +1; i <= Measurements.Count()-1;  i++)
            {
                KeyValuePair<string, bool> kvp = Measurements.ElementAt(i);
                if (kvp.Value == false)
                {
                    
                    index_nextFailed = i;
                    return index_nextFailed;
                }
            }
            return -1;
        }
    }
}
