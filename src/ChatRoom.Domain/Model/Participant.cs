namespace ChatRoom.Domain.Model;

/// <summary>
/// Participant
/// </summary>
public class Participant : Entity, IChatRoomEntity
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
        ChatRoomId = default;
        ChatRoom = null;

        return this;
    }

    public override Participant AddIdentity(int id)
    {
        Id = id;
        return this;
    }
}