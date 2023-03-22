namespace ChatRoom.Domain.Events;

public interface IChatEventWithMessage
{
    string? Message { get; }

    ChatEvent AddMessage(string? message);
}