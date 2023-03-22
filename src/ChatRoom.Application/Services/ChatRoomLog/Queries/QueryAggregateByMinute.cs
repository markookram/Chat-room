using System.Text;
using ChatRoom.Application.Abstractions.Extensions;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Application.Abstractions.Queries;
using ChatRoom.Application.Abstractions.Queries.Params;
using ChatRoom.Domain.Events;
using ChatRoom.Domain.Events.Enum;
using ChatRoom.Domain.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace ChatRoom.Application.Services.ChatRoomLog.Queries;

public class QueryAggregateByMinute : BasicStringResultQuery
{
    private readonly IChatRoomLogRepository<ChatEvent> _chatRoomLogRepository;

    public QueryAggregateByMinute(ILogger<QueryAggregateByMinute> logger,
        IChatRoomLogRepository<ChatEvent> chatRoomLogRepository)
        : base(logger)
    {
        _chatRoomLogRepository = chatRoomLogRepository;
    }

    public override async Task<StringQueryResult> ExecuteAsync(IQueryParams queryParams, CancellationToken cancellationToken = default)
    {
        var groups = (await _chatRoomLogRepository.GetAllAsync(cancellationToken))
            .Where(e => e.ChatRoomId == queryParams.RoomId)
            .OrderByDescending(e => e.CreatedOn)
            .GroupBy(e => new { e.CreatedOn.Date, e.CreatedOn.Hour, e.CreatedOn.Minute });

        var sb = new StringBuilder();

        foreach (var group in groups)
        {
            sb.AppendLine($"{(group.Key.Date, group.Key.Hour, group.Key.Minute).ToTuple().ToFormatedString("hh:mm tt")}: ");

            IEnumerable<IGrouping<dynamic, ChatEvent>> gr;

            if(group.All(x=>x.Type == EventType.ParticipantHighFived))
            {
                gr = group.GroupBy(i => new {i.Type, i.ParticipantId}).ToList();
            }
            else
            {
                gr = group.GroupBy(i => new {i.Type});
            }

            foreach (var agg in gr.Select(g => new
                     {
                         Event = g.First(),
                         Total = g.Count()
                     }))
            {
                sb.AppendLine(string.Join(Constants.vbTab, Constants.vbTab, agg.Event.AggregateString(agg.Total.ToString())));
            }

            sb.AppendLine();
        }

        return await Task.Run(() => new StringQueryResult(sb.ToString()));
    }
}