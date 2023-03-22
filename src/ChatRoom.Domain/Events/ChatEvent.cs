#pragma warning disable CS8618
using ChatRoom.Domain.Events.Enum;
using ChatRoom.Domain.Extensions;
using static System.Int32;

namespace ChatRoom.Domain.Events;

public abstract class ChatEvent : Entity, IChatEvent
{

    protected ChatEvent()
    {
    }

    protected ChatEvent(EventType type, int participantId, string participantName, int chatRoomId)
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
        if (participantId != default && participantName != default && Type != EventType.ParticipantHighFived)
            throw new InvalidOperationException(
                $"Only for {EventType.ParticipantHighFived} is allowed to set the recipient.");

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
        return ToEventString();
    }

    public abstract string ToEventString();

    public abstract string ToAggregateString(params string[] prms);

    public string ToAggregateStringFormat(params string[] prms)
    {
        TryParse(prms[0], out int count);

        return ToAggregateString(prms[0], count == 1 ? "person" : "people");
    }
}