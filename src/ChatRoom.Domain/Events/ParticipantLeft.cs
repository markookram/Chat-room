using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantLeft : ChatEvent
{
    public ParticipantLeft()
    { }

    public ParticipantLeft(EventType type)
        :base(type){}

    public ParticipantLeft(int participantId, string participantName, int chatRoomId)
        :base(EventType.ParticipantLeft, participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public static string StringFormat => "{0} leaves";

    public static string AggregateStringFormat => "{0} left";
}