using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public class Messages
    {
        public Queue<MessageWithIndex> SixTenBmessages = new Queue<MessageWithIndex>();                  //will hardcode these messages
        
        public Messages()
        {
            byte[] tempPayload = { 0b00000001}; 
            // adding to 610B message queue
            SixTenBmessages.Enqueue(new MessageWithIndex(1,1,tempPayload));
            SixTenBmessages.Enqueue(new MessageWithIndex(1, 1, tempPayload));
            SixTenBmessages.Enqueue(new MessageWithIndex(1, 1, tempPayload));
            SixTenBmessages.Enqueue(new MessageWithIndex(2, 1, tempPayload));
            SixTenBmessages.Enqueue(new MessageWithIndex(2, 1, tempPayload));
            SixTenBmessages.Enqueue(new MessageWithIndex(2, 1, tempPayload));
        }
        public 
        public struct MessageWithIndex                                                           //used for putting messages into send message function
        {
            public int ListIndex;                                                            //need index as different messages sent for each test. ex messages with index 1 are for the first test, messages index 0 are config
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
