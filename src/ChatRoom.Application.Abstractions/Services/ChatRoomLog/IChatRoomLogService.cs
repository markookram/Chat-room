using ChatRoom.Application.Abstractions.Queries;
using ChatRoom.Domain.Events;

namespace ChatRoom.Application.Abstractions.Services.ChatRoomLog;

/// <summary>
/// Provides basic APIs for events sourcing
/// </summary>
public interface IChatRoomLogService
{
    Task LogEventAsync(ChatEvent @event, CancellationToken cancellationToken = default);

    Task<StringQueryResult> ReadLogAsync(IQueryParams prms, CancellationToken cancellationToken = default);
}
