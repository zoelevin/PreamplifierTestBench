#include "TopLevelSM.h"

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


}