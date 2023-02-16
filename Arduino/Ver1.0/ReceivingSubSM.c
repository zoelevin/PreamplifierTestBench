#include "ReceivingSubSM.h"

/*
    GLOBAL VARIABLES
*/

    static ReceivingSubState thisSubState;

	static char thisLength = 0; 						// length of payload in bytes
	static char thisPayload[MAX_PAYLOAD];
	static char payloadCount = 0; 						// number of payload bytes read
	static char thisSum = 0;

/*
    FUNCTION DEFINITIONS
*/

void InitReceivingSubSM(void) {

    thisSubState= ReadHead;
    thisLength = 0;
    for (int i = 0; i < MAX_PAYLOAD; i++) {
        thisPayload[i] = 0x0;        
    }
    payloadCount = 0;
    thisSum = 0;
    

}

// Runs the Receiving sub state machine
Event RunReceivingSubSM(Event ThisEvent) {

	static char thisLength = 0; 						// length of payload in bytes
	static char thisPayload[MAX_PAYLOAD];
	static char payloadCount = 0; 						// number of payload bytes read
	static char thisSum = 0;
	

	switch(thisSubState) {
	case ReadHead:
			
			switch(ThisEvent.Type) {
			case NewByte:

				if (ThisEvent.Param1 == HEAD_BYTE){

                    //SerialWriteStr("Head Detected\r\n", 15);     //TEST, REMOVE            
                  
					thisSubState = ReadLength; 	
					thisLength = 0;
					for (int i = 0; i < MAX_PAYLOAD; i++){
						thisPayload[i] = 0x0;        
					}
					payloadCount = 0;
					thisSum = 0;
				}
				ThisEvent.Type = noEvent;			// consume event
				break;
			default:
				break;
			}
			
		break;
	case ReadLength:
	
			switch(ThisEvent.Type) {
			case NewByte:
			                                    
                //SerialWriteStr("Length Stored\r\n", 15);     //TEST, REMOVE   

				thisLength = ThisEvent.Param1;			// store length of payload

				for(int i = 0; i < MAX_PAYLOAD; i++) {
					thisPayload[i] = 0;					// clear payload variable
				}
				thisSubState = ReadPayload;
				ThisEvent.Type = noEvent;
				break;
			default:
				break;
			}
	
		break;
	case ReadPayload:
	
			switch(ThisEvent.Type) {
			case NewByte:
				
				thisPayload[payloadCount] = ThisEvent.Param1;
				payloadCount++;
				if (payloadCount >= thisLength) {
					thisSubState = ReadTail;			// transition once thisLength # of bytes read
                    //SerialWriteStr("Payload Stored\r\n", 16);     //TEST, REMOVE
				}
				ThisEvent.Type = noEvent;
				
				break;
			default:
				break;
			}
		
		break;
	case ReadTail:
	
			switch(ThisEvent.Type) {
			case NewByte:
				
				if (ThisEvent.Param1 == TAIL_BYTE){
					thisSubState = ReadSum; 			
                    //SerialWriteStr("Tail Detected\r\n", 15);     //TEST, REMOVE
					
				} else {
					thisSubState = ReadHead;
				}
				ThisEvent.Type = noEvent;
				break;
			default:
				break;
			}
		
		break;
	case ReadSum:
	
			switch(ThisEvent.Type) {
			case NewByte:
			
				thisSum = ThisEvent.Param1;
				thisSubState = ReadEnd;
				ThisEvent.Type = noEvent;
               // SerialWriteStr("Sum Stored\r\n", 12);     //TEST, REMOVE
					
				break;
			default:
				break;
			}
		
		break;
	case ReadEnd:
	
			switch(ThisEvent.Type) {
			case NewByte:
			
				if (ThisEvent.Param1 == END_BYTE){
                    //SerialWriteStr("End Detected\r\n", 14);     //TEST, REMOVE

																	// implements CompareSum "state"
					if (thisSum == CalculateChecksum(thisPayload, ARRAY_SIZE(thisPayload))){
																	// implements post "state"
						Event commandEvent;
						Event respondEvent;
			
						commandEvent.Type   = thisPayload[0];	    // payloads will be structured the same as command events
						commandEvent.Param1 = thisPayload[1];
						commandEvent.Param2 = thisPayload[2];
						commandEvent.Param3 = thisPayload[3];
						commandEvent.Param4 = thisPayload[4];
                        commandEvent.Param5 = thisPayload[5];

			
						respondEvent.Type = TransmitConfirm;
						respondEvent.Param1 = thisPayload[0];
			
						PostEvent(respondEvent);					// Post respond event THEN command event
						PostEvent(commandEvent);
						
			
					} 
				} 
				thisSubState = ReadHead;							// Reset to ReadHead state no matter what
				ThisEvent.Type = noEvent;
				break;
			default:
				break;
			}
		
		break;
	default:
		break;
	}

    
	return ThisEvent;

}

ReceivingSubState GetReceivingSubState(void) {

    return thisSubState;

}

// returns the Fletcher-16 sum of string char[]
char CalculateChecksum(char str[], char len){ 

    char sum = 0; // temporarily set to 1 while the rest is being tested

	for(int i = 0; i < len; i++) {

        sum |= str[i];

    }
    sum = ~sum;
	
    #ifdef MAIN
	return sum;
    #endif
    #ifndef MAIN
    return 1; // for testing purposes, checksum always returns 1;
    #endif

}