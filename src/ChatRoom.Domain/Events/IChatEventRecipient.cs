namespace ChatRoom.Domain.Events;

public interface IChatEventRecipient
{
    public int? ToParticipantId { get; }

    public string? ToParticipantName { get;}

    ChatEvent SetRecipient(int? participantId, string? participantName);
}