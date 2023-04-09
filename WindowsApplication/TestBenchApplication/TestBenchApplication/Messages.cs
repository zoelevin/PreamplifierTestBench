using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public enum Products { SixTenB=0, Tester}
    public class Messages
    {
        //PUBLIC OBJECTS AND VARS
        public Queue<MessageWithIndex> SixTenBmessages = new Queue<MessageWithIndex>();        //will hardcode these messages
        public Queue<MessageWithIndex> AnotherProduct = new Queue<MessageWithIndex>();

        //PUBLIC METHODS
        public Messages()
        {
        }
        public void AddToMessages(Products product)  //will have different init functions for each product
        {
            byte[] tempPayload = { 0b00000001 };
            switch (product)
            {
                case Products.Tester:
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 1, tempPayload));//dummy messages for testing
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(3, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(3, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(3, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(4, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(4, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(4, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(5, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(5, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(5, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(6, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(6, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(7, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(7, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(8, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(8, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(9, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(9, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(10, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(10, 1, tempPayload));
                    break;
                case Products.SixTenB:
                    //config
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 0b00000011, 0b00000000, 0b00000010 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 0b00000011, 0b00000001, 0b00000011 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 5, new byte[] { 0b00000101, 0b00000000, 0b00000100, 0b00000101, 0b00000110 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 5, new byte[] { 0b00000101, 0b00000001, 0b00001000, 0b00001001, 0b00001010 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 0b00001000, 0b00001100, 0b00001110 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 0b00001000, 0b00110000, 0b00001111 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 0b00001000, 0b00011111, 0b00010000 }));
                    //voltage checks
                    //SixTenBmessages.Enqueue(new MessageWithIndex(0, 2, new byte[] { 0b00001111, 0b0001100 }));
                    //SixTenBmessages.Enqueue(new MessageWithIndex(0, 2, new byte[] { 0b00001111, 0b00110000 }));
                    //SixTenBmessages.Enqueue(new MessageWithIndex(0, 2, new byte[] { 0b00001111, 0b00011111 }));
                    //first Audio Test
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 0b00001100, 0b00000000, 0b00000011 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 0b00001100, 0b00000001, 0b00000011 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 0b00001010, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 0b00001010, 0b00000001, 0b00000000 }));
                    //second audio test
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 3, new byte[] { 0b00001100, 0b00000000, 0b00000100 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 3, new byte[] { 0b00001100, 0b00000001, 0b00000011 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 3, new byte[] { 0b00001010, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 3, new byte[] { 0b00001010, 0b00000001, 0b00000000 }));
                    //third audio test
                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 3, new byte[] { 0b00001100, 0b00000000, 0b00000100 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 3, new byte[] { 0b00001100, 0b00000001, 0b00000011 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 3, new byte[] { 0b00001010, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 3, new byte[] { 0b00001010, 0b00000001, 0b00000001 }));
                    //fourth audio test
                    SixTenBmessages.Enqueue(new MessageWithIndex(3, 3, new byte[] { 0b00001100, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(3, 3, new byte[] { 0b00001100, 0b00000001, 0b00000011 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(3, 3, new byte[] { 0b00001010, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(3, 3, new byte[] { 0b00001010, 0b00000001, 0b00000000 }));
                    //fifth audio test
                    SixTenBmessages.Enqueue(new MessageWithIndex(4, 3, new byte[] { 0b00001100, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(4, 3, new byte[] { 0b00001100, 0b00000001, 0b00000100 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(4, 3, new byte[] { 0b00001010, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(4, 3, new byte[] { 0b00001010, 0b00000001, 0b00000000 }));
                    //sixth audio test
                    SixTenBmessages.Enqueue(new MessageWithIndex(5, 3, new byte[] { 0b00001100, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(5, 3, new byte[] { 0b00001100, 0b00000001, 0b00000101 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(5, 3, new byte[] { 0b00001010, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(5, 3, new byte[] { 0b00001010, 0b00000001, 0b00000000 }));
                    //seventh audio test
                    SixTenBmessages.Enqueue(new MessageWithIndex(6, 3, new byte[] { 0b00001100, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(6, 3, new byte[] { 0b00001100, 0b00000001, 0b00000110 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(6, 3, new byte[] { 0b00001010, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(6, 3, new byte[] { 0b00001010, 0b00000001, 0b00000000 }));
                    //eighth audio test
                    SixTenBmessages.Enqueue(new MessageWithIndex(7, 3, new byte[] { 0b00001100, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(7, 3, new byte[] { 0b00001100, 0b00000001, 0b00000111 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(7, 3, new byte[] { 0b00001010, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(7, 3, new byte[] { 0b00001010, 0b00000001, 0b00000000 }));
                    //ninth audio test
                    SixTenBmessages.Enqueue(new MessageWithIndex(8, 3, new byte[] { 0b00001100, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(8, 3, new byte[] { 0b00001100, 0b00000001, 0b00000111 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(8, 3, new byte[] { 0b00001010, 0b00000000, 0b00000001 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(8, 3, new byte[] { 0b00001010, 0b00000001, 0b00000000 }));
                    //tenth audio test
                    SixTenBmessages.Enqueue(new MessageWithIndex(9, 3, new byte[] { 0b00001100, 0b00000000, 0b00000001 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(9, 3, new byte[] { 0b00001100, 0b00000001, 0b00000011 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(9, 3, new byte[] { 0b00001010, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(9, 3, new byte[] { 0b00001010, 0b00000001, 0b00000000 }));
                    //eleventh audio test
                    SixTenBmessages.Enqueue(new MessageWithIndex(10, 3, new byte[] { 0b00001100, 0b00000000, 0b00000010 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(10, 3, new byte[] { 0b00001100, 0b00000001, 0b00000011 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(10, 3, new byte[] { 0b00001010, 0b00000000, 0b00000000 }));
                    SixTenBmessages.Enqueue(new MessageWithIndex(10, 3, new byte[] { 0b00001010, 0b00000001, 0b00000000 }));

                    break;
                default:
                    break;
            }
        }


        //STRUCTS
        public struct MessageWithIndex     //used for putting messages into send message function
        {
            public int ListIndex;   //need index as different messages sent for each test. ex messages with index 1 are for the first test, messages index 0 are config
            public byte length;
            public byte[] Payload;
            public MessageWithIndex(int index, byte len, byte[] pLoad)
            {
                ListIndex = index;
                length = len;
                Payload = pLoad;
            }
        }
    }
}
