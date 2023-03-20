using System.Text;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Application.Abstractions.Queries;
using ChatRoom.Domain.Events;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace ChatRoom.Application.Services.ChatRoomLog.Queries;

public class QueryAll : BasicStringResultQuery
{
    private readonly IChatRoomLogRepository<ChatEvent> _chatRoomLogRepository;

    public QueryAll(ILogger<QueryAll> logger,
        IChatRoomLogRepository<ChatEvent> chatRoomLogRepository)
        :base(logger)
    {
        _chatRoomLogRepository = chatRoomLogRepository;
    }

    public override async Task<StringQueryResult> ExecuteAsync(IQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        var events = (await _chatRoomLogRepository.GetAllAsync(cancellationToken))
            .Where(e => e.ChatRoomId == queryParams.RoomId)
            .OrderByDescending(e => e.CreatedOn);

        var sb = new StringBuilder();
        sb.AppendJoin(Constants.vbCrLf, events.Select(e => $"{e.CreatedOn:hh:mm tt}:{Constants.vbTab}{e}"));

        return await Task.Run(() => new StringQueryResult(sb.ToString()));

    }
}