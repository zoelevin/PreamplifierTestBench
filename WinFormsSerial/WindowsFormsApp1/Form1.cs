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

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {

        static readonly byte HEAD_BYTE = 0b10111111;
        static readonly byte TAIL_BYTE = 0b11011111;
        static readonly byte END_BYTE = 0b10001111;

        static int ThisMessageID;
        static int ThisParam1;
        static int ThisParam2;
        static int ThisParam3;
        static int ThisParam4;
        static int ThisParam5;

        static int payloadLen = 1;


        public Form1()
        {
            InitializeComponent();


        }


        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ThisMessageID = listboxMessageID.SelectedIndex;
        }


        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            byte packetLen = (byte)(payloadLen + 5);
            byte[] thisPacket = new byte[packetLen];
            byte[] thisPayload = new byte[payloadLen];
            byte thisChecksum = 1;

            thisPayload[0] = (byte)ThisMessageID;

            thisPacket[0] = HEAD_BYTE;
            thisPacket[1] = (byte)payloadLen;
            thisPacket[2] = (byte)ThisMessageID;

           

            if (checkboxParam1.Checked)
            {
                thisPayload[1] = (byte)ThisParam1;
                thisPacket[3] = (byte)ThisParam1;

                if (checkboxParam2.Checked)
                {
                    thisPayload[2] = (byte)ThisParam2;
                    thisPacket[4] = (byte)ThisParam2;
                    if (checkboxParam3.Checked)
                    {
                        thisPayload[3] = (byte)ThisParam3;
                        thisPacket[5] = (byte)ThisParam3;
                        if (checkboxParam4.Checked)
                        {
                            thisPayload[4] = (byte)ThisParam4;
                            thisPacket[6] = (byte)ThisParam4;
                            if (checkboxParam5.Checked)
                            {
                                thisPayload[5] = (byte)ThisParam5;
                                thisPacket[7] = (byte)ThisParam5;

                            }
                        }
                    }
                }
            }



            thisPacket[payloadLen + 2] = TAIL_BYTE;
            thisChecksum = ArduinoComms.CalculateChecksum(thisPayload, (byte)payloadLen);
            thisPacket[payloadLen + 3] = thisChecksum;
            thisPacket[payloadLen + 4] = END_BYTE;
            
            



            labelOutput.Text = String.Format(
                "Messgae ID: {0}\n" +
                "Param1: {1}\n" +
                "Param2: {2}\n" +
                "Param3: {3}\n" +
                "Param4: {4}\n" +
                "Param5: {5}\n\n" +
                "Payload Length: {6}\n" +
                "Checksum: {7}\n" 
                //+ "Packet: {7:X2} {8:X2} {9:X2} {10:X2} {11:X2} {12:X2} {13:X2} {14:X2}"
                ,  
                ThisMessageID, ThisParam1, ThisParam2, ThisParam3, ThisParam4, ThisParam5,
                payloadLen, thisChecksum
                //, 
                //thisPacket[0], thisPacket[1], thisPacket[2], thisPacket[3], thisPacket[4],
                //thisPacket[5], thisPacket[6], thisPacket[7]
                );

            ArduinoComms.SerialWrite(thisPacket, packetLen);
        }

        

        // Update ThisParamN variables

        private void textboxParam1_TextChanged(object sender, EventArgs e)
        {
            if (checkboxParam1.Checked)
            {
                try { 
                ThisParam1 = int.Parse(textboxParam1.Text);
                } catch (Exception ex)
                {
                    ThisParam1 = 0;
                }
            } else
            {
                ThisParam1 = 0;
            }
        }

        private void textboxParam2_TextChanged(object sender, EventArgs e)
        {
            if (checkboxParam2.Checked)
            {
                ThisParam2 = int.Parse(textboxParam2.Text);
            }
            else
            {
                ThisParam2 = 0;
            }
        }

        private void textboxParam3_TextChanged(object sender, EventArgs e)
        {
            if (checkboxParam3.Checked)
            {
                ThisParam3 = int.Parse(textboxParam3.Text);
            }
            else
            {
                ThisParam3 = 0;
            }
        }

        private void textboxParam4_TextChanged(object sender, EventArgs e)
        {
            if (checkboxParam4.Checked)
            {
                ThisParam4 = int.Parse(textboxParam4.Text);
            }
            else
            {
                ThisParam4 = 0;
            }
        }

        private void textboxParam5_TextChanged(object sender, EventArgs e)
        {
            if (checkboxParam5.Checked)
            {
                ThisParam5 = int.Parse(textboxParam5.Text);
            }
            else
            {
                ThisParam5 = 0;
            }
        }

        private void checkboxParam1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxParam1.Checked)
            {
                ThisParam1 = int.Parse(textboxParam1.Text);
                payloadLen++;
            }
            else
            {
                ThisParam1 = 0;
                payloadLen--;
            }
        }

        private void checkboxParam2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxParam2.Checked)
            {
                ThisParam2 = int.Parse(textboxParam2.Text);
                payloadLen++;
            }
            else
            {
                ThisParam2 = 0;
                payloadLen--;
            }
        }

        private void checkboxParam3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxParam3.Checked)
            {
                ThisParam3 = int.Parse(textboxParam3.Text);
                payloadLen++;

            }
            else
            {
                ThisParam3 = 0;
                payloadLen--;
            }
        }

        private void checkboxParam4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxParam4.Checked)
            {
                ThisParam4 = int.Parse(textboxParam4.Text);
                payloadLen++;
            }
            else
            {
                ThisParam4 = 0;
                payloadLen--;
            }
        }

        private void checkboxParam5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxParam5.Checked)
            {
                ThisParam5 = int.Parse(textboxParam5.Text);
                payloadLen++;
            }
            else
            {
                ThisParam5 = 0;
                payloadLen--;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ArduinoComms.TryConnect();
            labelConnected.Text = ArduinoComms.ConnectedStatus;
        }

        private void buttonRefreshRx_Click(object sender, EventArgs e)
        {

            

            if (ArduinoComms.Queue.Count > 0)
            {
                
                Message message = ArduinoComms.Queue.Dequeue();

                labelInput.Text += "New Message:" +
                    " Type: " + message.Type +
                ", Param1: " + message.Param1 +
                ", Param2: " + message.Param2 + "\n";

            }
        }

        private void buttonClearRx_Click(object sender, EventArgs e)
        {
            labelInput.Text = string.Empty;
        }

        private void buttonAutoconnect_Click(object sender, EventArgs e)
        {

            ArduinoComms.TryConnect();
            labelConnected.Text = ArduinoComms.ConnectedStatus;

        }
    }
}
