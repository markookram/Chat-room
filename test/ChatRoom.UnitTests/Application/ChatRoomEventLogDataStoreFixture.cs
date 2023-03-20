using ChatRoom.Domain.Events;


namespace ChatRoom.UnitTests.Application;

public class ChatRoomEventLogDataStoreFixture
{
    public const int RoomId = 2;
    private static int _counter = 1;

    public readonly IList<ChatEvent> ChatEvents = new List<ChatEvent>(6)
    {
        new ParticipantEntered(1, "Mike", RoomId)
            .AddIdentity(_counter ++),

        new ParticipantCommented(2, "Bob", RoomId)
            .AddIdentity(_counter ++)
            .AddMessage("Hi"),

        new ParticipantLeft(4, "Alice", RoomId)
            .AddIdentity(_counter ++),

        new ParticipantHighFived(3, "Kate", RoomId)
            .AddIdentity(_counter ++)
            .SetRecipient(2, "Bob"),
    };
}