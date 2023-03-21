using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using ChatRoom.Application.Abstractions.Extensions;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Application.Abstractions.Queries;
using ChatRoom.Domain.Events;
using ChatRoom.Domain.Events.Enum;
using ChatRoom.Domain.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace ChatRoom.Application.Services.ChatRoomLog.Queries;

public class QueryAggregateByHour : BasicStringResultQuery
{
    private readonly IChatRoomLogRepository<ChatEvent> _chatRoomLogRepository;

    public QueryAggregateByHour(ILogger<QueryAggregateByHour> logger,
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
            .GroupBy(e => new { e.CreatedOn.Date, e.CreatedOn.Hour });

        var sb = new StringBuilder();

        foreach (var group in groups)
        {

            sb.AppendLine($"{(group.Key.Date, group.Key.Hour, 0).ToTuple().ToFormatedString("hh:mm tt")}: ");

            IEnumerable<IGrouping<dynamic, ChatEvent>> gr;

            if(group.All(x=>x.Type == EventType.PariticipantHighFived))
            {
                gr = group.GroupBy(i => new {i.Type, i.ParticipantId}).ToList();
            }
            else
            {
                gr = group.GroupBy(i => new {i.Type});
            }

            foreach (var agg in gr.Select(g => new
                     {
                         Type = g.First().GetType(),
                         EventType = (EventType)g.Key.Type,
                         Total = g.Count()
                     }))
            {
                sb.AppendLine(string.Join(Constants.vbTab, Constants.vbTab, (agg.Type).ToDescription(agg.EventType, agg.Total.ToString())));
            }

            sb.AppendLine();
        }

        return await Task.Run(() => new StringQueryResult(sb.ToString()));
    }
}