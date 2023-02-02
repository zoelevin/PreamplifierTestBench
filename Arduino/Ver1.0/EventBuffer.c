#include "EventBuffer.h"

/*
    GLOBAL VARIABLES
*/

struct EventBuffer {
  Event List[BUFFER_SIZE];

  int Head;
  int Tail;
};

/*
    FUNCTION DEFINITIONS
*/

// Initialize Event Buffer, set head and tail to, set all events to noEvent, all params to 0
void InitEventBuffer(void) {

}

// add an event to the head of EventBuffer
int PostEvent(Event ThisEvent) {
}

// return the oldest event in EventBuffer
Event GetEvent(void) {
}

// Return TRUE is Event Buffer is Full and FALSE if it is not
bool IsEventBufferFull(void) {
}

// Return TRUE is Event Buffer is Empty and FALSE if it is not
bool IsEventBufferEmpty(void) {
}