using ChatRoom.Application.Abstractions.Events.Enum;

namespace ChatRoom.Application.Abstractions.Queries;

/// <summary>
/// Represents a query params
/// </summary>
public interface IQueryParams
{
    GranularityType GranularityType { get; }

    int RoomId { get; set; }

    int ParticipantId { get; set; }
}