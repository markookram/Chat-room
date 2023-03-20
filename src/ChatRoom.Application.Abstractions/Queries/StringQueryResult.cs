namespace ChatRoom.Application.Abstractions.Queries;

/// <summary>
/// String like query response
/// </summary>
public class StringQueryResult : IQueryResult<string>
{
    public StringQueryResult(string result)
    {
        Result = result;
    }

    public string Result { get; }
}