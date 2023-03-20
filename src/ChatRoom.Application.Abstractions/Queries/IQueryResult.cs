namespace ChatRoom.Application.Abstractions.Queries;

public interface IQueryResult
{
}

/// <summary>
/// Represents a query response
/// </summary>
/// <typeparam name="T">Type of the response</typeparam>
public interface IQueryResult<out T> : IQueryResult where T : class
{
    T Result { get; }
}

