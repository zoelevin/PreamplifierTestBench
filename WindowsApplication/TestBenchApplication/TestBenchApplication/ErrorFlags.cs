using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public class ErrorFlags
    {
        //PUBLIC OBJECTS AND VARS//
        public bool APnoPassFlag, UcCantConnectFlag, UcCantFindFlag, UcNoRespFlag;  //used to display errors


        //PRIVATE OBJECTS AND VARS
        private static ErrorFlags _instance= new ErrorFlags();


        public static ErrorFlags Instance
        {
            get
            {
                return _instance;
            }
        }

        //PRIVATE METHODS AND CONSTRUCTOR
        private ErrorFlags()  //constructor initializes everything to false
        {
            APnoPassFlag= false; //these flags will be chacked to update the error form
            UcCantConnectFlag= false;
            UcCantFindFlag= false;
            UcNoRespFlag= false;
        }
    }
}
