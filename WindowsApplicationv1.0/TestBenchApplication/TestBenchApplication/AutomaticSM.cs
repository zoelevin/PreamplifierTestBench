using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBenchApplication
{
    public class AutomaticSM
    {
        public enum AutoState { Generating = 1, Transmitting, AwaitingVoltage, AwaitingConfirmation, Delay, Testing, }

        public enum AutoTransitions { Generated = 13, PacketSent, VoltageCheck, VoltageFail, uCnoResponse, }
        private AutoState autoState = AutoState.Generating;
        public AutoState CurrentAutoState { get { return autoState; } }  //returns current state
        public void ChangeStates(AutoTransitions transition)
        {  //handles state transitions, ran when event happens
            switch (transition)
            {
                case (AutoTransitions.Generated):
                    if (autoState == AutoState.Generating)
                    {
                        autoState = AutoState.Transmitting;
                    }
                    break;



            }
        }
    }
}
