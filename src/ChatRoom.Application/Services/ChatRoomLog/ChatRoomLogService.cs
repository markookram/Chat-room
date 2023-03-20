using ChatRoom.Application.Abstractions.Events.Enum;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Application.Abstractions.Queries;
using ChatRoom.Application.Abstractions.Services.ChatRoomLog;
using ChatRoom.Application.Services.ChatRoom;
using ChatRoom.Domain.Events;
using Microsoft.Extensions.Logging;

namespace ChatRoom.Application.Services.ChatRoomLog;

/// <summary>
/// Implements chat-room log service.
/// </summary>
public class ChatRoomLogService : IChatRoomLogService
{
    private readonly ILogger<ChatRoomLogService> _logger;

    /// <summary>
    /// Factory for dynamic IQuery<IQueryParams,Task<StringQueryResult>>> service resolution
    /// </summary>
    private readonly Func<GranularityType, IQuery<IQueryParams, Task<StringQueryResult>>> _queryFactory;

    private readonly IChatRoomLogRepository<ChatEvent> _chatRoomLogRepository;

    public ChatRoomLogService(ILogger<ChatRoomLogService> logger,
        Func<GranularityType, IQuery<IQueryParams,
        Task<StringQueryResult>>> queryFactory,
        IChatRoomLogRepository<ChatEvent> chatRoomLogRepository)
    {
        _logger = logger;
        _queryFactory = queryFactory;
        _chatRoomLogRepository = chatRoomLogRepository;
    }

    public async Task LogEventAsync(ChatEvent @event, CancellationToken cancellationToken = default)
    {
        await _chatRoomLogRepository.AddAsync(@event);
    }

    public async Task<StringQueryResult> ReadLogAsync(IQueryParams prms, CancellationToken cancellationToken = default)
    {
        return await _queryFactory(prms.GranularityType).ExecuteAsync(prms, cancellationToken);
    }
}