
using ChatRoom.Mvc.Models.ChatRoom;
using ChatRoom.Mvc.Services.ChatRoom;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Mvc.Controllers
{
    /// <summary>
    /// Chat-room controller.
    /// Entry point for the chat-room functionality.
    /// </summary>
    public class ChatRoomController : BaseController
    {
        private readonly ILogger<ChatRoomController> _logger;
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomController(ILogger<ChatRoomController> logger, IChatRoomService chatRoomService)
        {
            _logger = logger;
            _chatRoomService = chatRoomService;
            _chatRoomService = chatRoomService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var data = await _chatRoomService.GetChatRoomsAndParticipantsAsync(cancellationToken);

            ViewBag.Rooms = data.rooms;
            ViewBag.Participants = data.participants;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetIntoTheRoom(ChatRoomLobyVm vm, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                ViewBag.RoomId = vm.ChatRoomId;
                ViewBag.ParticipantId = vm.ParticipantId;

                return RedirectToAction("WelcomeToTheRoom",
                    new { roomId = vm.ChatRoomId, participantId = vm.ParticipantId });
            }

            var data = await _chatRoomService.GetChatRoomsAndParticipantsAsync(cancellationToken);

            ViewBag.Rooms = data.rooms;
            ViewBag.Participants = data.participants;

            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> WelcomeToTheRoom(int roomId, int participantId, CancellationToken cancellationToken)
        {
            await _chatRoomService.AddParticipantAsync(participantId, roomId, cancellationToken);

            var roomVm = await _chatRoomService.GetChatRoomAsync(roomId, participantId, cancellationToken);
            if (roomVm == default)
                return BadRequest("Room or participant doesn't exist.");

            ViewBag.Participants = await _chatRoomService.GetParticipantsAsync(cancellationToken);

            return View("Room", roomVm);
        }

        [HttpPost]
        public async Task<IActionResult> LeaveTheRoom(ChatRoomVm vm, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _chatRoomService.RemoveParticipantAsync(vm.ParticipantId, vm.Id, cancellationToken);

                return RedirectToAction("Index");
            }

            var data = await _chatRoomService.GetChatRoomsAndParticipantsAsync(cancellationToken);

            ViewBag.Rooms = data.rooms;
            ViewBag.Participants = data.participants;

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(int roomId, int participantId, int? toParticipantId = null, string? message = null, CancellationToken cancellationToken = default)
        {
            var resultMsg = "Error. Data are wrong";

            if (roomId == 0 || participantId == 0 || toParticipantId == default && string.IsNullOrEmpty(message))
            {
                return WithStatusCode(500, () => new JsonResult(resultMsg));
            }
            if (toParticipantId != default)
            {
                var sentTo = await _chatRoomService.HighFiveAsync(roomId, participantId, toParticipantId!.Value, cancellationToken);
                resultMsg = $"High-five successfully sent to {sentTo}.";
            }
            else
            {
                await _chatRoomService.LeaveACommentAsync(roomId, participantId, message!, cancellationToken);
                resultMsg = $"{message} sent.";
            }

            return WithStatusCode(200, () => new JsonResult(resultMsg));
        }

    }
}