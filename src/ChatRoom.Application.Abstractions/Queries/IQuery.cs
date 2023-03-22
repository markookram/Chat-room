using ChatRoom.Application.Abstractions.Queries.Params;

namespace ChatRoom.Application.Abstractions.Queries;

/// <summary>
/// Defines a query command.
/// </summary>
/// <typeparam name="Tin"></typeparam>
/// <typeparam name="Tout"></typeparam>
public interface IQuery<in Tin, out Tout> where Tin : IQueryParams
                                          where Tout : Task<StringQueryResult>
{
    Tout ExecuteAsync(Tin queryParams, CancellationToken cancellationToken = default);
}