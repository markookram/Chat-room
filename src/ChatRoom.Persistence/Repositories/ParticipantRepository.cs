using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Domain.Model;

namespace ChatRoom.Persistence.Repositories;

/// <summary>
/// Implements participant repo.
/// </summary>
public class ParticipantRepository : IAggregateRootRepository<Participant>
{
    private readonly IDataStore _dataStore;

    public ParticipantRepository(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public async Task AddOrUpdateAsync(Participant participant, CancellationToken cancellationToken = default)
    {
        await _dataStore.UpsertDataAsync(participant, cancellationToken);
    }

    public Task DeleteAsync(Participant participant, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<Participant>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return (await _dataStore.GetDataAsync<Participant>(cancellationToken)).ToList();
    }

    public async Task<Participant?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return (await _dataStore.GetDataAsync<Participant>(id, cancellationToken));
    }
}