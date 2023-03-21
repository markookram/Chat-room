using ChatRoom.Domain.Model;
using ChatRoom.Persistence.SqlLite.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Persistence.SqlLite.Migrations;

public static class ChatRoomDataSeed
{
    public static ModelBuilder SeedChatRoomData(this ModelBuilder builder)
    {
        builder.SeedChatRoom();
        builder.SeedParticipant();

        return builder;
    }

    private static ModelBuilder SeedChatRoom(this ModelBuilder builder)
    {
        EntityTypeBuilder<Domain.Model.ChatRoom> ent = builder.Entity<Domain.Model.ChatRoom>();

        ent.HasData(CreateChatRoom(1, "Sql server"));
        ent.HasData(CreateChatRoom(2, "Oracle"));

        return builder;

    }

    private static ModelBuilder SeedParticipant(this ModelBuilder builder)
    {
        EntityTypeBuilder<Participant> ent = builder.Entity<Participant>();

        ent.HasData(CreateParticipant(1, "Mike on Sql"));
        ent.HasData(CreateParticipant(2, "Bob on Sql"));
        ent.HasData(CreateParticipant(3, "Kate on Sql"));
        ent.HasData(CreateParticipant(4, "Alice on Sql"));

        return builder;

    }

    private static Domain.Model.ChatRoom CreateChatRoom(int id, string name)
    {
        return new Domain.Model.ChatRoom(name)
            .AddIdentity(id)
            .CompleteAuditing();
    }

    private static Participant CreateParticipant(int id, string name)
    {
        return new Participant(name)
            .AddIdentity(id)
            .CompleteAuditing();
    }
}