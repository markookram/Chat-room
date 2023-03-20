using ChatRoom.Domain;

namespace ChatRoom.Application.Abstractions.Infrastructure.Repositories;

/// <summary>
/// Defines chat-room repo.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IChatRoomRepository<T> : IRepository<T> where T : class, IAggregateRoot
{
    Task AddOrUpdateAsync(T room, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}