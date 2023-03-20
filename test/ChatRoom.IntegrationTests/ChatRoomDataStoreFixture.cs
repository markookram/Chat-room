using ChatRoom.Domain.Model;

namespace ChatRoom.IntegrationTests;

public class ChatRoomDataStoreFixture
{
    public readonly IList<Domain.Model.ChatRoom> Rooms = new List<Domain.Model.ChatRoom>(2)
    {
        new Domain.Model.ChatRoom("Sql server")
            .AddIdentity(1),
        new Domain.Model.ChatRoom("Oracle")
            .AddIdentity(2)
    };

    public readonly IList<Participant> Participants = new List<Participant>(6)
    {
        new Participant("Mike on Sql")
            .AddIdentity(1),
        new Participant("Bob on Sql")
            .AddIdentity(2),
        new Participant("Kate on Sql")
            .AddIdentity(3),
        new Participant("Alice on Sql")
            .AddIdentity(4)
    };
}