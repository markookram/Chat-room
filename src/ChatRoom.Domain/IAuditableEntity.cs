namespace ChatRoom.Domain;

/// <summary>
/// Defines an auditing options
/// </summary>
public interface IAuditableEntity
{
    DateTime CreatedOn { get; }

    IAuditableEntity TweakDateOfBirth(DateTime fiction);
}