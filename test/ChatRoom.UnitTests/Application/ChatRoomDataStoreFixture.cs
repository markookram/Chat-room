using ChatRoom.Domain.Model;

namespace ChatRoom.UnitTests.Application;

public class ChatRoomDataStoreFixture
{
    public readonly IList<Domain.Model.ChatRoom> Rooms = new List<Domain.Model.ChatRoom>(2)
    {
        new Domain.Model.ChatRoom("IT")
            .AddIdentity(1),
        new Domain.Model.ChatRoom("Sports")
            .AddIdentity(2)
    };

    public readonly IList<Participant> Participants = new List<Participant>(6)
    {
        new Participant("Mike")
            .AddIdentity(1),
        new Participant("Bob")
            .AddIdentity(2),
        new Participant("Kate")
            .AddIdentity(3),
        new Participant("Alice")
            .AddIdentity(4)
    };
}