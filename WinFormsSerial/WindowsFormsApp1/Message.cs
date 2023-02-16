using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Message
    {
        public byte Type;
        public byte Param1;
        public byte Param2;
        public Message(byte t, byte p1, byte p2) {

            Type = t;
            Param1 = p1;
            Param2 = p2;

        
        }


    }
}
