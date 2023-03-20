using ChatRoom.Mvc.Models.ChatRoomLog;

namespace ChatRoom.Mvc.Services.ChatRoomLog;

/// <summary>
/// Implements IChatRoomLogService
/// </summary>
public class ChatRoomLogService :IChatRoomLogService
{
    private readonly Application.Abstractions.Services.ChatRoomLog.IChatRoomLogService _chatRoomLogService;
    private readonly ILogger<ChatRoomLogService> _logger;

    public ChatRoomLogService(Application.Abstractions.Services.ChatRoomLog.IChatRoomLogService chatRoomLogService, ILogger<ChatRoomLogService> logger)
    {
        _chatRoomLogService = chatRoomLogService;
        _logger = logger;
    }

    public async Task<ChatRoomLogVm> ReadLogAsync(ChatRoomLogRequest request, CancellationToken cancellationToken = default)
    {
        var log = await _chatRoomLogService.ReadLogAsync(request.Params, cancellationToken);

        return new ChatRoomLogVm
        {
            Id = request.Params.RoomId,
            ParticipantId = request.Params.ParticipantId,
            Name = request.ChatRoomName ?? string.Empty,
            Log = log.Result
        };
    }
}