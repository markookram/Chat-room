namespace ChatRoom.Domain.Events.Enum;

public enum EventType
{
    None = 0,

    ParticipantEntered = 1,

    ParticipantLeft,

    ParticipantCommented,

    ParticipantHighFived
}