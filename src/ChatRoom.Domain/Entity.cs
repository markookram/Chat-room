namespace ChatRoom.Domain;

/// <summary>
/// Base aggregate root.
/// Abstract class, defines for inheritance.
/// </summary>
public abstract class Entity : IEntity, IAuditableEntity
{
    public int Id { get; protected set; }

    public DateTime CreatedOn { get; protected set; }

    //Just for seeding
    public IAuditableEntity TweakDateOfBirth(DateTime fiction)
    {
        CreatedOn = fiction;

        return this;
    }

    public virtual IEntity AddIdentity(int id)
    {
        Id = id;
        return this;
    }
}