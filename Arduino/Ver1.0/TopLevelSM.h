#ifndef TOP_LEVEL_SM_H
#define TOP_LEVEL_SM_H

#include "EventBuffer.h"

#ifdef __cplusplus
 extern "C" {
#endif

/*
    LIBRARY DEFINES, ENUMS, STRUCTS
*/

	#define RESPONSE_STR_LEN 6          // Length of string to be transmitted over UART carrying a confirmation of reception message
	#define AREAD_STR_LEN    7          // Length of string to be transmitted over UART containing voltage readings of power rails

	typedef enum {
		InitTop,
		Receiving,
		Transmitting,
		Executing
	} TopLevelState;

	enum {
		Voltage5 =   5,
		Voltage12 =  12,
		Voltage48 =  48,
		Voltage300 = 300
	} VoltageNum;

	enum {FAILED = 0,
		  PASSED = 1
        };

/*
    FUNCTION PROTOTYPES
*/

// Initializes global variables
void InitTopLevelSM(void);

// Runs the top level state machine
// Param1: ThisEvent
//         The event to be acted upon during this iteration of the state machine
// return: ThisEvent
//         Either modified or unmodified, if not set to noEvent, it will be reposted to the bugger
Event RunTopLevelSM(Event ThisEvent);

#ifdef __cplusplus
}
#endif

#endif