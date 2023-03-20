using ChatRoom.Domain.Events;
using ChatRoom.Persistence.SqlLite.Configuration;
using ChatRoom.Persistence.SqlLite.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ChatRoom.Persistence.DataStore.SqlLite.Persistence.Context;

public sealed class SqlLiteChatRoomLogDataContext : DbContext
{
    private static bool _created;

    public SqlLiteChatRoomLogDataContext(DbContextOptions<SqlLiteChatRoomLogDataContext> options)
        : base(options)
    {
        if (_created) return;
        _created = true;
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    /*public SqlLiteChatRoomLogDataContext(IOptions<ConnectionStringsOptions> options)
        : base(GetOptions(options.Value))
    {
        if (_created) return;
        _created = true;
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    private static DbContextOptions GetOptions(ConnectionStringsOptions conOptions)
    {
        var builder = new DbContextOptionsBuilder()
            .UseSqlite(conOptions.ChatRoomLogDatabase);

        return builder.Options;
    }*/

    public DbSet<ChatEvent> ChatEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChatEventTypeConfiguration());

        modelBuilder.SeedChatRoomEventsData();
    }
}