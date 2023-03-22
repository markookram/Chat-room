using ChatRoom.Domain.Events;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ChatRoom.Domain.Events.Enum;
using ChatRoom.Persistence.SqlLite.Extensions;

namespace ChatRoom.Persistence.DataStore.SqlLite.Migrations;

public static class ChatRoomEventsDataSeed
{
    private const int _roomId1 = 1;
    private const int _roomId2 = 2;
    private static int _counter = 1;

    public static ModelBuilder SeedChatRoomEventsData(this ModelBuilder builder)
    {
        builder.SeedEvents();

        return builder;
    }

    private static ModelBuilder SeedEvents(this ModelBuilder builder)
    {
        var startDateTime = new DateTime(2021, 3, 18, 8, 10, 0);

        EntityTypeBuilder<ParticipantEntered> pe = builder.Entity<ParticipantEntered>();
        EntityTypeBuilder<ParticipantLeft> pl = builder.Entity<ParticipantLeft>();
        EntityTypeBuilder<ParticipantCommented> pc = builder.Entity<ParticipantCommented>();
        EntityTypeBuilder<ParticipantHighFived> phf = builder.Entity<ParticipantHighFived>();

        pe.HasData(CreateChatEvent(1, EventType.ParticipantEntered, 1, "Mike on Sql", _roomId1).TweakDateOfBirth(startDateTime));
        pe.HasData(CreateChatEvent(2, EventType.ParticipantEntered, 2, "Bob on Sql", _roomId1).TweakDateOfBirth(startDateTime));
        pe.HasData(CreateChatEvent(3, EventType.ParticipantEntered, 3, "Kate on Sql", _roomId1).TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));
        pe.HasData(CreateChatEvent(4, EventType.ParticipantEntered, 4, "Alice on Sql", _roomId1).TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));

        pc.HasData(CreateChatEvent(5, EventType.ParticipantCommented, 1, "Mike on Sql", _roomId1, message: "Hi...").TweakDateOfBirth(startDateTime.AddMinutes(1)));
        pc.HasData(CreateChatEvent(6, EventType.ParticipantCommented, 2, "Bob on Sql", _roomId1, message: "Same to you").TweakDateOfBirth(startDateTime.AddMinutes(1)));
        pc.HasData(CreateChatEvent(7, EventType.ParticipantCommented, 3, "Kate on Sql", _roomId1, message: "Alice do you hear us?").TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));
        pc.HasData(CreateChatEvent(8, EventType.ParticipantCommented, 4, "Alice on Sql", _roomId1, message: "Yes, sorry my headphones were muted.").TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));

        pl.HasData(CreateChatEvent(9, EventType.ParticipantLeft, 4, "Alice on Sql", _roomId1).TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));
        pc.HasData(CreateChatEvent(10, EventType.ParticipantCommented, 2, "Bob on Sql", _roomId1, message: "Before we start, Kate last time forgot to tell you how I like your high-five gesture :)").TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));
        phf.HasData(CreateChatEvent(11, EventType.ParticipantHighFived, 3, "Kate on Sql", _roomId1, 2, "Bob on Sql").TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));
        pc.HasData(CreateChatEvent(12, EventType.ParticipantCommented, 1, "Mike on Sql", _roomId1, message: "Oooo sorry guys I have to go on another meeting, sorryyyyy....").TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));

        pl.HasData(CreateChatEvent(13, EventType.ParticipantLeft, 1, "Mike on Sql", _roomId1).TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));


        pe.HasData(CreateChatEvent(14, EventType.ParticipantEntered, 4, "Alice on Sql", _roomId2).TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));
        pe.HasData(CreateChatEvent(15, EventType.ParticipantEntered, 1, "Mike on Sql", _roomId2).TweakDateOfBirth(startDateTime.AddHours(_counter++).AddMinutes(_counter++)));
        pc.HasData(CreateChatEvent(16, EventType.ParticipantCommented, 1, "Mike on Sql", _roomId2, message: "Hi Alice, sorry for being late.").TweakDateOfBirth(startDateTime));

        pc.HasData(CreateChatEvent(17, EventType.ParticipantCommented, 4, "Alice on Sql", _roomId2, message: "nw").TweakDateOfBirth(startDateTime));
        phf.HasData(CreateChatEvent(18, EventType.ParticipantHighFived, 3, "Kate on Sql", _roomId2, 1, "Mike on Sql").TweakDateOfBirth(startDateTime));
        phf.HasData(CreateChatEvent(19, EventType.ParticipantHighFived, 3, "Kate on Sql", _roomId2, 2, "Bob on Sql").TweakDateOfBirth(startDateTime));
        phf.HasData(CreateChatEvent(20, EventType.ParticipantHighFived, 3, "Kate on Sql", _roomId2, 1, "Mike on Sql").TweakDateOfBirth(startDateTime.AddHours(1)));
        phf.HasData(CreateChatEvent(21, EventType.ParticipantHighFived, 1, "Mike on Sql", _roomId2, 2, "Bob on Sql").TweakDateOfBirth(startDateTime.AddHours(1)));

        return builder;

    }

    private static ChatEvent CreateChatEvent(int id, EventType type, int participantId, string participantName,
        int chatRoomId,
        int? toParticipantId = null, string? toParticipantName = null, string? message = null)
    {
        return type switch
        {
            EventType.ParticipantEntered => new ParticipantEntered(participantId, participantName, chatRoomId)
                .AddIdentity(id)
                .AddMessage(message)
                .SetRecipient(toParticipantId, toParticipantName)
                .CompleteAuditing(),
            EventType.ParticipantLeft => new ParticipantLeft(participantId, participantName, chatRoomId).AddIdentity(id)
                .AddMessage(message)
                .SetRecipient(toParticipantId, toParticipantName)
                .CompleteAuditing(),
            EventType.ParticipantCommented => new ParticipantCommented(participantId, participantName, chatRoomId)
                .AddIdentity(id)
                .AddMessage(message)
                .SetRecipient(toParticipantId, toParticipantName)
                .CompleteAuditing(),
            EventType.ParticipantHighFived => new ParticipantHighFived(participantId, participantName, chatRoomId)
                .AddIdentity(id)
                .AddMessage(message)
                .SetRecipient(toParticipantId, toParticipantName)
                .CompleteAuditing(),
            _ => throw new NotSupportedException()
        };
    }
}