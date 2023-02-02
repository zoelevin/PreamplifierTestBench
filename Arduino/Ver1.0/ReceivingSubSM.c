#include "ReceivingSubSM.h"

/*
    GLOBAL VARIABLES
*/

    static ReceivingSubState thisSubState = ReadHead;

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


}

// returns the Fletcher-16 sum of string char[]
char CalculateChecksum(char str[]){ 


}