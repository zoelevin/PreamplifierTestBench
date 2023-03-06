using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WindowsFormsApp1;

namespace TestBenchApplication
{
    //ENUMS
    public enum ErrorCodes { uCnotVisible = 0, uCnotConnected, uCnotResponding, APnotOpening, APnotResponding, VoltageFail, InvalidProduct }
    public enum ProgramTransitions
    {
        Cancel, TechConfirm, ProductSelectedValid, PacketSent, uCconfirm, uCnoResponse, Start, NewTest, Reconnected,
        Generated, PacketSentVolt, PacketSentNoVolt, VoltageFail, VoltageSuccess, uCconfirmNoMess, uCconfirmMess, DelayDone, APnoResponse, APdoneNoTest, APdoneTest,
        APtimeout, DelayDoneCountLow, DelayDoneCountHigh, APopen, NoConfirmCountLow, NoConfirmCountHigh, uCconfirmAPfail, uCconfirmAPpass, Reboot, BootDone, uCcantFind, uCcantConnect,
    }
    //class for linking all of the state machines together into a single class
    //handles all the transitions for all the state machines
    public class ProgramSM
    {
        //EVENT HANDLERS
        public EventHandler StateChangeEvent;

        //OBJECT DECLARIATIONS
        public BootSM bootSM = new BootSM();                                        //make instance of boot state machine
        public AutomaticSM autoSM = new AutomaticSM();                              //make instance of auto state machine
        public TopLevelStateMachine topSM = new TopLevelStateMachine();             //make instance of top state machine


        //PUBLIC VARIABLES
        public Timer relayDelayTimer = new Timer(1000);                             //timer used for delaying process for relays to switch, currently 3 second delay
        public Timer uCtimeoutTimer = new Timer(1000);                              //gives the micro time to respond, currently 3 second delay
        public Timer uCMessagePollTimer = new Timer(100);                           //gives the micro time to respond, currently 3 second delay
        public int UcattemptCounter, APattemptCounter;
        public bool APnoPassFlag, uCcantConnectFlag, uCcantFindFlag, uCnoRespFlag;


        //PRIVATE VARIABLES AND OBJECTS
        private static ProgramSM _instance = new ProgramSM();                       //creates signle instance of this class for the entire program
        private Message currentInMessage;
        public Message currentOutMessage;                                           //updated in the send message function of Arduino class

        public static ProgramSM Instance
        {
            get
            {
                return _instance;
            }
        }

        //FUNCTIONS AND CONSTRUCTOR
        private ProgramSM()
        {
            //init vars
            APattemptCounter = 0;
            UcattemptCounter = 0;
            uCcantConnectFlag = false;
            uCcantFindFlag = false;
            uCnoRespFlag = false;
            APnoPassFlag = false;
            //init timers
            //timer for polling message que while waiting for confirmation
            uCMessagePollTimer.Elapsed += uCMessagePollTimer_Elapsed;  //adding event handler
            uCMessagePollTimer.Enabled = true;  //enables events
            uCMessagePollTimer.AutoReset = true;  //we want this timer to reset automatically, then stop when the message is recieved
            uCMessagePollTimer.Stop();

            //timer for delaying for relay switching
            relayDelayTimer.Elapsed += RelayDelayTimer_Elapsed;  //adding event handler
            relayDelayTimer.Enabled = true;  //enables events
            relayDelayTimer.AutoReset = false;  //dont want it to restart automatically, only when it eneters the delay state
            relayDelayTimer.Stop();

            //timer for generating uC timeout events
            uCtimeoutTimer.Elapsed += uCtimeoutTimer_Elapsed;
            uCtimeoutTimer.Enabled = true;  //enables event
            uCtimeoutTimer.AutoReset = false;  //dont want it to restart automatically, only when it eneters the delay state
            uCtimeoutTimer.Stop();

        }

