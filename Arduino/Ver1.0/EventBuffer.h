#ifndef EVENT_BUFFER_H
#define EVENT_BUFFER_H

#ifdef __cplusplus
 extern "C" {
#endif

/*
    LIBRARY DEFINES, ENUMS, STRUCTS
*/

#define BUFFER_SIZE 16                          // The number of entries allocated for the event buffer, may need to be changed

typedef enum {FALSE = 0, TRUE = 1} bool;

enum EventType {

  Connected = 0,                                // the command events will also be used to identify message IDs
  Reset,
  Config_Pot,
  Config_Switch_2T,
  Config_Switch_3T,
  Config_Switch_5T,
  Config_Switch_7T,
  Config_Switch_11T,
  Config_Voltage,
  Set_Pot,
  Set_Switch_2T,
  Set_Switch_3T,
  Set_Switch_5T,
  Set_Switch_7T,
  Set_Switch_11T,
  Read_Voltage,
  noEvent,
  NewByte,
  TransmitConfirm,
  TransmitInfo

};

typedef struct {
  char Type;
  char Param1;
  char Param2;
  char Param3;
  char Param4;
  char Param5;

} Event;


/*
    FUNCTION PROTOTYPES
*/

// Initialize Event Buffer, set head and tail to, set all events to noEvent, all params to 0
void InitEventBuffer(void);

// add an event to the head of EventBuffer
int PostEvent(Event ThisEvent);

// return the oldest event in EventBuffer
Event GetEvent(void);

// Return TRUE is Event Buffer is Full and FALSE if it is not
bool IsEventBufferFull(void);

// Return TRUE is Event Buffer is Empty and FALSE if it is not
bool IsEventBufferEmpty(void);

#ifdef __cplusplus
}
#endif

#endif