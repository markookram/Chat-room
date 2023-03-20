using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Domain.Model;

namespace ChatRoom.Persistence.Repositories;

/// <summary>
/// Implements participant repo.
/// </summary>
public class ParticipantRepository : IRepository<Participant>
{
    private readonly IDataStore _dataStore;

    public ParticipantRepository(IDataStore dataStore)
    {
        _dataStore = dataStore;
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