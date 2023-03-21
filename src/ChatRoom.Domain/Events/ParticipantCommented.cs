using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantCommented : ChatEvent
{
    public ParticipantCommented()
    {
    }

    public static string StringFormat => "{0} comments: {1}";

    public static string AggregateStringFormat => "{0} comments";

    public ParticipantCommented(EventType type)
        :base(type){}

    public ParticipantCommented(int participantId, string participantName, int chatRoomId)
    :base(EventType.ParticipantCommented,  participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }
}