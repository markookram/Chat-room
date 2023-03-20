using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Domain;
using ChatRoom.Domain.Model;

namespace ChatRoom.Persistence.DataStore.InMemory.Persistence;

/// <summary>
/// Custom in-memory chat-room events data store.
/// </summary>
public class InMemoryDataStore : IDataStore
{
    private static int _counter;

    private readonly IDictionary<int, string> _participants = new Dictionary<int, string>()
    {
        {1, "Mike"},
        {2, "Bob"},
        {3, "Kate"},
        {4, "Alice"},
    };

    private readonly IList<IChatRoomEntity> _store = new List<IChatRoomEntity>(6);

    public InMemoryDataStore()
    {
        InitDataStore();
    }

    private void InitDataStore()
    {
        var itRoom = new Domain.Model.ChatRoom("IT")
            .AddIdentity(1);

        var sportRoom = new Domain.Model.ChatRoom("Sports")
            .AddIdentity(2);

        _store.Add(itRoom);
        _store.Add(sportRoom);

        for (_counter = 1; _counter < 5; _counter++)
        {
            var participant = new Participant(_participants[_counter])
                .AddIdentity(_counter);

            _store.Add(participant);
        }
    }

    public async Task<int> UpsertDataAsync<T>(T data, CancellationToken cancellationToken = default) where T : class, IAggregateRoot
    {
        return await Task.Run(() =>
        {
            var entity = _store.OfType<T>().First(s => s.Id == data.Id);

            entity = data;

            return entity.Id;
        });
    }

    public async Task<T> GetDataAsync<T>(int id, CancellationToken cancellationToken = default) where T : class, IChatRoomEntity
    {
        return await Task.Run(() =>
        {
            var data = _store.OfType<T>().First(x => x.Id == id);

            if (data is Domain.Model.ChatRoom chatRoom)
            {
                ResolveAggregateRoot(chatRoom);
            }
            return data;
        });
    }

    public async Task<IList<T>> GetDataAsync<T>(CancellationToken cancellationToken = default) where T : class, IChatRoomEntity
    {
        return await Task.Run(() =>
        {
            var data = _store.OfType<T>().ToList();

            if (data is IList<Domain.Model.ChatRoom> chatRooms)
            {
                ResolveAggregateRoots(chatRooms);
            }
            return data;
        });
    }

    void ResolveAggregateRoots(IList<Domain.Model.ChatRoom> chatRooms)
    {
        foreach (var chatRoom in chatRooms)
        {
            ResolveAggregateRoot(chatRoom);
        }
    }

    void ResolveAggregateRoot(Domain.Model.ChatRoom chatRoom)
    {
        var participants = _store.OfType<Participant>().Where(p => p.ChatRoomId == chatRoom.Id).ToList();
        chatRoom.AddRangeParticipants(participants);
    }
}