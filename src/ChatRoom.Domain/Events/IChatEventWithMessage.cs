namespace ChatRoom.Domain.Events;

public interface IChatEventWithMessage : IChatEvent
{
    string? Message { get; }
}