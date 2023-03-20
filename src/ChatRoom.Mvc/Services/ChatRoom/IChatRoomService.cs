using ChatRoom.Application.Abstractions.Model;
using ChatRoom.Mvc.Models.ChatRoom;

namespace ChatRoom.Mvc.Services.ChatRoom;

/// <summary>
/// Encapsulates app service and defines delivery API's.
/// </summary>
public interface IChatRoomService
{
    Task AddParticipantAsync(int participantId, int roomId, CancellationToken cancellationToken = default);

    Task RemoveParticipantAsync(int participantId, int roomId, CancellationToken cancellationToken = default);

    Task LeaveACommentAsync(int roomId, int participantId, string comment, CancellationToken cancellationToken = default);

    Task<string> HighFiveAsync(int roomId, int fromParticipantId, int toParticipantId, CancellationToken cancellationToken = default);

    Task<(List<ChatRoomDto> rooms, List<ParticipantDto> participants)> GetChatRoomsAndParticipantsAsync(
        CancellationToken cancellationToken = default);

    Task<List<ParticipantDto>> GetParticipantsAsync(CancellationToken cancellationToken = default);

    Task<ChatRoomVm?> GetChatRoomAsync(int roomId, int participantId, CancellationToken cancellationToken = default);
}