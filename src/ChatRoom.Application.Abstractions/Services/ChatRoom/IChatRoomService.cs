using ChatRoom.Application.Abstractions.Model;

namespace ChatRoom.Application.Abstractions.Services.ChatRoom;

/// <summary>
/// Describe basic chat-room features.
/// It helps events sourcing.
/// </summary>
public interface IChatRoomService
{
    Task AddParticipantAsync(int participantId,  int roomId, CancellationToken cancellationToken = default);

    Task RemoveParticipantAsync(int participantId, int roomId, CancellationToken cancellationToken = default);

    Task LeaveACommentAsync(int roomId, int participantId, string comment, CancellationToken cancellationToken = default);

    Task<ParticipantDto> HighFiveAsync(int roomId, int fromParticipantId, int toParticipantId, CancellationToken cancellationToken = default);


    Task<List<ChatRoomDto>> GetChatRoomsAsync(CancellationToken cancellationToken = default);

    Task<ChatRoomDto?> GetChatRoomAsync(int roomId, CancellationToken cancellationToken);

    Task<List<ParticipantDto>> GetParticipantsAsync(CancellationToken cancellationToken = default);

    Task<ParticipantDto?> GetParticipantAsync(int participantId, CancellationToken cancellationToken);

    Task<ChatRoomParticipantsDto?> GetChatRoomWithParticipantsAsync(int roomId, CancellationToken cancellationToken);
}