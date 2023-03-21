using ChatRoom.Domain.Model;
using ChatRoom.Persistence.DataStore.SqlLite.Migrations;
using ChatRoom.Persistence.SqlLite.Configuration;
using Microsoft.EntityFrameworkCore;
#pragma warning disable CS8618

namespace ChatRoom.Persistence.DataStore.SqlLite.Persistence.Context;

public sealed class SqlLiteChatRoomDbContext : DbContext
{
    private static bool _created;

    public SqlLiteChatRoomDbContext(DbContextOptions<SqlLiteChatRoomDbContext> options)
        : base(options)
    {
        if (_created) return;
        _created = true;
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public DbSet<Domain.Model.ChatRoom> ChatRooms { get; set; }
    public DbSet<Participant> Participants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChatRoomTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ParticipantTypeConfiguration());

        modelBuilder.SeedChatRoomData();
    }
}