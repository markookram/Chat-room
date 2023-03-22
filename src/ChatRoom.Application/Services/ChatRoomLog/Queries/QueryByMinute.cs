using System.Text;
using ChatRoom.Application.Abstractions.Extensions;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Application.Abstractions.Queries;
using ChatRoom.Application.Abstractions.Queries.Params;
using ChatRoom.Domain.Events;
using Microsoft.Extensions.Logging;

namespace ChatRoom.Application.Services.ChatRoomLog.Queries;

public class QueryByMinute : BasicStringResultQuery
{
    private readonly IChatRoomLogRepository<ChatEvent> _chatRoomLogRepository;

    public QueryByMinute(ILogger<QueryByMinute> logger,
        IChatRoomLogRepository<ChatEvent> chatRoomLogRepository)
        :base(logger)
    {
        _chatRoomLogRepository = chatRoomLogRepository;
    }

    public override async Task<StringQueryResult> ExecuteAsync(IQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        var groups = (await _chatRoomLogRepository.GetAllAsync(cancellationToken))
            .Where(e => e.ChatRoomId == queryParams.RoomId)
            .OrderByDescending(e => e.CreatedOn)
            .GroupBy(e => new {e.CreatedOn.Date, e.CreatedOn.Hour, e.CreatedOn.Minute});

        var sb = new StringBuilder();

        foreach (var group in groups)
        {
            sb.AppendLine($"{(group.Key.Date, group.Key.Hour, group.Key.Minute).ToTuple().ToFormatedString("hh:mm tt")}: ");
            sb.AppendLine();
            foreach (var @event in group)
            {
                sb.AppendLine($"        {@event}");
            }
            sb.AppendLine();
        }

        return await Task.Run(() => new StringQueryResult(sb.ToString()));
    }
}