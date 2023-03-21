using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;

namespace ChatRoom.Persistence.Repositories;

/// <summary>
/// Implements chat-room repo.
/// </summary>
public class ChatRoomRepository : IChatRoomRepository<Domain.Model.ChatRoom>
{
    private readonly IDataStore _dataStore;

    public ChatRoomRepository(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }


    async Task<IList<Domain.Model.ChatRoom>> IRepository<Domain.Model.ChatRoom>.GetAllAsync(CancellationToken cancellationToken)
    {
        return (await _dataStore.GetDataAsync<Domain.Model.ChatRoom>(cancellationToken)).ToList();
    }

    async Task<Domain.Model.ChatRoom?> IRepository<Domain.Model.ChatRoom>.GetAsync(int id, CancellationToken cancellationToken)
    {
        return (await _dataStore.GetDataAsync<Domain.Model.ChatRoom>(id, cancellationToken));
    }

    public async Task AddOrUpdateAsync(Domain.Model.ChatRoom room, CancellationToken cancellationToken = default)
    {
        await _dataStore.UpsertDataAsync(room, cancellationToken);
    }

    public Task DeleteAsync(Domain.Model.ChatRoom room, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}