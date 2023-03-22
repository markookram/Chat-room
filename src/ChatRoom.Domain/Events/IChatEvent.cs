using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public interface IChatEvent : IEntity, IAuditableEntity
{
    EventType Type { get; }

    int ParticipantId { get; }

    string ParticipantName { get; }

    int ChatRoomId { get; }

    string ToAggregateStringFormat(params string[] prms);
}