using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Domain.Events;
using ChatRoom.Domain.Events.Enum;

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
        new ChatEvent(EventType.ParticipantEntered, 1, "Mike", _roomId1)
            .AddIdentity(_counter++),

        new ChatEvent(EventType.ParticipantEntered, 2, "Bob", _roomId1)
            .AddIdentity(_counter++),

        new ChatEvent(EventType.ParticipantEntered, 3, "Kate", _roomId1)
            .AddIdentity(_counter++),

        new ChatEvent(EventType.ParticipantEntered, 4, "Alice", _roomId1)
            .AddIdentity(_counter++),



        new ChatEvent(EventType.ParticipantCommented, 1, "Mike", _roomId1)
            .AddIdentity(_counter++)
            .AddMessage("Hi..."),

        new ChatEvent(EventType.ParticipantCommented, 2, "Bob", _roomId1)
            .AddIdentity(_counter++)
            .AddMessage("Same to you"),

        new ChatEvent(EventType.ParticipantCommented, 3, "Kate", _roomId1)
            .AddIdentity(_counter++)
            .AddMessage("Alice do you hear us?"),

        new ChatEvent(EventType.ParticipantCommented, 4, "Alice", _roomId1)
            .AddIdentity(_counter++)
            .AddMessage("Yes, sorry my headphones were muted."),



        new ChatEvent(EventType.ParticipantLeft, 4, "Alice", _roomId1)
            .AddIdentity(_counter++),

        new ChatEvent(EventType.ParticipantCommented, 2, "Bob", _roomId1)
            .AddIdentity(_counter++)
            .AddMessage("Before we start, Kate last time forgot to tell you how I like your high-five gesture :)"),

        new ChatEvent(EventType.PariticipantHighFived, 3, "Kate", _roomId1)
            .AddIdentity(_counter++)
            .SetRecipient(2, "Bob"),

        new ChatEvent(EventType.PariticipantHighFived, 3, "Kate", _roomId1)
            .AddIdentity(_counter++)
            .SetRecipient(2, "Bob"),

        new ChatEvent(EventType.ParticipantLeft, 1, "Mike", _roomId1)
            .AddIdentity(_counter++),



        new ChatEvent(EventType.ParticipantEntered, 4, "Alice", _roomId2)
            .AddIdentity(_counter++),

        new ChatEvent(EventType.ParticipantEntered, 1, "Mike", _roomId2)
            .AddIdentity(_counter++),

        new ChatEvent(EventType.ParticipantCommented, 1, "Mike", _roomId2)
            .AddIdentity(_counter++)
            .AddMessage("Hi Alice, sorry for being late."),

        new ChatEvent(EventType.ParticipantCommented, 4, "Alice", _roomId2)
            .AddIdentity(_counter++)
            .AddMessage("nw"),
    };

    public InMemoryEventsDataStore()
    {
        InitDataStore();
    }

    private void InitDataStore()
    {
        var startDateTime = new DateTime(2021, 3, 18, 8, 10, 0);

        //Room1
        _store[0].TweakDateOfBirth(startDateTime);
        _store[1].TweakDateOfBirth(startDateTime.AddMinutes(1));
        _store[2].TweakDateOfBirth(startDateTime.AddMinutes(1));
        _store[3].TweakDateOfBirth(startDateTime.AddMinutes(2));


        _store[4].TweakDateOfBirth(startDateTime.AddHours(1).AddMinutes(10));
        _store[5].TweakDateOfBirth(startDateTime.AddHours(1).AddMinutes(11));
        _store[6].TweakDateOfBirth(startDateTime.AddHours(2).AddMinutes(11));
        _store[7].TweakDateOfBirth(startDateTime.AddHours(2).AddMinutes(11));

        _store[8].TweakDateOfBirth(startDateTime.AddHours(1).AddMinutes(10));
        _store[9].TweakDateOfBirth(startDateTime.AddHours(1).AddMinutes(12));
        _store[10].TweakDateOfBirth(startDateTime.AddHours(1).AddMinutes(13));
        _store[11].TweakDateOfBirth(startDateTime.AddHours(1).AddMinutes(13));
        _store[12].TweakDateOfBirth(startDateTime.AddHours(2).AddMinutes(10));

        //Room2
        _store[13].TweakDateOfBirth(startDateTime.AddHours(2).AddMinutes(10));
        _store[14].TweakDateOfBirth(startDateTime.AddHours(2).AddMinutes(11));
        _store[15].TweakDateOfBirth(startDateTime.AddHours(3).AddMinutes(10));
        _store[16].TweakDateOfBirth(startDateTime.AddHours(3).AddMinutes(10));
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
        return await Task.Run(() => _store.FirstOrDefault(e => e.Id == id));
    }
}