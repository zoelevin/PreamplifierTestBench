﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace WindowsFormsApp1
{

    public class ArduinoComms
    {

        enum ReceivingSubState {
            ReadHead,
            ReadLength,
            ReadPayload,
            ReadTail,
            ReadSum,
            ReadEnd
        };

        static private ReceivingSubState thisState;
        static private byte thisLength;
        static private byte[] thisPayload;
        static private int payloadCount;
        static private byte thisSum;

        static readonly byte HEAD_BYTE = 0b10111111;
        static readonly byte TAIL_BYTE = 0b11011111;
        static readonly byte END_BYTE = 0b10001111;


        public static SerialPort port;
        public static string ArduinoPortName;
        public static string ConnectedStatus;

        //private static Message thisMessage = new Message();
        public static Queue<Message> Queue = new Queue<Message>();

        public ArduinoComms()
        {

            // Find and open COM port with arduino

            ArduinoPortName = AutodetectArduinoPort();
            //ArduinoPortName = "COM4";

            if (ArduinoPortName != null)
            {
                try
                {
                    port = new SerialPort(ArduinoPortName, 9600, Parity.None, 8, StopBits.One);
                    port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                    port.Open();

                    ConnectedStatus = "Connected to Arduino (" + ArduinoPortName + ").";
                }
                catch (Exception ex)
                {
                    ConnectedStatus = "Could not connect to Arduino, " + ArduinoPortName + " Busy.";
                    
                }
            } else
            {
                ConnectedStatus = "No Arduino Found, Check if device is plugged in.";
            }

            // Initialize Receiving Sub SM

            thisState = ReceivingSubState.ReadHead;

        }

        ~ArduinoComms()
        {
            try {
                port.Close();
            } finally
            {

            }
        }

        public string GetConnectedStatus()
        {
            return ConnectedStatus;
        }

        static private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            while (port.BytesToRead > 0)
            {
                byte thisByte = (byte)port.ReadByte();

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


                        // insert checksum check


                        if (thisByte == END_BYTE)
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

        public static void SerialWrite(byte[] packet, byte len)
        {
            if (port.IsOpen)
            {
                port.Write(packet, 0, len);
            }
        }


        public static byte CalculateChecksum(byte[] data, byte len)
        {
            byte sum = 0;

            for (int i = 0; i < len; i++)
            {
                sum |= data[i];
            }
            sum = (byte)~sum;

            //return sum;
            return 1;
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

}