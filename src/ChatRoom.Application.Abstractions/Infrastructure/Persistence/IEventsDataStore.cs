using ChatRoom.Domain.Events;

namespace ChatRoom.Application.Abstractions.Infrastructure.Persistence;

/// <summary>
/// Abstract chat room events persistence.
/// </summary>
public interface IEventsDataStore
{
    Task<int> AddDataAsync(IChatEvent data, CancellationToken cancellationToken = default);

    Task<IList<ChatEvent>> GetDataAsync(CancellationToken cancellationToken = default);

    Task<ChatEvent?> GetDataAsync(int id, CancellationToken cancellationToken = default);
}