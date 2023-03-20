using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Domain.Events;
using ChatRoom.Persistence.DataStore.SqlLite.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Persistence.DataStore.SqlLite.Persistence;

public sealed class SqlLiteEventsDataStore : IEventsDataStore
{
    private readonly SqlLiteChatRoomLogDataContext _context;

    public SqlLiteEventsDataStore(SqlLiteChatRoomLogDataContext context)
    {
        _context = context;
    }

    public async Task<int> AddDataAsync(IChatEvent data, CancellationToken cancellationToken = default)
    {
        _context.Add(data);
        _context.Entry(data).State = EntityState.Added;

        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IList<ChatEvent>> GetDataAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ChatEvents.ToListAsync(cancellationToken);
    }

    public async Task<ChatEvent?> GetDataAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.ChatEvents.FirstOrDefaultAsync(e=>e.Id == id, cancellationToken);
    }
}