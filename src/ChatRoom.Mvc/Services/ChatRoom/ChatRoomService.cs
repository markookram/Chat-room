using AutoMapper;
using ChatRoom.Application.Abstractions.Model;
using ChatRoom.Mvc.Models.ChatRoom;

namespace ChatRoom.Mvc.Services.ChatRoom;

/// <summary>
/// Implements chat-room service interface.
/// </summary>
public class ChatRoomService : IChatRoomService
{
    private readonly IMapper _mapper;
    private readonly Application.Abstractions.Services.ChatRoom.IChatRoomService _service;

    public ChatRoomService(IMapper mapper, Application.Abstractions.Services.ChatRoom.IChatRoomService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task AddParticipantAsync(int participantId, int roomId, CancellationToken cancellationToken = default)
    {
        await _service.AddParticipantAsync(participantId, roomId, cancellationToken);
    }

    public async Task RemoveParticipantAsync(int participantId, int roomId, CancellationToken cancellationToken = default)
    {
        await _service.RemoveParticipantAsync(participantId, roomId, cancellationToken);
    }

    public async Task LeaveACommentAsync(int roomId, int participantId, string comment, CancellationToken cancellationToken = default)
    {
        await _service.LeaveACommentAsync(roomId, participantId, comment, cancellationToken);
    }

    public async Task<string> HighFiveAsync(int roomId, int fromParticipantId, int toParticipantId,
        CancellationToken cancellationToken = default)
    {
        return (await _service.HighFiveAsync(roomId, fromParticipantId, toParticipantId, cancellationToken)).Name;
    }

    public async Task<(List<ChatRoomDto> rooms, List<ParticipantDto> participants)> GetChatRoomsAndParticipantsAsync(CancellationToken cancellationToken = default)
    {
        var rooms = await _service.GetChatRoomsAsync(cancellationToken);

        var participants = await GetParticipantsAsync(cancellationToken);

        return new(rooms, participants);
    }

    public async Task<List<ParticipantDto>> GetParticipantsAsync(CancellationToken cancellationToken = default)
    {
        return (await _service.GetParticipantsAsync(cancellationToken))
            .Where(p => p.ChatRoomId == default).ToList();
    }

    public async Task<ChatRoomVm?> GetChatRoomAsync(int roomId, int participantId, CancellationToken cancellationToken = default)
    {
        var room = await _service.GetChatRoomWithParticipantsAsync(roomId, cancellationToken);
        var participant = await _service.GetParticipantAsync(participantId, cancellationToken);

        if (room == default! || participant == default)
            return default;

        var roomVm = _mapper.Map<ChatRoomVm>(room);
        roomVm.ParticipantName = participant.Name;
        roomVm.ParticipantId = participantId;

        foreach (var participantVm in roomVm.Participants?.Where(participantVm => participantVm.Id == participantId) ?? new List<ParticipantVm>())
        {
            participantVm.Name = $"Me ({participantVm.Name})";
        }

        return roomVm;
    }
}