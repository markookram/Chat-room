using ChatRoom.Domain.Events;
using ChatRoom.Domain.Events.Enum;


namespace ChatRoom.UnitTests.Application;

public class ChatRoomEventLogDataStoreFixture
{
    public const int RoomId = 2;
    private static int _counter = 1;

    public readonly IList<ChatEvent> ChatEvents = new List<ChatEvent>(6)
    {
        new ChatEvent(EventType.ParticipantEntered, 1, "Mike", RoomId)
            .AddIdentity(_counter ++),

        new ChatEvent(EventType.ParticipantCommented, 2, "Bob", RoomId)
            .AddIdentity(_counter ++)
            .AddMessage("Hi"),

        new ChatEvent(EventType.ParticipantLeft, 4, "Alice", RoomId)
            .AddIdentity(_counter ++),

        new ChatEvent(EventType.PariticipantHighFived, 3, "Kate", RoomId)
            .AddIdentity(_counter ++)
            .SetRecipient(2, "Bob"),
    };

    public void ModifyEventsBirthdays(DateTime dateTime)
    {
        for (int i = 0; i < ChatEvents.Count; i++)
        {
            var @event = ChatEvents[i];
            @event.TweakDateOfBirth(dateTime.AddHours(i).AddMinutes(i));
        }
    }

}