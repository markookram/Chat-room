#pragma warning disable CS8618
using ChatRoom.Domain.Events.Enum;
using static System.Int32;

namespace ChatRoom.Domain.Events;

public abstract class ChatEvent : Entity,
    IChatEvent,
    IChatEventMessage,
    IChatEventRecipient

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

    public virtual string? Message { get; protected set; }

    public virtual int? ToParticipantId { get; protected set; }

    public virtual string? ToParticipantName { get; protected set; }

    public override ChatEvent AddIdentity(int id)
    {
        Id = id;
        return this;
    }

    public virtual ChatEvent AddMessage(string? message)
    {
        return this;
    }

    public virtual ChatEvent SetRecipient(int? participantId, string? participantName)
    {
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

        return ToAggregateString(count.ToString(), count == 1 ? "person" : "people");
    }
}