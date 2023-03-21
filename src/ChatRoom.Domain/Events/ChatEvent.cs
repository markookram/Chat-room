using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ChatEvent : Entity, IChatEvent
{
#pragma warning disable CS8618
    public ChatEvent()
#pragma warning restore CS8618
    {
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
            EventType.ParticipantEntered => $"{ParticipantName} enters the room.",
            EventType.ParticipantLeft => $"{ParticipantName} leaves.",
            EventType.ParticipantCommented => $"{ParticipantName} comments: {Message}",
            EventType.PariticipantHighFived => $"{ParticipantName} high-fives: {ToParticipantName}",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public virtual string Describe(params string[] prms)
    {
        switch (Type)
        {
            case EventType.ParticipantCommented:
            {
                if (!prms.Any() || !int.TryParse(prms[0], out int count)) return "comments";
                return $"{count} comments";
            }
            case EventType.ParticipantEntered:
            {
                if (!prms.Any() || !int.TryParse(prms[0], out int count)) return "person entered";
                var subj = count == 1 ? "person" : "people";
                return $"{count} {subj} entered";
            }
            case EventType.ParticipantLeft:
            {
                if (!prms.Any() || !int.TryParse(prms[0], out int count)) return "left";
                return $"{count} left";
            }
            case EventType.PariticipantHighFived:
            {
                if (!prms.Any() || !int.TryParse(prms[0], out int count)) return "high-fived other people";
                return $"{count} high-fived other people";
            }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}