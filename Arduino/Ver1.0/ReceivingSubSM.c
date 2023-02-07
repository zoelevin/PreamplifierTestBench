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

    static ReceivingSubState thisSubState = ReadHead;

	static char thisLength = 0; 						// length of payload in bytes
	static char thisPayload[MAX_PAYLOAD];
	static char payloadCount = 0; 						// number of payload bytes read
	static char thisSum = 0;
	

	switch(thisSubState) {
	case ReadHead:
			
			switch(ThisEvent.Type) {
			case NewByte:
			
				if (ThisEvent.Param1 == HEAD_BYTE){
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
					
				break;
			default:
				break;
			}
		
		break;
	case ReadEnd:
	
			switch(ThisEvent.Type) {
			case NewByte:
			
				if (ThisEvent.Param1 == END_BYTE){
																	// implements CompareSum "state"
					if (thisSum == CalculateChecksum(thisPayload, ARRAY_SIZE(thisPayload))){
																	// implements post "state"
						Event commandEvent;
						Event respondEvent;
			
						commandEvent.Type = thisPayload[0];			// payloads will be structured the same as command events
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
char CalculateChecksum(char str[], char size){ 

    char sum;

	// INSERT ALGORITHM
	
	return sum;

}