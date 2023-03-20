using ChatRoom.Application.Abstractions.Events.Enum;

namespace ChatRoom.Application.Abstractions.Queries.Params;

public class QueryParams : DefaultQueryParams
{
    public QueryParams(GranularityType granularityType)
    {
        GranularityType = granularityType;
    }

    public override GranularityType GranularityType { get; }
}