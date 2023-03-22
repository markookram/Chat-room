using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantCommented : ChatEvent
{
    public ParticipantCommented()
    {
    }


    public override string ToEventString()
    {
        return $"{ParticipantName} comments {Message}";
    }

    public override string ToAggregateString(params string[] prms)
    {
        return $"{prms[0]} comments";
    }

    public ParticipantCommented(int participantId, string participantName, int chatRoomId)
    :base(EventType.ParticipantCommented,  participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }
}