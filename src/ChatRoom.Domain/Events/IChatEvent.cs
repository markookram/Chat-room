using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public interface IChatEvent : IEntity, IAuditableEntity
{
    EventType Type { get; }

    int ParticipantId { get; }

    string ParticipantName { get; }

    int ChatRoomId { get; }

    string Describe(params string[] prms);
}