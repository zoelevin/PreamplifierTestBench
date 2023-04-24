using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public class ErrorFlags
    {
        public bool APnoPassFlag, UcCantConnectFlag, UcCantFindFlag, UcNoRespFlag, Volt300Fail, Volt48Fail, Volt12Fail;  //used to display errors
        private static ErrorFlags _instance= new ErrorFlags();
        public static ErrorFlags Instance
        {
            get
            {
                return _instance;
            }
        }
        private ErrorFlags()
        {
            APnoPassFlag= false;
            UcCantConnectFlag= false;
            UcCantFindFlag= false;
            UcNoRespFlag= false;
            Volt300Fail= false;
            Volt48Fail= false;
            Volt12Fail= false;
        }
    }
}
