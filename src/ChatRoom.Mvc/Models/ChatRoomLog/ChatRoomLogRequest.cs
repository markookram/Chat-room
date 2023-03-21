using ChatRoom.Application.Abstractions.Queries;
#pragma warning disable CS8618

namespace ChatRoom.Mvc.Models.ChatRoomLog;

public class ChatRoomLogRequest
{
    public IQueryParams Params { get; set; }

    public string? ChatRoomName { get; set; }
}