using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Domain.Events;

namespace ChatRoom.Persistence.Repositories;

/// <summary>
/// Implements chat-room events repo.
/// </summary>
public class ChatRoomLogRepository : IChatRoomLogRepository<ChatEvent>
{
    private readonly IEventsDataStore _dataStore;

    public ChatRoomLogRepository(IEventsDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public async Task<IList<ChatEvent>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dataStore.GetDataAsync(cancellationToken);
    }

    public async Task AddAsync(ChatEvent @event, CancellationToken cancellationToken = default)
    {
        await _dataStore.AddDataAsync(@event, cancellationToken);
    }

    public async Task<ChatEvent?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dataStore.GetDataAsync(id, cancellationToken);
    }
}