// Library Inclusions


// Project Inclusions
#include "Configuration.h"
#include "EventBuffer.h"
#include "ReceivingSubSM.h"
#include "TopLevelSM.h"

/* 
	GLOBAL VARIABLES
*/

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
    ResetConfig();
    InitTopLevelSM();
    InitReceivingSubSM();
}

// built in while(1)
void loop() {



}
