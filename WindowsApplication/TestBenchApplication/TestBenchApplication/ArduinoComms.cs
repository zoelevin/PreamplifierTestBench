using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Windows.Forms;


namespace WindowsFormsApp1
{

    public static class ArduinoComms
    {

        // ENUMS AND READONLYS

        enum ReceivingSubState {
            ReadHead,
            ReadLength,
            ReadPayload,
            ReadTail,
            ReadSum,
            ReadEnd
        };

        static readonly byte HEAD_BYTE = 0b10111111;
        static readonly byte TAIL_BYTE = 0b11011111;
        static readonly byte END_BYTE =  0b10001111;

        // PRIVATE VARIABLES AND OBJECTS

        static private ReceivingSubState thisState;
        static private byte              thisLength;
        static private byte[]            thisPayload;
        static private int               payloadCount;
        static private byte              thisSum;
         
        // PUBLIC VARIABLES AND OBJECTS

        public static SerialPort Port = new SerialPort();
        public static string     PortName;
        public static string     ConnectedStatus;
        public static bool       IsConnected;


        public static Queue<Message> Queue = new Queue<Message>();

        static ArduinoComms()
        {

            // Initialize Receiving Sub SM

            thisState = ReceivingSubState.ReadHead;
            IsConnected = false;

        }

        static public bool TryConnect()
        {

            if (!IsConnected)
            {
                PortName = AutodetectArduinoPort();

                if (PortName != null)
                {
                    try
                    {
                        Port = new SerialPort(PortName, 9600, Parity.None, 8, StopBits.One);
                        Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
                        Port.Open();

                        ConnectedStatus = "Connected to Arduino (" + PortName + ").";
                        IsConnected = true;
                    }
                    catch (Exception ex)
                    {
                        ConnectedStatus = "Could not connect to Arduino, " + PortName + " Busy.";
                        IsConnected = false;
                    }
                }
                else
                {
                    ConnectedStatus = "No Arduino Found, Check if device is plugged in.";
                    IsConnected = false;
                }
            }

            return IsConnected;
        }


        static private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            while (Port.BytesToRead > 0)
            {
                byte thisByte = (byte)Port.ReadByte();

                switch (thisState)
                {
                    case ReceivingSubState.ReadHead:

                        if (thisByte == HEAD_BYTE)
                        {
                            thisState = ReceivingSubState.ReadLength;
                            thisLength = 0;
                        }
                        break;

                    case ReceivingSubState.ReadLength:

                        thisLength = thisByte;
                        thisState = ReceivingSubState.ReadPayload;
                        thisPayload = new byte[thisLength];
                        payloadCount = 0;
                        break;

                    case ReceivingSubState.ReadPayload:

                        thisPayload[payloadCount] = (byte)thisByte;
                        payloadCount++;
                        if (payloadCount >= thisLength)
                        {
                            thisState = ReceivingSubState.ReadTail;
                        }
                        break;

                    case ReceivingSubState.ReadTail:

                        if (thisByte == TAIL_BYTE)
                        {
                            thisState = ReceivingSubState.ReadSum;
                        }
                        else
                        {
                            thisState = ReceivingSubState.ReadHead;
                        }
                        break;

                    case ReceivingSubState.ReadSum:

                        thisSum = thisByte;
                        thisState = ReceivingSubState.ReadEnd;
                        break;

                    case ReceivingSubState.ReadEnd:

                        if ((thisByte == END_BYTE) && (thisSum == CalculateChecksum(thisPayload, (byte)thisPayload.Length)))
                        {
                            
                            if (thisPayload.Length == 1)
                            {
                                Queue.Enqueue(new Message(thisPayload[0], 0, 0));
                            } 
                            else if (thisPayload.Length == 2)
                            {
                                Queue.Enqueue(new Message(thisPayload[0], thisPayload[1], 0));
                            }
                            else if (thisPayload.Length == 3)
                            {
                                Queue.Enqueue(new Message(thisPayload[0], thisPayload[1], thisPayload[2]));
                            }

                        }

                        thisState = ReceivingSubState.ReadHead;
                        break;
                    default:
                        break;
                }

            }
            
        }
        public static void SendPacket(byte[] payload, byte len)
        {
            byte[] thisArray1 = { HEAD_BYTE };
            byte[] thisArray2 = {TAIL_BYTE, CalculateChecksum(payload, len), END_BYTE};

            byte[] thisPacket = (byte[])thisArray1.Concat(payload).Concat(thisArray2);

            Port.Write(thisPacket, 0, thisPacket.Length);
        }

        public static byte CalculateChecksum(byte[] data, byte len)
        {
            byte sum = 0;

            for (int i = 0; i < len; i++)
            {
                sum ^= data[i];
            }
            sum = (byte)~sum;

            return sum;
        }

        private static string AutodetectArduinoPort()
        {
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);

            try
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    string desc = item["Description"].ToString();
                    string deviceId = item["DeviceID"].ToString();

                    if (desc.Contains("Arduino"))
                    {
                        return deviceId;
                    }
                }
            }
            catch (ManagementException e)
            {
                
            }

            return null;
        }


    }

    public struct Message
    {
        public byte Type;
        public byte Param1;
        public byte Param2;
        public Message(byte t, byte p1, byte p2)
        {

            Type = t;
            Param1 = p1;
            Param2 = p2;


        }
    }

}