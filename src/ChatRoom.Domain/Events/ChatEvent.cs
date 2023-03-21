using ChatRoom.Domain.Events.Enum;
#pragma warning disable CS8618

namespace ChatRoom.Domain.Events;

public class ChatEvent : Entity, IChatEvent
{

    public ChatEvent()
    {
    }

    public ChatEvent(EventType type)
    {
        Type = type;
    }

    public ChatEvent(EventType type, int participantId, string participantName, int chatRoomId)
    {
        Type = type;
        ParticipantId = participantId;
        ParticipantName = participantName;
        ChatRoomId = chatRoomId;
    }

    public EventType Type { get; private set; }

    public int ParticipantId { get; private set; }

    public string ParticipantName { get; private set; }

    public int ChatRoomId { get; private set; }

    public string? Message { get; private set; }

    public int? ToParticipantId { get; private set; }

    public string? ToParticipantName { get; private set; }

    public ChatEvent AddMessage(string? message)
    {
        if (!string.IsNullOrEmpty(message) && Type != EventType.ParticipantCommented)
            throw new InvalidOperationException(
                $"Only for {EventType.ParticipantCommented} is allowed to add the comment.");

        Message = message;

        return this;
    }

    public ChatEvent SetRecipient(int? participantId, string? participantName)
    {
        if (participantId != default && participantName != default && Type != EventType.PariticipantHighFived)
            throw new InvalidOperationException(
                $"Only for {EventType.PariticipantHighFived} is allowed to set the recipient.");

        ToParticipantId = participantId;
        ToParticipantName = participantName;

        return this;
    }

    public override ChatEvent AddIdentity(int id)
    {
        Id = id;
        return this;
    }

    public override string ToString()
    {
        return Type switch
        {
            EventType.ParticipantEntered => string.Format(ParticipantEntered.StringFormat, ParticipantName),
            EventType.ParticipantLeft => string.Format(ParticipantLeft.StringFormat, ParticipantName),
            EventType.ParticipantCommented => string.Format(ParticipantCommented.StringFormat, ParticipantName, Message),
            EventType.PariticipantHighFived => string.Format(ParticipantHighFived.StringFormat, ParticipantName, ToParticipantName),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public virtual string AggregateString(params string[] prms)
    {
        switch (Type)
        {
            case EventType.ParticipantEntered:
            {
                int.TryParse(prms[0], out int count);
                var subj = count == 1 ? "person" : "people";

                return string.Format(ParticipantEntered.AggregateStringFormat, prms[0], subj);
            }
            case EventType.ParticipantLeft:
            {
                return string.Format(ParticipantLeft.AggregateStringFormat, prms[0]);
            }
            case EventType.ParticipantCommented:
            {
                return string.Format(ParticipantCommented.AggregateStringFormat, prms[0]);
            }
            case EventType.PariticipantHighFived:
            {
                    return string.Format(ParticipantHighFived.AggregateStringFormat, prms[0]);
            }

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}