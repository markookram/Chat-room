using ChatRoom.Domain;

namespace ChatRoom.Application.Abstractions.Infrastructure.Repositories;

public interface IRepository
{
}

public interface IRepository<T> : IRepository where T : class, IEntity
{
    Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetAsync(int id, CancellationToken cancellationToken = default);
}