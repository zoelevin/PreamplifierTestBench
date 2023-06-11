using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestBenchApplication;

namespace UA_GUI
{

    public partial class FullResultForm : Form
    {
        //PRIVATE OBJECTS AND VARS
        private Dictionary<string, Dictionary<string, bool>> SignalPath;

        //PUBLIC OBJECTS AND VARS
        public FullResultForm()
        {
            formatResults();
            initializeComponent();

        }

        //PRIVATE METHODS
        /* Parses name of button pressed */
        private string convertObjectToName(object sender)
        {
            string objString = sender.ToString();
            string[] stringSeparators = new string[] { "Text: ", ": " };
            string[] objStringList = objString.Split(stringSeparators, StringSplitOptions.None);
            string name = objStringList[1];
            return name;
        }

        private void fullResultForm_Load(object sender, EventArgs e)
        {

        }
        private void Closing(object sender, EventArgs e)
        {
           // Application.Exit();
        }
        private void restartBtn_Click(object sender, EventArgs e)
        {
            Form form = new ProductSelect();
            programSM.Instance.ChangeStates(ProgramTransitions.NewTest);
            form.Show();
            this.Close();
        }

        /* Formats the Measurement Forms */
        private void formatMeasurements(object sender, EventArgs e)
        {
            string name = convertObjectToName(sender);
            Console.WriteLine(name);
            Form measurementForm = new MeasResults(name, SignalPath[name]);
            measurementForm.Show();
        }

        /*Formats the main Results Form */
        private void formatResults()
        {
            SignalPath = AudioPrecisionRunner.Instance.APISequenceReport;
            SignalPath.Remove("Dummy Signal Path For Report");
           
            
            int numElem = SignalPath.Count;

            //Find size of form
            (int horizLen, int vertLen) = Divisor(numElem);
            int maxMeasStr = LongestDict(SignalPath);
            int border = 20;
            int buttonWidth = ((maxMeasStr + 13) * 10);
            int formWidth = (buttonWidth + 20) * (horizLen);
            
            //20 is where first button is, 
            //55 gives room for buttons,
            //60 is how high button is
            int formHeight = (border + 55 + 80 + 60*vertLen);
            int formWidth2 = ((horizLen )*buttonWidth + 2*border );
            Rectangle screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            int resolutiondiv = 1;
            if (screenWidth.Width < formWidth2)
            {
                resolutiondiv = 2;
                formWidth = formWidth / resolutiondiv;
                //formHeight = formHeight/resolutiondiv;
                formWidth2 = formWidth2 / resolutiondiv;
                buttonWidth = buttonWidth / resolutiondiv;
                border = border / resolutiondiv;
            }

            //Place buttons
            int bIndex = 0;
            int realIndex = 0;
            int pointx = 0, pointy = 0;
            string fullTestIndicator = "PASSED";
            string seqIndicator = "PASSED";
            string product = ProductSelect.productName;
            foreach (var kvp in SignalPath)
            {
                
                Button b = new Button();
                b.Size = new Size(buttonWidth, 60);
                realIndex = 0;
                
                bIndex++;
                for (int j = 0; j < vertLen; j++)
                {
                    for (int i = 0; i < horizLen; i++)
                    {
                        realIndex++;
                        if (realIndex == bIndex)
                        {
                            pointx = (i * b.Width) + border;
                            pointy = (j * b.Height) + 80 + border;
                            b.Location = new Point(pointx, pointy);
                            break;
                        }
                    }
                    if (realIndex == bIndex)
                    {
                        break;
                    }
                }

                //Names buttons
                

                //Colors the buttons based on pass/fail results
                if (kvp.Value.ContainsValue(false) == true)
                {
                    int totalCount = kvp.Value.Sum(x=> x.Value == false ? 1 : 0);
                    b.BackColor = System.Drawing.Color.Red;
                    fullTestIndicator = "FAILED";
                    seqIndicator = totalCount + "/" + kvp.Value.Count() +" FAILED";
                }
                else
                {
                    b.BackColor = System.Drawing.Color.LimeGreen;
                    seqIndicator = " PASSED";
                }

                b.Text = kvp.Key + ": " + seqIndicator;
                b.Name = kvp.Key;
                b.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
                //b.Click += new EventHandler(formatMeasurements);
                Controls.Add(b);
            }
            Button FullSeq = new Button();
            
            FullSeq.Name = "ResBtn";
            FullSeq.Size = new System.Drawing.Size(250, 32);
            FullSeq.Location = new System.Drawing.Point((formWidth / 2) - (FullSeq.Width / 2), 55);
            FullSeq.Text = "Full Sequence Report";
            FullSeq.UseVisualStyleBackColor = true;
            FullSeq.Click += new EventHandler(OpenAdvReport);
            Controls.Add(FullSeq);

            Label APResults = new Label();
            APResults.Location = new System.Drawing.Point(20,20);
            APResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            APResults.Size = new System.Drawing.Size(750, 30);
            APResults.Name = "APlabel";
            APResults.Text = "Analysis finished for the " + product + ": Sequence " + fullTestIndicator;
            Controls.Add(APResults);


            //Add restart button to bottom
            Button RestartButton= new Button();
            //RestartButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            
            RestartButton.Name = "RestartBtn";
            RestartButton.Size = new System.Drawing.Size(75, 32);
            RestartButton.Location = new System.Drawing.Point((formWidth / 2) - (RestartButton.Width / 2), pointy + 70);
            RestartButton.Text = "Restart";
            RestartButton.UseVisualStyleBackColor = true;
            RestartButton.Click += new System.EventHandler(restartBtn_Click);
            Controls.Add(RestartButton);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(10, 10);
           
            this.ClientSize = new System.Drawing.Size(formWidth2, formHeight);
 
        }

        //PUBLIC METHODS
        /* Opens pdf of the final testing report */
        public void OpenAdvReport(object sender, EventArgs e)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filepath = path + "\\TestingReports\\" + AutomaticSM.FinalReportName + ".pdf";
            Console.Write(filepath);
            System.Diagnostics.Process.Start(filepath);

        }


        /* Using the number of buttons, calculates the least extreme
         * divisors. 1 is added to prime numbers and then their least
         * extreme divisors are calculated.
         * EX: if number = 12, then div1 = 3, div2 = 4
         *     if number = 17, then div1 = 3, div2 = 6 */
        public (int, int) Divisor(int number)
        {
            int i;
            int div1 = 0;
            int div2 = 0;
            for (i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                {
                    if (i > div1)
                    {
                        div2 = number / i;
                        div1 = i;
                    }
                }
            }
            if (div1 > 0)
            {
                return (div1, div2);
            }
            else
            {
                return Divisor(number + 1);
            }
        }

        /* Find the length of the longest Measurement name in the dictionary */
        public int LongestDict(Dictionary<string, Dictionary<string, bool>> SignalPath)
        {
            int maxlength = 0;
            foreach (var kvp in SignalPath)
            {
                if (kvp.Key.Length > maxlength)
                {
                    maxlength = kvp.Key.Length;
                }
            }
            return maxlength;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FullResultForm));
            this.SuspendLayout();
            // 
            // FullResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.ClientSize = new System.Drawing.Size(992, 1025);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FullResultForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }
    }
}
