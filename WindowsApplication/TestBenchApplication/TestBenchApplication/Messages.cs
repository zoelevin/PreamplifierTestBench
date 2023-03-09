using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public enum Products { SixTenB=0}
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
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(1, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 1, tempPayload));
                    SixTenBmessages.Enqueue(new MessageWithIndex(2, 1, tempPayload));
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
