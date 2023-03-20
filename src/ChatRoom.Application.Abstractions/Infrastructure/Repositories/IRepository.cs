using ChatRoom.Domain;

namespace ChatRoom.Application.Abstractions.Infrastructure.Repositories;

public interface IRepository
{
}

/// <summary>
/// Defines read-only repo.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> : IRepository where T : class, IEntity
{
    Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetAsync(int id, CancellationToken cancellationToken = default);
}