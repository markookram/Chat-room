namespace ChatRoom.Domain;

/// <summary>
/// Defines aggregate root
/// </summary>
public interface IEntity
{
    int Id { get; }

    IEntity AddIdentity(int id);
}