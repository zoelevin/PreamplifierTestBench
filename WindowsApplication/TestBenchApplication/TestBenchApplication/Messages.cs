using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public class Messages
    {
        Queue<OutMessage> SixTenBmessages = new Queue<OutMessage>();  //will hardcode these messages
        public struct OutMessage    //used for putting messages into send message function
        {
            public int ListIndex;  //need index as different messages sent for each test
            public byte Type;
            public byte length;
            public byte[] Payload;
            public OutMessage(int index,byte t, byte len, byte[] pLoad)
            {
                ListIndex = index;
                Type = t;
                length = len;
                Payload = pLoad;
            }
        }
    }
}
