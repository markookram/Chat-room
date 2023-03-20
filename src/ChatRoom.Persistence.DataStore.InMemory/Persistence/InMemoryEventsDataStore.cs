using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Domain.Events;

namespace ChatRoom.Persistence.DataStore.InMemory.Persistence;

/// <summary>
/// Custom in-memory chat-room data store.
/// </summary>
public class InMemoryEventsDataStore : IEventsDataStore
{
    private const int _roomId1 = 1;
    private const int _roomId2 = 2;
    private static int _counter = 1;

    private readonly IList<ChatEvent> _store = new List<ChatEvent>(6)
    {
        new ParticipantEntered(1, "Mike", _roomId1)
            .AddIdentity(_counter ++),

        new ParticipantEntered(2, "Bob", _roomId1)
            .AddIdentity(_counter ++),

        new ParticipantEntered(3, "Kate", _roomId1)
            .AddIdentity(_counter ++),

        new ParticipantEntered(4, "Alice", _roomId1)
            .AddIdentity(_counter ++),

        new ParticipantCommented(1, "Mike", _roomId1)
            .AddIdentity(_counter ++)
            .AddMessage("Hi..."),

        new ParticipantCommented(2, "Bob", _roomId1)
            .AddIdentity(_counter ++)
            .AddMessage("Same to you"),

        new ParticipantCommented(3, "Kate", _roomId1)
            .AddIdentity(_counter ++)
            .AddMessage("Alice do you hear us?"),

        new ParticipantCommented(4, "Alice", _roomId1)
            .AddIdentity(_counter ++)
            .AddMessage("Yes, sorry my headphones were muted."),

        new ParticipantLeft(4, "Alice", _roomId1)
            .AddIdentity(_counter ++),

        new ParticipantCommented(2, "Bob", _roomId1)
            .AddIdentity(_counter ++)
            .AddMessage("Before we start, Kate last time forgot to tell you how I like your high-five gesture :)"),

        new ParticipantHighFived(3, "Kate", _roomId1)
            .AddIdentity(_counter ++)
            .SetRecipient(2, "Bob"),

        new ParticipantCommented(1, "Mike", _roomId1)
            .AddIdentity(_counter ++)
            .AddMessage("Oooo sorry guys I have to go on another meeting, sorryyyyy...."),

        new ParticipantLeft(1, "Mike", _roomId1)
            .AddIdentity(_counter ++),

        new ParticipantEntered(4, "Alice", _roomId2)
            .AddIdentity(_counter ++),

        new ParticipantEntered(1, "Mike", _roomId2)
            .AddIdentity(_counter ++),

        new ParticipantCommented(1, "Mike", _roomId2)
            .AddIdentity(_counter ++)
            .AddMessage("Hi Alice, sorry for being late."),

        new ParticipantCommented(4, "Alice", _roomId2)
            .AddIdentity(_counter ++)
            .AddMessage("nw"),
    };

    public InMemoryEventsDataStore()
    {
        InitDataStore();
    }

    private void InitDataStore()
    {
        _store[0].TweakDateOfBirth(DateTime.Now.AddHours(-10));

        //Room1
        _store[1].TweakDateOfBirth(_store[0].CreatedOn.AddMinutes(4));
        _store[2].TweakDateOfBirth(_store[0].CreatedOn.AddMinutes(5));
        _store[3].TweakDateOfBirth(_store[0].CreatedOn.AddMinutes(6));
        _store[4].TweakDateOfBirth(_store[0].CreatedOn.AddMinutes(7));

        _store[5].TweakDateOfBirth(_store[3].CreatedOn.AddMinutes(12));
        _store[6].TweakDateOfBirth(_store[3].CreatedOn.AddMinutes(14));
        _store[7].TweakDateOfBirth(_store[3].CreatedOn.AddMinutes(16));
        _store[8].TweakDateOfBirth(_store[3].CreatedOn.AddMinutes(18));

        _store[9].TweakDateOfBirth(_store[7].CreatedOn.AddMinutes(2));
        _store[10].TweakDateOfBirth(_store[7].CreatedOn.AddMinutes(5));

        _store[11].TweakDateOfBirth(_store[7].CreatedOn.AddMinutes(8));
        _store[12].TweakDateOfBirth(_store[7].CreatedOn.AddMinutes(8));

        //Room2
        _store[13].TweakDateOfBirth(_store[12].CreatedOn.AddHours(1));
        _store[14].TweakDateOfBirth(_store[12].CreatedOn.AddHours(2));
        _store[15].TweakDateOfBirth(_store[12].CreatedOn.AddHours(3));
        _store[16].TweakDateOfBirth(_store[12].CreatedOn.AddHours(5));
    }

    public async Task<int> AddDataAsync(IChatEvent data, CancellationToken cancellationToken = default)
    {
        data.AddIdentity(_counter++);

        await Task.Run(() => _store.Add((ChatEvent)data));

        return data.Id;
    }

    public async Task<IList<ChatEvent>> GetDataAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _store);
    }

    public async Task<ChatEvent?> GetDataAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _store.FirstOrDefault(e=>e.Id == id));
    }
}