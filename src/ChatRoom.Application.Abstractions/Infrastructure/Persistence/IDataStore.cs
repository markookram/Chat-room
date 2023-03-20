using ChatRoom.Domain;
using ChatRoom.Domain.Model;

namespace ChatRoom.Application.Abstractions.Infrastructure.Persistence;

/// <summary>
/// Abstract chat room persistence.
/// </summary>
public interface IDataStore
{
    Task<int> UpsertDataAsync<T>(T data, CancellationToken cancellationToken = default) where T : class, IAggregateRoot;

    Task<T> GetDataAsync<T>(int id, CancellationToken cancellationToken = default) where T : class, IChatRoomEntity;

    Task<IList<T>> GetDataAsync<T>(CancellationToken cancellationToken = default) where T : class, IChatRoomEntity;
}