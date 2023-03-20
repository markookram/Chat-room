namespace ChatRoom.Domain.Model;

/// <summary>
/// Participant
/// </summary>
public class Participant : Entity, IAggregateRoot
{
    public Participant(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public int? ChatRoomId { get; private set; }
    public ChatRoom? ChatRoom { get; private set; }


    public Participant AddToTheRoom(int? chatRoomId)
    {
        ChatRoomId = chatRoomId;

        return this;
    }

    public Participant RemoveFromTheRoom()
    {
        if(ChatRoomId == default)
            return this;

        ChatRoomId = default;
        ChatRoom?.RemoveParticipant(Id);

        return this;
    }

    public override Participant AddIdentity(int id)
    {
        Id = id;
        return this;
    }
}