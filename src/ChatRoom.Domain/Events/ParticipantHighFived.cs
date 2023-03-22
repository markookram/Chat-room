using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantHighFived : ChatEvent
{
    public ParticipantHighFived()
    {
    }

    public ParticipantHighFived(int participantId, string participantName, int chatRoomId)
    :base(EventType.ParticipantHighFived, participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public override string ToEventString()
    {
        return $"{ParticipantName} high-fives: {ToParticipantName}";
    }

    public override string ToAggregateString(params string[] prms)
    {
        return $"1 person high-fived {prms[0]} other {prms[1]}";
    }
}