using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantHighFived : ChatEvent, IChatEventRecipient
{
    public ParticipantHighFived()
    {
    }

    public ParticipantHighFived(int participantId, string participantName, int chatRoomId)
    :base(EventType.ParticipantHighFived, participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public override ParticipantHighFived SetRecipient(int? participantId, string? participantName)
    {
        if (participantId != default && participantName != default && Type != EventType.ParticipantHighFived)
            throw new InvalidOperationException(
                $"Only for {EventType.ParticipantHighFived} is allowed to set the recipient.");

        ToParticipantId = participantId;
        ToParticipantName = participantName;

        return this;
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