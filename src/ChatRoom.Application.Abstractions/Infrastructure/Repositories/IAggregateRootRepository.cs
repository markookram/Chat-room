using ChatRoom.Domain;

namespace ChatRoom.Application.Abstractions.Infrastructure.Repositories;

/// <summary>
/// Defines write only repo.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAggregateRootRepository<T> : IRepository<T> where T : class, IAggregateRoot
{
    Task AddOrUpdateAsync(T root, CancellationToken cancellationToken = default);

    Task DeleteAsync(T root, CancellationToken cancellationToken = default);
}