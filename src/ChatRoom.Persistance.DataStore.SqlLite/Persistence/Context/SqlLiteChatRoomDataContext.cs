using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Domain.Model;
using ChatRoom.Persistence.SqlLite.Configuration;
using ChatRoom.Persistence.SqlLite.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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

    /*public SqlLiteChatRoomDbContext(IOptions<ConnectionStringsOptions> options)
        : base(GetOptions(options.Value))
    {
        if (_created) return;
        _created = true;
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    private static DbContextOptions GetOptions(ConnectionStringsOptions connOptions)
    {
        var builder = new DbContextOptionsBuilder()
            .UseSqlite(connOptions.ChatRoomDatabase);

        return builder.Options;
    }*/

    public DbSet<Domain.Model.ChatRoom> ChatRooms { get; set; }
    public DbSet<Participant> Participants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChatRoomTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ParticipantTypeConfiguration());

        modelBuilder.SeedChatRoomData();
    }
}