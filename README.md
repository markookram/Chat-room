# Chat-room

### Features

- Chat-room 

Chat-room is a helper component  for different types of events generation.
1. enter-the-room
2. leave-the-room
3. comment
4. high-five-another-user

- Chat-room event logger (statistics) 

Main component, responsible for logging events and serving their projected logs.
1. all
2. grouped by minute
3. grouped by hour
4. aggregations
	1. by hour
	2. by minute

All search results are delivered in string format.

------------

### Design
Chat-room app. tries to follow clean architecting principle using Onion implementation.
It's been divided into three main parts/layers
- Presentation
- Core
- Infrastructure

![Design](https://user-images.githubusercontent.com/5808394/226568338-f96861af-db88-4f31-9678-dee46ffce27a.png)

Presentation is an outer delivery layer (Web UI).
Infrastructure layer is an outer layer,  includes repositories and data stores.
Application and domain bellongs to the core of the system and forms inner layers responsible for the business.



------------


### Implementation

Solution follows design decisions. 

![Sln](https://user-images.githubusercontent.com/5808394/226568085-ed526cd5-523f-45bc-9800-a9216e412f1d.png)


**Presentation**
- WebUI

**Core layer**
- Application
- Domain

**Infrastructure layer**
- Persistance

**Composition**

Implements composition root,  where all of the services in the application dependencies are defined and "wired up" and makes layer dependencies and flow control clean  as possible.

***Note:***  It's not starting point of the app.

**Tests**
- Unit tests
- Integration tests

#### Tech stack
- .NET6
- Autofac IoC
- AspNet MVC
- EF Core
- Automapper
- Sql lite
- xunit
- Moq
- Fluent.Assertions

#### Core layer

**Domain**

Defines domain objects, together with data and behaviour. It follows DDD best practicies, to some extent, having aggregate composition, with Participant entity as an aggregate and ChatRoom as an entity.

```csharp
/// <summary>
/// Participant
/// </summary>
public class Participant : Entity, IAggregateRoot
{
    public Participant(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public int? ChatRoomId { get; private set; }
    public ChatRoom? ChatRoom { get; private set; }


    public Participant AddToTheRoom(int? chatRoomId)
    {
        ChatRoomId = chatRoomId;

        return this;
    }

    public Participant RemoveFromTheRoom()
    {
        if(ChatRoomId == default)
            return this;

        ChatRoomId = default;
        ChatRoom?.RemoveParticipant(Id);

        return this;
    }

    public override Participant AddIdentity(int id)
    {
        Id = id;
        return this;
    }
}
```
```csharp
/// <summary>
/// Chat room
/// </summary>
public class ChatRoom : Entity, IChatRoomEntity
{
    public ChatRoom(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    private readonly List<Participant> _participants = new();
    public IReadOnlyCollection<Participant> Participants => _participants;

    public void AddParticipant(int participantId, string participantName)
    {
        if (_participants.All(p => p.Id != participantId))
            _participants.Add(new Participant(participantName)
                                              .AddIdentity(participantId)
                                              .AddToTheRoom(Id));
    }

    public void AddParticipant(Participant participant)
    {
        if (_participants.All(p => p.Id != participant.Id))
            _participants.Add(participant.AddToTheRoom(Id));
    }

    public void AddRangeParticipants(IList<Participant> participants)
    {
        foreach (var participant in participants)
        {
            AddParticipant(participant.Id, participant.Name);
        }
    }

    public void RemoveParticipant(int participantId)
    {
        var participant = _participants.SingleOrDefault(p => p.Id == participantId);
        if (participant == null)
            return;
        _participants.Remove(participant.RemoveFromTheRoom());
    }

    public override ChatRoom AddIdentity(int id)
    {
        Id = id;
        return this;
    }
}
```

 **Applicaition.Abstractions**
 
 Defines all the services application layer needs from the rest of the system and what serves to the rest of the system, following Inversion of dependency principle. 
 
 **Application**
 
 Implements Application abstraction and hosts application business logic (chat-room feature and chat-room event sourcing feature).
 
 *IChatRoomService*, helps event sourcing.
 ```csharp
/// <summary>
/// Describe basic chat-room features.
/// It helps events sourcing.
/// </summary>
public interface IChatRoomService
{
    Task AddParticipantAsync(int participantId,  int roomId, CancellationToken cancellationToken = default);

    Task RemoveParticipantAsync(int participantId, int roomId, CancellationToken cancellationToken = default);

    Task LeaveACommentAsync(int roomId, int participantId, string comment, CancellationToken cancellationToken = default);

    Task<ParticipantDto> HighFiveAsync(int roomId, int fromParticipantId, int toParticipantId, CancellationToken cancellationToken = default);


    Task<List<ChatRoomDto>> GetChatRoomsAsync(CancellationToken cancellationToken = default);

    Task<ChatRoomDto?> GetChatRoomAsync(int roomId, CancellationToken cancellationToken);

    Task<List<ParticipantDto>> GetParticipantsAsync(CancellationToken cancellationToken = default);

    Task<ParticipantDto?> GetParticipantAsync(int participantId, CancellationToken cancellationToken);

    Task<ChatRoomParticipantsDto?> GetChatRoomWithParticipantsAsync(int roomId, CancellationToken cancellationToken);
}
```
 
 *IChatRoomLogService* , API's for inserting and reading created events.
```csharp
/// <summary>
/// Provides basic APIs for events sourcing.
/// </summary>
public interface IChatRoomLogService
{
    Task LogEventAsync(ChatEvent @event, CancellationToken cancellationToken = default);

    Task<StringQueryResult> ReadLogAsync(IQueryParams prms, CancellationToken cancellationToken = default);
}
```
Quering events are implemented using Command pattern where requester only knows for the invoker and query. Query execution is left to the query itself. Using this approach also OCP principle has been satisfied, every new query type leaves user code unchanged.

IQuery, abstracts query command qith IQuery params as an input and IQueryResult (StringQuery result in this case) as an output.

```csharp
/// <summary>
/// Defines a query command.
/// </summary>
/// <typeparam name="Tin"></typeparam>
/// <typeparam name="Tout"></typeparam>
public interface IQuery<in Tin, out Tout> where Tin : IQueryParams
                                          where Tout : Task<StringQueryResult>
{
    Tout ExecuteAsync(Tin queryParams, CancellationToken cancellationToken = default);
}
```

```csharp

/// <summary>
/// Represents a query params
/// </summary>
public interface IQueryParams
{
    GranularityType GranularityType { get; }

    int RoomId { get; set; }

    int ParticipantId { get; set; }
}
```

```csharp
public interface IQueryResult
{
}

/// <summary>
/// Represents a query response
/// </summary>
/// <typeparam name="T">Type of the response</typeparam>
public interface IQueryResult<out T> : IQueryResult where T : class
{
    T Result { get; }
}

```

#### Presentation

Uses AspNetCore MVC Web project as an delivery in combination with Javascript. 
Start-up component.

*ChatRoomController*

Delivers chat-room features

- enter the room
- send comments
- send high-five
- leave the room

*ChatRoomLogController*

Events source delivery.

*ChatRoomService*

It hides application services from the UI code, proxies it, behaving as smart proxy, adapting request/replies to the UI needs.

#### Infrastructure

**Persistence**

Persistence layer has been abstracted as an unit of work divided on repositories and data stores. 

Repositories are organised separately for write and read only.

```csharp
/// <summary>
/// Defines write only repo.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAggregateRootRepository<T> : IRepository<T> where T : class, IAggregateRoot
{
    Task AddOrUpdateAsync(T root, CancellationToken cancellationToken = default);

    Task DeleteAsync(T root, CancellationToken cancellationToken = default);
}
```

```csharp
public interface IRepository
{
}

/// <summary>
/// Defines read-only repo.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> : IRepository where T : class, IEntity
{
    Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetAsync(int id, CancellationToken cancellationToken = default);
}
```
There are two different types of data stores
- In-memory (currently, not fully operational)
- Sql lite

***Note:***  Both data stores are seeded with chatRoom's and participant's.

Data stores are abstracted based on a type of entities they persisted. One for chat-room and the other for event sourcing.
 - IDataStore
 - IEventDataStore

They expose read/write services, again depends on the business needs.
 
 IDataStore -  Read, Insert and Update
```csharp
/// <summary>
/// Abstract chat room persistence.
/// </summary>
public interface IDataStore
{
    Task<int> UpsertDataAsync<T>(T data, CancellationToken cancellationToken = default) where T : class, IChatRoomEntity;

    Task<T> GetDataAsync<T>(int id, CancellationToken cancellationToken = default) where T : class, IChatRoomEntity;

    Task<IList<T>> GetDataAsync<T>(CancellationToken cancellationToken = default) where T : class, IChatRoomEntity;
}
```
 
 IEventDataStore - Read, Insert

```csharp
/// <summary>
/// Abstract chat room events persistence.
/// </summary>
public interface IEventsDataStore
{
    Task<int> AddDataAsync(IChatEvent data, CancellationToken cancellationToken = default);

    Task<IList<ChatEvent>> GetDataAsync(CancellationToken cancellationToken = default);
}
```
 
####  Tests

**Unit tests**

Tests main API's of the application layer.
- Chat-room events generation.
- Chat-room events sourcing.

**Integration tests**

Tests how different components work together. Uses In-Memory database as a data-store. 
- Chat-room events generation.
- Chat-room events sourcing.
 
####  Cross-cutting

All cross-cutting features, like logging, validation, exception-handling, security, that normally exists, here are ommited for the sake of simplicity and 'readibility'.
 

