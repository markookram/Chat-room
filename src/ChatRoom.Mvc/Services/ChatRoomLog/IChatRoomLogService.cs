using ChatRoom.Mvc.Models.ChatRoomLog;

namespace ChatRoom.Mvc.Services.ChatRoomLog;

/// <summary>
/// Encapsulates application chat-room log service
/// </summary>
public interface IChatRoomLogService
{
    Task<ChatRoomLogVm> ReadLogAsync(ChatRoomLogRequest request, CancellationToken cancellationToken = default);
}