
#include <Arduino.h>

#include "TopLevelSM.h"
#include "Configuration.h"


/*
    GLOBAL VARIABLES
*/

	static TopLevelState thisState;

/*
    FUNCTION DEFINITIONS
*/

void InitTopLevelSM(void) {

    thisState = InitTop;

}

Event RunTopLevelSM(Event ThisEvent) {

    // Local Variable Declarations
    char responsePacket[RESPONSE_STR_LEN];  // belongs to Transmitting > TransmitConfirm
    char infoPacket[AREAD_STR_LEN];         // belongs to Transmitting > TransmitInfo
    char thisPacket[2];                     // belongs to Transmitting > TransmitInfo
    Event newEvent;
    int thisVoltage = 0;                    // belongs to Executing > Read_Voltage

    switch(thisState) {
	case InitTop:
		thisState = Receiving;
		break;
	case Receiving:
			ThisEvent = RunReceivingSubSM(ThisEvent);
			if (ThisEvent.Type == TransmitConfirm) {
				thisState = Transmitting;
				ThisEvent = RunTopLevelSM(ThisEvent);			// transition when transmit event is found
			}
		break;
	case Transmitting:
	
		switch (ThisEvent.Type) {
		case TransmitConfirm:
    
            // Define string char by char because C is annoying
            responsePacket[0] = HEAD_BYTE;
            responsePacket[1] = 1;
            responsePacket[2] = ThisEvent.Param1;
            responsePacket[3] = TAIL_BYTE;
            responsePacket[4] = CalculateChecksum(ThisEvent.Param1, 1);
            responsePacket[5] = END_BYTE;
			
			// send message and verify it sent before transition
			if (SerialWriteStr(responsePacket, RESPONSE_STR_LEN) == RESPONSE_STR_LEN) {
				thisState = Executing;	
				ThisEvent.Type = noEvent;
			} else {
				// INSERT ERROR HANDLING
			}
	    break;
		case TransmitInfo:

            thisPacket[0] = ThisEvent.Param1;
            thisPacket[1] = ThisEvent.Param2;
			infoPacket[0] = HEAD_BYTE;
            infoPacket[1] = 2;
            infoPacket[2] = ThisEvent.Param1;
            infoPacket[3] = ThisEvent.Param2;
            infoPacket[4] = TAIL_BYTE;
            infoPacket[5] = CalculateChecksum(thisPacket, ARRAY_SIZE(thisPacket));
            infoPacket[6] = END_BYTE;
			
			// send message and verify it sent before transition
			if (SerialWriteStr(infoPacket, AREAD_STR_LEN) == AREAD_STR_LEN) {
				thisState = Executing;		
				ThisEvent.Type = noEvent;
			} else {
				// INSERT ERROR HANLDING
			}
			
		break;
			default:
		break;	
		}
	
		break;
	case Executing:
	
		switch(ThisEvent.Type) {
		case Connected:
			break;
            ThisEvent.Type = noEvent;
		case Reset:
			ResetConfig();
            thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Config_Pot:
			PotList[ThisEvent.Param1].Pin1 = ThisEvent.Param2;
			PotList[ThisEvent.Param1].Pin2 = ThisEvent.Param3;
			PotList[ThisEvent.Param1].Pin3 = ThisEvent.Param4;
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Config_Switch_2T:
			SW2TList[ThisEvent.Param1].Pin1 = ThisEvent.Param2;
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Config_Switch_3T:
			SW3TList[ThisEvent.Param1].Pin1 = ThisEvent.Param2;
			SW3TList[ThisEvent.Param1].Pin2 = ThisEvent.Param3;
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Config_Switch_5T:
			SW5TList[ThisEvent.Param1].Pin1 = ThisEvent.Param2;
			SW5TList[ThisEvent.Param1].Pin2 = ThisEvent.Param3;
			SW5TList[ThisEvent.Param1].Pin3 = ThisEvent.Param4;
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Config_Switch_7T:
			SW7TList[ThisEvent.Param1].Pin1 = ThisEvent.Param2;
			SW7TList[ThisEvent.Param1].Pin2 = ThisEvent.Param3;
			SW7TList[ThisEvent.Param1].Pin3 = ThisEvent.Param4;
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Config_Switch_11T:
			SW11TList[ThisEvent.Param1].Pin1 = ThisEvent.Param2;
			SW11TList[ThisEvent.Param1].Pin2 = ThisEvent.Param3;
			SW11TList[ThisEvent.Param1].Pin3 = ThisEvent.Param4;
			SW11TList[ThisEvent.Param1].Pin4 = ThisEvent.Param5;
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Config_Voltage:
			switch(ThisEvent.Param1) {
			case Voltage5:
				Voltage5Pin = ThisEvent.Param2;
				break;
			case Voltage12:
				Voltage12Pin = ThisEvent.Param2;
				break;
			case Voltage48:
				Voltage48Pin = ThisEvent.Param2;
				break;
			case Voltage300:
				Voltage300Pin = ThisEvent.Param2;
				break;
			}
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Set_Pot:
			digitalWrite(PotList[ThisEvent.Param1].Pin1, ThisEvent.Param2 & FIRST_BIT);
			digitalWrite(PotList[ThisEvent.Param1].Pin2, ThisEvent.Param2 & SECOND_BIT);
			digitalWrite(PotList[ThisEvent.Param1].Pin3, ThisEvent.Param2 & THIRD_BIT);
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Set_Switch_2T:
			digitalWrite(SW2TList[ThisEvent.Param1].Pin1, ThisEvent.Param2 & FIRST_BIT);
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Set_Switch_3T:
			digitalWrite(SW3TList[ThisEvent.Param1].Pin1, ThisEvent.Param2 & FIRST_BIT);
			digitalWrite(SW3TList[ThisEvent.Param1].Pin2, ThisEvent.Param2 & SECOND_BIT);
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Set_Switch_5T:
			digitalWrite(SW5TList[ThisEvent.Param1].Pin1, ThisEvent.Param2 & FIRST_BIT);
			digitalWrite(SW5TList[ThisEvent.Param1].Pin2, ThisEvent.Param2 & SECOND_BIT);
			digitalWrite(SW5TList[ThisEvent.Param1].Pin3, ThisEvent.Param2 & THIRD_BIT);
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Set_Switch_7T:
			digitalWrite(SW7TList[ThisEvent.Param1].Pin1, ThisEvent.Param2 & FIRST_BIT);
			digitalWrite(SW7TList[ThisEvent.Param1].Pin2, ThisEvent.Param2 & SECOND_BIT);
			digitalWrite(SW7TList[ThisEvent.Param1].Pin3, ThisEvent.Param2 & THIRD_BIT);
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
		case Set_Switch_11T:
			digitalWrite(SW11TList[ThisEvent.Param1].Pin1, ThisEvent.Param2 & FIRST_BIT);
			digitalWrite(SW11TList[ThisEvent.Param1].Pin2, ThisEvent.Param2 & SECOND_BIT);
			digitalWrite(SW11TList[ThisEvent.Param1].Pin3, ThisEvent.Param2 & THIRD_BIT);
			digitalWrite(SW11TList[ThisEvent.Param1].Pin3, ThisEvent.Param2 & FOURTH_BIT);
			thisState = Receiving;
            ThisEvent.Type = noEvent;
			break;
			
		case Read_Voltage:
			
			newEvent.Type = TransmitInfo;
			switch(ThisEvent.Param1) {
			case Voltage5:
				thisVoltage = analogRead(Voltage5Pin);
				break;
			case Voltage12:
				thisVoltage = analogRead(Voltage12Pin);
				break;
			case Voltage48:
				thisVoltage = analogRead(Voltage48Pin);
				break;
			case Voltage300:
				thisVoltage = analogRead(Voltage300Pin);
				break;
			}
			if (thisVoltage > ThisEvent.Param2) {
				newEvent.Param1 = PASSED;
			} else {
				newEvent.Param1 = FAILED;
			}
			PostEvent(newEvent);
            ThisEvent.Type = noEvent;
			thisState = Transmitting;
		break;
		default:
			break;
		}
	
		break;
	deafault:
		break;
	}
	
	return ThisEvent;

}

TopLevelState GetTopLevelState(void) {

    return thisState;

}