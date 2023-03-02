using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UA_GUI
{

    public partial class FullResultForm : Form
    {

        public FullResultForm()
        {
            FormatResults();
            InitializeComponent();

        }

        /* Parses name of button pressed */
        private string ConvertObjectToName(object sender)
        {
            string objString = sender.ToString();
            string[] stringSeparators = new string[] { "Text: " };
            string[] objStringList = objString.Split(stringSeparators, StringSplitOptions.None);
            string name = objStringList[1];
            return name;
        }

        private void FullResultForm_Load(object sender, EventArgs e)
        {

        }

        private void RestartBtn_Click(object sender, EventArgs e)
        {
            Form form = new ProductSelect();
            form.Show();
            this.Close();
        }

        /* Formats the Measurement Forms */
        private void FormatMeasurements(object sender, EventArgs e)
        {
            string name = ConvertObjectToName(sender);
            Form measurementForm = new MeasResults(name, ProgressForm.SignalPath[name]);
            measurementForm.Show();
        }

        /*Formats the main Results Form */
        private void FormatResults()
        {
            Dictionary<string, Dictionary<string, bool>> SignalPath = ProgressForm.SignalPath;
            //Point newLoc = new Point(5, 5);
            int numElem = SignalPath.Count;

            //Find size of form
            (int horizLen, int vertLen) = divisor(numElem);
            int maxMeasStr = longestDict(SignalPath);
            int formWidth = ((maxMeasStr * 17) + 20) * (horizLen);
            //20 is where first button is, 60 is how high button is,
            //40 gives room for restart button
            int formHeight = (20 + 40 + 60*vertLen); 

            //Place buttons
            int bIndex = 0;
            int realIndex = 0;
            int pointx, pointy = 0;
            foreach (var kvp in SignalPath)
            {
                Button b = new Button();
                b.Size = new Size(maxMeasStr * 17, 60);
                realIndex = 0;
                bIndex++;
                for (int j = 0; j < vertLen; j++)
                {
                    for (int i = 0; i < horizLen; i++)
                    {
                        realIndex++;
                        if (realIndex == bIndex)
                        {
                            pointx = (i * b.Width) + 20;
                            pointy = (j * b.Height) + 20;
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
                b.Text = kvp.Key;
                b.Name = kvp.Key;

                //Colors the buttons based on pass/fail results
                if (kvp.Value.ContainsValue(false) == true)
                {
                    b.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    b.BackColor = System.Drawing.Color.LimeGreen;
                }

                b.Click += new EventHandler(FormatMeasurements);
                Controls.Add(b);
            }

            //Add restart button to bottom
            Button RestartButton= new Button();
            //RestartButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            RestartButton.Location = new System.Drawing.Point((formWidth/2)-27, pointy + 70);
            RestartButton.Name = "RestartBtn";
            RestartButton.Size = new System.Drawing.Size(55, 24);
            RestartButton.Text = "Restart";
            RestartButton.UseVisualStyleBackColor = true;
            RestartButton.Click += new System.EventHandler(RestartBtn_Click);
            Controls.Add(RestartButton);

            this.ClientSize = new System.Drawing.Size(formWidth, formHeight);
            //this.AutoSize= true;
        }

        /* Using the number of buttons, calculates the least extreme
         * divisors. 1 is added to prime numbers and then their least
         * extreme divisors are calculated.
         * EX: if number = 12, then div1 = 3, div2 = 4
         *     if number = 17, then div1 = 3, div2 = 6 */

        public (int, int) divisor(int number)
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
                return divisor(number + 1);
            }
        }

        /* Find the length of the longest Measurement name in the dictionary */
        public int longestDict(Dictionary<string, Dictionary<string, bool>> SignalPath)
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




    }
}
