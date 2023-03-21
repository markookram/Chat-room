using ChatRoom.Domain.Events;
using ChatRoom.Persistence.DataStore.SqlLite.Configuration;
using ChatRoom.Persistence.DataStore.SqlLite.Migrations;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Persistence.DataStore.SqlLite.Persistence.Context;

#pragma warning disable CS8618
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

    public DbSet<ChatEvent> ChatEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChatEventTypeConfiguration());

        modelBuilder.SeedChatRoomEventsData();
    }
}