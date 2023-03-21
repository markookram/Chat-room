using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantHighFived : ChatEvent
{
    public ParticipantHighFived()
    {
    }

    public ParticipantHighFived(EventType type)
    :base(type){}


    public ParticipantHighFived(int participantId, string participantName, int chatRoomId)
    :base(EventType.PariticipantHighFived, participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public static string StringFormat => "{0} high-fives: {1}";

    public static string AggregateStringFormat => "1 person high-fived {0} other people";
}