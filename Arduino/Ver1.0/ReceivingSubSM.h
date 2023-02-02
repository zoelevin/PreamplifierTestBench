#ifndef RECEIVING_SUB_SM_H
#define RECEIVING_SUB_SM_H

#ifdef __cplusplus
 extern "C" {
#endif

#include "EventBuffer.h"

/*
    LIBRARY DEFINES, ENUMS, STRUCTS
*/

#define MAX_PAYLOAD 16

#define HEAD_BYTE   0b10111111
#define TAIL_BYTE   0b11011111
#define END_BYTE    0b10001111


typedef enum {
		ReadHead,
		ReadLength,
		ReadPayload,
		ReadTail,
		ReadSum,
		ReadEnd,
		CompareSum,
		Post
	} ReceivingSubState;

/*
    FUNCTION PROTOTYPES
*/

// Initializes global variables
void InitReceivingSubSM(void);

// Runs the Receiving sub state machine
Event RunReceivingSubSM(Event ThisEvent);

// returns the Fletcher-16 sum of string char[]
char CalculateChecksum(char[]);

#ifdef __cplusplus
}
#endif

#endif