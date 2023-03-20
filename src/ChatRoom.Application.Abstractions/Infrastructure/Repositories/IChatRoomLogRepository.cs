using ChatRoom.Domain.Events;

namespace ChatRoom.Application.Abstractions.Infrastructure.Repositories;

/// <summary>
/// Defines chat events data repo.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IChatRoomLogRepository<T> : IRepository<T> where T : class, IChatEvent
{
    Task AddAsync(T @event, CancellationToken cancellationToken = default);
}