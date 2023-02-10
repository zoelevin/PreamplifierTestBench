using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    internal class ProgramSM
    {
        BootSM bootSM = new BootSM();  //make instance of boot state machine
        AutomaticSM autoSM = new AutomaticSM(); //make instance of auto state machine
        TopLevelStateMachine topSM = new TopLevelStateMachine(); //make instance of top state machine
    }
}
