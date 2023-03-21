using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantEntered : ChatEvent
{
    public ParticipantEntered()
    {
    }

    public ParticipantEntered(EventType type)
        :base(type){}

    public ParticipantEntered(int participantId, string participantName, int chatRoomId)
    :base(EventType.ParticipantEntered, participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public static string StringFormat => "{0} enters the room";

    public static string AggregateStringFormat=> "{0} {1} entered";
}