        private void uCtimeoutTimer_Elapsed(object sender, ElapsedEventArgs e) //event hadnler for the delay timer expiring, will need to reset this timer if a message does come in, will need to call timer.stop
        {
            uCMessagePollTimer.Stop();
            uCtimeoutTimer.Stop();
            if (ProgramSM.Instance.topSM.CurrentState == TopState.AwaitingConfirmation | ProgramSM.Instance.topSM.CurrentState == TopState.Automatic)
            {
                ProgramSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);  //handle delay done event
            }
            else if (ProgramSM.Instance.topSM.CurrentState == TopState.Boot)
            {
                if (ProgramSM.Instance.UcattemptCounter < 3)
                {
                    ProgramSM.Instance.ChangeStates(ProgramTransitions.NoConfirmCountLow);
                }
                else
                {
                    uCnoRespFlag = true;
                    ProgramSM.Instance.ChangeStates(ProgramTransitions.NoConfirmCountHigh);
                }
            }

        }
        private void RelayDelayTimer_Elapsed(object sender, ElapsedEventArgs e)    //event hadnler for the delay timer expiring
        {
            relayDelayTimer.Stop();
            ProgramSM.Instance.ChangeStates(ProgramTransitions.DelayDone);  //handle delay done event
        }
        private void uCMessagePollTimer_Elapsed(object sender, ElapsedEventArgs e)    //event hadnler for the delay timer expiring
        {
            if (ArduinoComms.Queue.Count == 0)  //no message available
            {
                return;
            }
            else
            {
                uCMessagePollTimer.Stop();
                uCtimeoutTimer.Stop();
                currentInMessage = ArduinoComms.Queue.Dequeue();                                                   //deque from message buffer
                if (currentInMessage.Param1 == currentOutMessage.Type)  //message correct
                {

                    if (topSM.CurrentState == TopState.Boot)                                                       //handline correct Uc response in boot SM
                    {
                        if (APnoPassFlag == false)
                        {
                            ProgramSM.Instance.ChangeStates(ProgramTransitions.uCconfirmAPpass);
                        }
                        else
                        {
                            ProgramSM.Instance.ChangeStates(ProgramTransitions.uCconfirmAPfail);
                        }
                    }

                    else if (topSM.CurrentState == TopState.Automatic)                                              //handles correct message in automatic testing SM
                    {
                        if (autoSM.CurrentAutoState == AutoState.AwaitingConfirmation)
                        {
                           if (autoSM.MessagesRemaining() == true)                                                  
                            {
                                ProgramSM.Instance.ChangeStates(ProgramTransitions.uCconfirmMess);
                            }
                            else
                            {
                                ProgramSM.Instance.ChangeStates(ProgramTransitions.uCconfirmNoMess);
                            }
                        }
                        else if (autoSM.CurrentAutoState == AutoState.AwaitingVoltage)
                        {
                            ProgramSM.Instance.ChangeStates(ProgramTransitions.VoltageSuccess);
                        }
                    }

                    else                                                                                          //handles correct response in top level
                    {
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.uCconfirm);
                    }
                }
                else  //message incorrect
                {

                    if (topSM.CurrentState == TopState.Boot)                                                      //handling uC wrong response in boot SM
                    {
                        if (ProgramSM.Instance.UcattemptCounter < 3)
                        {
                            ProgramSM.Instance.ChangeStates(ProgramTransitions.NoConfirmCountLow);
                        }
                        else
                        {
                            uCnoRespFlag = true;
                            ProgramSM.Instance.ChangeStates(ProgramTransitions.NoConfirmCountHigh);
                        }
                    }

                    else if (topSM.CurrentState == TopState.Automatic)                                            //handles incorrect message in automatic testing SM
                    {
                        if (autoSM.CurrentAutoState == AutoState.AwaitingConfirmation)
                        {
                            ProgramSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);
                        }
                        else if (autoSM.CurrentAutoState == AutoState.AwaitingVoltage)
                        {
                            ProgramSM.Instance.ChangeStates(ProgramTransitions.VoltageFail);
                        }
                    }

                    else                                                                                          //handles wrong response in top level
                    {
                        ProgramSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);
                    }
                }
            }
        }

        public void ChangeStates(ProgramTransitions transition)
        {
            topSM.ChangeStates(transition);
            if (topSM.CurrentState == TopState.Automatic)  //only change auto states if top level state is automatic
            {
                autoSM.ChangeStates(transition);
            }
            else if (topSM.CurrentState == TopState.Boot)  // only change boot states if current top state is boot
            {
                bootSM.ChangeStates(transition);  // if boot is done change top level state with boot transition
            }
            else if (topSM.CurrentState == TopState.ProductConfirmed)
            {
                if (transition == ProgramTransitions.Start)
                {
                    autoSM.ChangeStates(transition);
                }
            }else if (topSM.CurrentState == TopState.Results)
            {
                if (transition == ProgramTransitions.APdoneNoTest)
                {
                    autoSM.ChangeStates(ProgramTransitions.APdoneNoTest);
                }
            }
            else if ((transition == ProgramTransitions.Cancel) & (topSM.CurrentState != TopState.Boot))
            {
                autoSM.ChangeStates(transition);
                bootSM.ChangeStates(transition);
            }
            StateChangeEvent.Invoke(this, EventArgs.Empty);  //this is just sent to test GUI form to see current state
        }
    }
}
