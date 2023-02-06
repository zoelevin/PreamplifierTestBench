// Library Inclusions


// Project Inclusions
#include "Configuration.h"
#include "EventBuffer.h"
#include "ReceivingSubSM.h"
#include "TopLevelSM.h"

// Test Harness Defines, uncomment exactly one

#define TOP_LEVEL_TEST
//#define UART_TEST
//#define MAIN

/* 
	GLOBAL VARIABLES
*/

/*
	HELPER FUNCTIONS FOR COMPILING C CODE INSIDE CPP 
*/

// converts Serial.write method to a function usable in C mode
int SerialWriteStr(char str[], char len){

    int val = Serial.write(str, len);
    return val;
    
}


/*
	MAIN FUNCTIONS
*/


// Runs once at the beginning of the program, will implement the INIT top-level state
void setup() {

    // Hardware initializations

    Serial.begin(9600);

    pinMode(2, OUTPUT);                     // Configure all available digital pins as outputs
    pinMode(3, OUTPUT);
    pinMode(4, OUTPUT);
    pinMode(5, OUTPUT);
    pinMode(6, OUTPUT);
    pinMode(7, OUTPUT);
    pinMode(8, OUTPUT);
    pinMode(9, OUTPUT);
    pinMode(10, OUTPUT);
    pinMode(11, OUTPUT);
    pinMode(12, OUTPUT);
    pinMode(13, OUTPUT);

    pinMode (A0, INPUT);                    // Configure all analog pins as inputs
    pinMode (A4, INPUT);
    pinMode (A2, INPUT);
    pinMode (A3, INPUT);
    pinMode (A5, INPUT);

    // State machine initializations

    InitEventBuffer();
    InitTopLevelSM();
    InitReceivingSubSM();
    ResetConfig();
}


#ifdef MAIN
// built in while(1)
void loop() {

    if (!IsEventBufferEmpty()){
	
		Event ThisEvent = RunTopLevelSM(GetEvent());
		if (ThisEvent.Type != noEvent){
			PostEvent(ThisEvent);  // re-post event if it is not consumed in that iteration of SM
		} 
	
	}
	
	// Check for events
	
	if (Serial.available()) {
		Event newEvent;
		newEvent.Type = NewByte;
		newEvent.Param1 = Serial.read();
		
		PostEvent(newEvent);
	}

}
#endif

// Top Level SM and events framework test harness loop()

#ifdef TOP_LEVEL_TEST
void loop(){


}
#endif 

// UART Test harness loop()

#ifdef UART_TEST
void loop(){

    
}

#endif