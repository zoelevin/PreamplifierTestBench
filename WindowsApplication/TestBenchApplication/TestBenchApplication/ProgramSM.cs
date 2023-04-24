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
    public enum ProgramTransitions   //all the transitions that can happen that affect the three state machines
    {
        Cancel, TechConfirm, ProductSelectedValid, PacketSent, uCconfirm, uCnoResponse, Start, NewTest, Reconnected,
        Generated, PacketSentVolt, PacketSentNoVolt, VoltageFail, VoltageSuccess, uCconfirmNoMess, uCconfirmMess, DelayDone, APnoResponse, APdoneNoTest, APdoneTest,
        APtimeout, DelayDoneCountLow, DelayDoneCountHigh, APopen, NoConfirmCountLow, NoConfirmCountHigh, uCconfirmAPfail, uCconfirmAPpass, Reboot, BootDone, uCcantFind, uCcantConnect,
    }
    //class for linking all of the state machines together into a single class
    //handles all the transitions for all the state machines
    public class programSM
    {
        //PRIVATE VARIABLES AND OBJECTS
        private static programSM _instance = new programSM();      //creates signle instance of this class for the entire program
        private Message currentInMessage;        //used to compare out message vs in message to determine confirmation
        public Message currentOutMessage;        //updated in the send message function of Arduino class


        //PUBLIC OJECTS AND VARS
        public EventHandler StateChangeEvent;  //will be used for updating GUI
        public BootSM BootSM = new BootSM();               //make instance of boot state machine
        public AutomaticSM AutoSM = new AutomaticSM();            //make instance of auto state machine
        public TopLevelSM TopSM = new TopLevelSM();  //make instance of top state machine
        public Timer RelayDelayTimer = new Timer(1000);          //timer used for delaying process for relays to switch, currently 3 second delay
        public Timer UcTimeoutTimer = new Timer(1000);           //gives the micro time to respond, currently 3 second delay
        public Timer UcMessagePollTimer = new Timer(100);        //gives the micro time to respond, currently 3 second delay
        public int UcattemptCounter, APattemptCounter;


   
        //CONSTRUCTOR
        public static programSM Instance
        {
            get
            {
                return _instance;
            }
        }
        private programSM()
        {
            //init vars
            APattemptCounter = 0;
            UcattemptCounter = 0;
            //init timers
            //timer for polling message que while waiting for confirmation
            UcMessagePollTimer.Elapsed += uCMessagePollTimer_Elapsed;  //adding event handler
            UcMessagePollTimer.Enabled = true;  //enables events
            UcMessagePollTimer.AutoReset = true;  //we want this timer to reset automatically, then stop when the message is recieved
            UcMessagePollTimer.Stop();

            //timer for delaying for relay switching
            RelayDelayTimer.Elapsed += relayDelayTimer_Elapsed;  //adding event handler
            RelayDelayTimer.Enabled = true;  //enables events
            RelayDelayTimer.AutoReset = false;  //dont want it to restart automatically, only when it eneters the delay state
            RelayDelayTimer.Stop();

            //timer for generating uC timeout events
            UcTimeoutTimer.Elapsed += uCtimeoutTimer_Elapsed;
            UcTimeoutTimer.Enabled = true;  //enables event
            UcTimeoutTimer.AutoReset = false;  //dont want it to restart automatically, only when it eneters the delay state
            UcTimeoutTimer.Stop();

        }


        //PRIVATE METHODS
        private void uCtimeoutTimer_Elapsed(object sender, ElapsedEventArgs e) //event handler for the delay timer expiring, will need to reset this timer if a message does come in, will need to call timer.stop
        {
            UcMessagePollTimer.Stop();
            UcTimeoutTimer.Stop();
            if (programSM.Instance.TopSM.CurrentState == TopState.AwaitingConfirmation | programSM.Instance.TopSM.CurrentState == TopState.Automatic)    //same transition in auto vs top
            {
                programSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);  //handle delay done event
            }
            else if (programSM.Instance.TopSM.CurrentState == TopState.Boot)    //differnet because boot state machine tries to contact multiple times
            {
                if (programSM.Instance.UcattemptCounter < 3)      
                {
                    programSM.Instance.ChangeStates(ProgramTransitions.NoConfirmCountLow);
                }
                else
                {
                    ErrorFlags.Instance.UcNoRespFlag = true;
                    programSM.Instance.ChangeStates(ProgramTransitions.NoConfirmCountHigh);
                }
            }

        }
        private void relayDelayTimer_Elapsed(object sender, ElapsedEventArgs e)    //event hadnler for the delay timer expiring
        {
            RelayDelayTimer.Stop();
            programSM.Instance.ChangeStates(ProgramTransitions.DelayDone);  //handle delay done event
        }
        private void uCMessagePollTimer_Elapsed(object sender, ElapsedEventArgs e)    //poll message queue and see if message arrived
        {
            if (ArduinoComms.Queue.Count == 0)  //no message available
            {
                return;
            }
            else
            {
                UcMessagePollTimer.Stop(); //something recieved reset timer
                UcTimeoutTimer.Stop();
                currentInMessage = ArduinoComms.Queue.Dequeue();        //deque from message buffer
                if (currentInMessage.Param1 == currentOutMessage.Type || AutoSM.CurrentAutoState == AutoState.AwaitingVoltage)  //message correct
                {

                    if (TopSM.CurrentState == TopState.Boot)   //handline correct Uc response in boot SM
                    {
                        if (ErrorFlags.Instance.APnoPassFlag == false)
                        {
                            programSM.Instance.ChangeStates(ProgramTransitions.uCconfirmAPpass);
                        }
                        else
                        {
                            programSM.Instance.ChangeStates(ProgramTransitions.uCconfirmAPfail);
                        }
                    }

                    else if (TopSM.CurrentState == TopState.Automatic)    //handles correct message in automatic testing SM
                    {
                        if (AutoSM.CurrentAutoState == AutoState.AwaitingConfirmation)  //two states that await uC responsed in auto
                        {
                           if (AutoSM.MessagesRemaining() == true)  //needs to transition differently if more messages need to be sent before full test is setup                  
                            {
                                programSM.Instance.ChangeStates(ProgramTransitions.uCconfirmMess);
                            }
                            else
                            {
                                programSM.Instance.ChangeStates(ProgramTransitions.uCconfirmNoMess);
                            }
                        }
                        else if (AutoSM.CurrentAutoState == AutoState.AwaitingVoltage)
                        {
                            if (currentInMessage.Type == 19)   //Voltage ID check
                            {
                                if (currentInMessage.Param2 == 1)   //Pass =1 fail =0
                                {
                                    programSM.Instance.ChangeStates(ProgramTransitions.VoltageSuccess);
                                }
                                else
                                {
                                    programSM.Instance.ChangeStates(ProgramTransitions.VoltageFail);   
                                }
                                }
                                else
                                {
                                UcMessagePollTimer.Start(); //something recieved reset timer
                                UcTimeoutTimer.Start();
                                return; 
                            }
                            
                        }
                    }
                    else     //handles correct response in top level
                    {
                        programSM.Instance.ChangeStates(ProgramTransitions.uCconfirm);
                    }
                }
                else  //message incorrect
                {
                    if (TopSM.CurrentState == TopState.Boot)     //handling uC wrong response in boot SM
                    {
                        if (programSM.Instance.UcattemptCounter < 3)
                        {
                            programSM.Instance.ChangeStates(ProgramTransitions.NoConfirmCountLow);
                        }
                        else
                        {
                            ErrorFlags.Instance.UcNoRespFlag = true;
                            programSM.Instance.ChangeStates(ProgramTransitions.NoConfirmCountHigh);
                        }
                    }
                    else if (TopSM.CurrentState == TopState.Automatic)   //handles incorrect message in automatic testing SM
                    {
                        if (AutoSM.CurrentAutoState == AutoState.AwaitingConfirmation)
                        {
                            programSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);
                        }
                        else if (AutoSM.CurrentAutoState == AutoState.AwaitingVoltage)
                        {
                            programSM.Instance.ChangeStates(ProgramTransitions.VoltageFail);
                        }
                    }
                    else //handles wrong response in top level
                    {
                        programSM.Instance.ChangeStates(ProgramTransitions.uCnoResponse);
                    }
                }
            }
        }

        //PUBLIC METHODS
        public void ChangeStates(ProgramTransitions transition) //mainly just handles edge cases where state machines are linked
        {
            TopSM.ChangeStates(transition);
           // StateChangeEvent.Invoke(this, EventArgs.Empty);  //this is just sent to test GUI form to see current state for debug
            if (TopSM.CurrentState == TopState.Automatic)  //only change auto states if top level state is automatic
            {
                AutoSM.ChangeStates(transition);
               // StateChangeEvent.Invoke(this, EventArgs.Empty);  //this is just sent to test GUI form to see current state for debuf
            }
            else if (TopSM.CurrentState == TopState.Boot)  // only change boot states if current top state is boot
            {
                BootSM.ChangeStates(transition);  // if boot is done change top level state with boot transition
              //  StateChangeEvent.Invoke(this, EventArgs.Empty);  //this is just sent to test GUI form to see current state for debug
            }
            else if (TopSM.CurrentState == TopState.D_BenchChecks)
            {
                if (transition == ProgramTransitions.BootDone);
                {
                    BootSM.ChangeStates(transition);
                  //  StateChangeEvent.Invoke(this, EventArgs.Empty);  //this is just sent to test GUI form to see current state for debug
                }
            }
            else if (TopSM.CurrentState == TopState.Reconnection)
            {
                if (transition == ProgramTransitions.uCnoResponse)
                {
                    AutoSM.ChangeStates(transition);
                   // StateChangeEvent.Invoke(this, EventArgs.Empty);  //this is just sent to test GUI form to see current state for debug
                }
            }
            else if (TopSM.CurrentState == TopState.VoltageErrors)
            {
                if (transition == ProgramTransitions.VoltageFail)
                {
                    AutoSM.ChangeStates(transition);
                   // StateChangeEvent.Invoke(this, EventArgs.Empty);  //this is just sent to test GUI form to see current state for debug
                }
            }
            else if (TopSM.CurrentState == TopState.ProductConfirmed)
            {
                if (transition == ProgramTransitions.Start)
                {
                    AutoSM.ChangeStates(transition);
                   // StateChangeEvent.Invoke(this, EventArgs.Empty);  //this is just sent to test GUI form to see current state for debug
                }
            }else if (TopSM.CurrentState == TopState.Results)
            {
                if (transition == ProgramTransitions.APdoneNoTest)
                {
                    AutoSM.ChangeStates(transition);
                  // StateChangeEvent.Invoke(this, EventArgs.Empty);  //this is just sent to test GUI form to see current state for debug
                }
            }
            else if ((transition == ProgramTransitions.Cancel) & (TopSM.CurrentState != TopState.Boot))
            {
                AutoSM.ChangeStates(transition);
                BootSM.ChangeStates(transition);
                //StateChangeEvent.Invoke(this, EventArgs.Empty);  //this is just sent to test GUI form to see current state for debug
            }
        }
    }
}
