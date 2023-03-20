using ChatRoom.Application.Abstractions.Events.Enum;
using ChatRoom.Application.Abstractions.Queries.Params;
using ChatRoom.Application.Abstractions.Services.ChatRoom;
using ChatRoom.Mvc.Models.ChatRoomLog;
using ChatRoom.Mvc.Services.ChatRoomLog;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Mvc.Controllers;

/// <summary>
/// Chat-room events controller.
/// Entry point for the chat-room events functionality.
/// </summary>
public class ChatRoomLogController : BaseController
{
    private readonly ILogger<ChatRoomLogController> _logger;
    private readonly IChatRoomLogService _chatRoomLogService;
    private readonly IChatRoomService _chatRoomService;

    public ChatRoomLogController(ILogger<ChatRoomLogController> logger,
        IChatRoomLogService chatRoomLogService,
        IChatRoomService chatRoomService)
    {
        _logger = logger;
        _chatRoomLogService = chatRoomLogService;
        _chatRoomService = chatRoomService;
    }

    public async Task<IActionResult> Index(ChatRoomLogVm vm, CancellationToken cancellationToken)
    {
        var room = await _chatRoomService.GetChatRoomAsync(vm.Id, cancellationToken);

        var logVm = await _chatRoomLogService.ReadLogAsync(new ChatRoomLogRequest
        {
           Params = new DefaultQueryParams()
               .AddRoomId(vm.Id)
               .AddParticipantId(vm.ParticipantId),
           ChatRoomName = room?.Name ?? string.Empty
        }, cancellationToken);

        return View("Index", logVm);
    }


    public async Task<IActionResult> CheckLogs(int granularityId, int chatRoomId, int participantId, CancellationToken cancellationToken)
    {
        var logVm = await _chatRoomLogService.ReadLogAsync( new ChatRoomLogRequest
        {
            Params = new QueryParams((GranularityType)granularityId)
                .AddRoomId(chatRoomId)
                .AddParticipantId(participantId)
        }, cancellationToken);

        return PartialView("_LogResult", logVm.Log);
    }
}