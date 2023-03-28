namespace ChatRoom.Domain.Events;

public interface IChatEventMessage
{
    string? Message { get; }

    ChatEvent AddMessage(string? message);
}