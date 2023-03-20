using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Domain;
using ChatRoom.Domain.Model;
using ChatRoom.Persistence.DataStore.SqlLite.Extensions;
using ChatRoom.Persistence.DataStore.SqlLite.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Persistence.DataStore.SqlLite.Persistence;

public class SqlLiteDataStore: IDataStore
{
    private readonly SqlLiteChatRoomDbContext _context;

    public SqlLiteDataStore(SqlLiteChatRoomDbContext context)
    {
        _context = context;
    }

    public async Task<int> UpsertDataAsync<T>(T data, CancellationToken cancellationToken = default) where T : class, IAggregateRoot
    {
        EntityState state;

        state = data.Id > 0 ? EntityState.Modified : EntityState.Added;

        _context.Add(data);

        _context.Entry(data).SetState(_context, state);

        var res =  await _context.SaveChangesAsync(cancellationToken);

        _context.Entry(data).SetState(_context, EntityState.Detached);

        return res;
    }

    public async Task<IList<T>> GetDataAsync<T>(CancellationToken cancellationToken = default) where T : class, IChatRoomEntity
    {
        return typeof(T).Name switch
        {
            nameof(Domain.Model.ChatRoom) => (IList<T>)await _context.ChatRooms.ToListAsync(cancellationToken),
            nameof(Participant) => (IList<T>)await _context.Participants.ToListAsync(cancellationToken),
            _ => new List<T>()
        };
    }

    public async Task<T> GetDataAsync<T>(int id, CancellationToken cancellationToken = default) where T : class, IChatRoomEntity
    {
        return (typeof(T).Name switch
        {
            nameof(Domain.Model.ChatRoom) => (await _context.ChatRooms.FirstAsync(c => c.Id == id, cancellationToken)) as T,
            nameof(Participant) => (await _context.Participants.FirstAsync(p => p.Id == id, cancellationToken)) as T,
            _ => default!
        })!;

    }
}