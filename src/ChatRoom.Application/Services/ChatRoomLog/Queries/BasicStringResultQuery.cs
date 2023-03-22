using ChatRoom.Application.Abstractions.Queries;
using ChatRoom.Application.Abstractions.Queries.Params;
using Microsoft.Extensions.Logging;

namespace ChatRoom.Application.Services.ChatRoomLog.Queries;

public abstract class BasicStringResultQuery : IQuery<IQueryParams, Task<StringQueryResult>>
{
    protected readonly ILogger<BasicStringResultQuery> Logger;

    protected BasicStringResultQuery(ILogger<BasicStringResultQuery> logger)
    {
        Logger = logger;
    }

    public abstract Task<StringQueryResult> ExecuteAsync(IQueryParams queryParams,
        CancellationToken cancellationToken = default);

}