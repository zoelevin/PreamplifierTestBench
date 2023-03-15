using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public enum Products { SixTenB=0, RealTest}
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
                case Products.SixTenB:
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

                    SixTenBmessages.Enqueue(new MessageWithIndex(11, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(11, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(12, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(12, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(13, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(13, 1, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(14, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(14, 1, tempPayload));

                    break;
                case Products.RealTest:
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 5, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 5, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(0, 3, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 2, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 2, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 2, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 3, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(3, 3, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(4, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(4, 3, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(5, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(5, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(5, 3, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(6, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(6, 3, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(7, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(7, 3, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(8, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(8, 3, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(9, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(9, 3, tempPayload));

                    SixTenBmessages.Enqueue(new MessageWithIndex(10, 3, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(10, 3, tempPayload));


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
