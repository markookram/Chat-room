using System.Text;
using ChatRoom.Application.Abstractions.Extensions;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Application.Abstractions.Queries;
using ChatRoom.Domain.Events;
using ChatRoom.Domain.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace ChatRoom.Application.Services.ChatRoomLog.Queries;

public class QueryAggregateByHour : BasicStringResultQuery
{
    private readonly IChatRoomLogRepository<ChatEvent> _chatRoomLogRepository;

    public QueryAggregateByHour(ILogger<QueryAggregateByHour> logger,
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
            .GroupBy(e => new {e.CreatedOn.Date, e.CreatedOn.Hour});

        var sb = new StringBuilder();

        foreach (var group in groups)
        {
            sb.AppendLine($"{(group.Key.Date, group.Key.Hour, 0).ToTuple().ToFormatedString("hh:mm tt")}: ");

            foreach (var agg in group.GroupBy(i => i.Type).Select(g => new
                     {
                         Type = g.First().GetType(),
                         EventType = g.Key,
                         Total = g.Count()
                     }))
            {
                sb.AppendLine(string.Join(Constants.vbTab, Constants.vbTab, agg.Type.ToDescription(agg.EventType, agg.Total)));
            }

            sb.AppendLine();
        }

        return await Task.Run(() => new StringQueryResult(sb.ToString()));

    }
}