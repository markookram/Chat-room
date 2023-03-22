using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantLeft : ChatEvent
{
    public ParticipantLeft()
    { }

    public ParticipantLeft(int participantId, string participantName, int chatRoomId)
        :base(EventType.ParticipantLeft, participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public override string ToEventString()
    {
        return $"{ParticipantName} leaves";
    }

    public override string ToAggregateString(params string[] prms)
    {
        return $"{prms[0]} left";
    }
}