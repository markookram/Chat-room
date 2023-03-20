using ChatRoom.Application.Abstractions.Queries;

namespace ChatRoom.Mvc.Models.ChatRoomLog;

public class ChatRoomLogRequest
{
    public IQueryParams Params { get; set; }

    public string? ChatRoomName { get; set; }
}