using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public enum Products { SixTenB=0, YourProductHere}
    public class Messages
    {
        //PUBLIC OBJECTS AND VARS
        public Queue<MessageWithIndex> ProductMessages = new Queue<MessageWithIndex>();        //will hardcode these messages

        //PUBLIC METHODS
        public Messages()
        {
        }
        public void AddToMessages(Products product)  //will have different init functions for each product
        {
            ProductMessages.Clear();
            switch (product)
            {
                case Products.YourProductHere:
                    //your messages here
                    break;
                case Products.SixTenB:
                    //config
                    ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 3, 0, 2 })); // config 2throw switch #0 as pin 2
                    ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 3, 1, 3 })); // config 3throw switch #1 as pin 3
                    ProductMessages.Enqueue(new MessageWithIndex(0, 5, new byte[] { 5, 0, 4, 5, 6 })); // config 5throw switch #0 as pins 4,5,6
                    ProductMessages.Enqueue(new MessageWithIndex(0, 5, new byte[] { 5, 1, 8, 9, 10 })); // config 5throw switch #1 as pins 8,9,10
                    ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 8, 12, 14 })); // config 12V rail to pin 14
                    ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 8, 48, 15 })); // config 48V rail to pin 15
                    ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 8, 31, 16 })); // config 310V rail to pin 16
                    //voltage checks
                    //ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 15, 12, 175}));  // test 12v
                    //ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 15, 48, 175}));  // test 48v
                    //ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 15, 31, 175}));  // test 310v
                    //first Audio Test
                    ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 12, 0, 3 })); // input mic500
                    ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 12, 1, 3 })); // gain -10
                    ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(0, 3, new byte[] { 10, 1, 0 })); // pad off
                    //second audio test
                    ProductMessages.Enqueue(new MessageWithIndex(1, 3, new byte[] { 12, 0, 4 })); // input mic2k
                    ProductMessages.Enqueue(new MessageWithIndex(1, 3, new byte[] { 12, 1, 3 })); // gain -10
                    ProductMessages.Enqueue(new MessageWithIndex(1, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(1, 3, new byte[] { 10, 1, 0 })); // pad off
                    //third audio test
                    ProductMessages.Enqueue(new MessageWithIndex(2, 3, new byte[] { 12, 0, 4 })); // input mic2k
                    ProductMessages.Enqueue(new MessageWithIndex(2, 3, new byte[] { 12, 1, 3 })); // gain -10
                    ProductMessages.Enqueue(new MessageWithIndex(2, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(2, 3, new byte[] { 10, 1, 1 })); // pad on
                    //fourth audio test
                    ProductMessages.Enqueue(new MessageWithIndex(3, 3, new byte[] { 12, 0, 0 })); // input line
                    ProductMessages.Enqueue(new MessageWithIndex(3, 3, new byte[] { 12, 1, 3 })); // gain -10
                    ProductMessages.Enqueue(new MessageWithIndex(3, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(3, 3, new byte[] { 10, 1, 0 })); // pad off
                    //fifth audio test
                    ProductMessages.Enqueue(new MessageWithIndex(4, 3, new byte[] { 12, 0, 0 })); // input line
                    ProductMessages.Enqueue(new MessageWithIndex(4, 3, new byte[] { 12, 1, 3 })); // gain -10
                    ProductMessages.Enqueue(new MessageWithIndex(4, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(4, 3, new byte[] { 10, 1, 0 })); // pad off
                    //sixth audio test
                    ProductMessages.Enqueue(new MessageWithIndex(5, 3, new byte[] { 12, 0, 0 })); // input line
                    ProductMessages.Enqueue(new MessageWithIndex(5, 3, new byte[] { 12, 1, 6 })); // gain +5
                    ProductMessages.Enqueue(new MessageWithIndex(5, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(5, 3, new byte[] { 10, 1, 0 })); // pad off
                    //seventh audio test
                    ProductMessages.Enqueue(new MessageWithIndex(6, 3, new byte[] { 12, 0, 0 })); // input line
                    ProductMessages.Enqueue(new MessageWithIndex(6, 3, new byte[] { 12, 1, 6 })); // gain +5
                    ProductMessages.Enqueue(new MessageWithIndex(6, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(6, 3, new byte[] { 10, 1, 0 })); // pad off
                    //eighth audio test
                    ProductMessages.Enqueue(new MessageWithIndex(7, 3, new byte[] { 12, 0, 0 })); // input line
                    ProductMessages.Enqueue(new MessageWithIndex(7, 3, new byte[] { 12, 1, 4 })); // gain -5
                    ProductMessages.Enqueue(new MessageWithIndex(7, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(7, 3, new byte[] { 10, 1, 0 })); // pad off
                    //ninth audio test
                    ProductMessages.Enqueue(new MessageWithIndex(8, 3, new byte[] { 12, 0, 0 })); // input line
                    ProductMessages.Enqueue(new MessageWithIndex(8, 3, new byte[] { 12, 1, 5 })); // gain 0
                    ProductMessages.Enqueue(new MessageWithIndex(8, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(8, 3, new byte[] { 10, 1, 0 })); // pad off
                    //tenth audio test
                    ProductMessages.Enqueue(new MessageWithIndex(9, 3, new byte[] { 12, 0, 0 })); // input line
                    ProductMessages.Enqueue(new MessageWithIndex(9, 3, new byte[] { 12, 1, 7 })); // gain +10
                    ProductMessages.Enqueue(new MessageWithIndex(9, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(9, 3, new byte[] { 10, 1, 0 })); // pad off
                    //eleventh audio test
                    ProductMessages.Enqueue(new MessageWithIndex(10, 3, new byte[] { 12, 0, 0 })); // input line
                    ProductMessages.Enqueue(new MessageWithIndex(10, 3, new byte[] { 12, 1, 7 })); // gain +10
                    ProductMessages.Enqueue(new MessageWithIndex(10, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(10, 3, new byte[] { 10, 1, 0 })); // pad off
                    //twelfth audio test
                    ProductMessages.Enqueue(new MessageWithIndex(11, 3, new byte[] { 12, 0, 0 })); // input line
                    ProductMessages.Enqueue(new MessageWithIndex(11, 3, new byte[] { 12, 1, 7 })); // gain +10
                    ProductMessages.Enqueue(new MessageWithIndex(11, 3, new byte[] { 10, 0, 1 })); // level lo
                    ProductMessages.Enqueue(new MessageWithIndex(11, 3, new byte[] { 10, 1, 0 })); // pad off
                    //thirteenth audio test
                    ProductMessages.Enqueue(new MessageWithIndex(12, 3, new byte[] { 12, 0, 1 })); // input 2.2M
                    ProductMessages.Enqueue(new MessageWithIndex(12, 3, new byte[] { 12, 1, 3 })); // gain -10
                    ProductMessages.Enqueue(new MessageWithIndex(12, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(12, 3, new byte[] { 10, 1, 0 })); // pad off
                    //fourteenth audio test
                    ProductMessages.Enqueue(new MessageWithIndex(13, 3, new byte[] { 12, 0, 2 })); // input 47k
                    ProductMessages.Enqueue(new MessageWithIndex(13, 3, new byte[] { 12, 1, 3 })); // gain -10
                    ProductMessages.Enqueue(new MessageWithIndex(13, 3, new byte[] { 10, 0, 0 })); // level hi
                    ProductMessages.Enqueue(new MessageWithIndex(13, 3, new byte[] { 10, 1, 0 })); // pad off
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
