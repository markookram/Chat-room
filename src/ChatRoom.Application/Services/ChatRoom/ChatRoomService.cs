using ChatRoom.Application.Abstractions.Model;
using ChatRoom.Domain.Model;
using ChatRoom.Domain.Events;
using AutoMapper;
using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Application.Abstractions.Services.ChatRoom;
using ChatRoom.Application.Abstractions.Services.ChatRoomLog;
using Microsoft.Extensions.Logging;

namespace ChatRoom.Application.Services.ChatRoom
{
    /// <summary>
    /// Implements basic chatRoom features
    /// Used just as a helper for events sourcing
    /// </summary>
    public class ChatRoomService : IChatRoomService
    {
        private readonly ILogger<ChatRoomService> _logger;
        private IRepository<Domain.Model.ChatRoom> _chatRoomRepository;
        private IAggregateRootRepository<Participant> _participantRepository;
        private readonly IChatRoomRepositoryFactory _chatRoomRepositoryFactory;
        private readonly IChatRoomLogService _chatLogService;
        private readonly IMapper _mapper;

        public ChatRoomService(ILogger<ChatRoomService> logger,
            IChatRoomRepositoryFactory chatRoomRepositoryFactory,
            IChatRoomLogService chatLogService,
            IMapper mapper)
        {
            _logger = logger;
            _chatRoomRepositoryFactory = chatRoomRepositoryFactory;
            _chatRoomRepository = _chatRoomRepositoryFactory.Repository<IRepository<Domain.Model.ChatRoom>>();
            _participantRepository = _chatRoomRepositoryFactory.Repository<IAggregateRootRepository<Participant>>();
            _chatLogService = chatLogService;
            _mapper = mapper;
        }

        public async Task AddParticipantAsync(int participantId, int roomId, CancellationToken cancellationToken = default)
        {
            var participant = await _participantRepository.GetAsync(participantId, cancellationToken);

            participant?.AddToTheRoom(roomId);

            await _participantRepository.AddOrUpdateAsync(participant!, cancellationToken);
            await _chatLogService.LogEventAsync(new ParticipantEntered(participantId, participant!.Name, roomId), cancellationToken);
        }

        public async Task RemoveParticipantAsync(int participantId, int roomId, CancellationToken cancellationToken = default)
        {
            var participant = await _participantRepository.GetAsync(participantId, cancellationToken);

            participant?.RemoveFromTheRoom();

            await _participantRepository.AddOrUpdateAsync(participant!, cancellationToken);
            await _chatLogService.LogEventAsync(new ParticipantLeft(participantId, participant!.Name, roomId), cancellationToken);
        }

        public async Task LeaveACommentAsync(int roomId, int participantId, string comment, CancellationToken cancellationToken = default)
        {
            var participant = await _participantRepository.GetAsync(participantId, cancellationToken);

            await _chatLogService.LogEventAsync(new ParticipantCommented(participantId, participant!.Name, roomId).AddMessage(comment), cancellationToken);
        }

        public async Task<ParticipantDto> HighFiveAsync(int roomId, int fromParticipantId, int toParticipantId, CancellationToken cancellationToken = default)
        {
            var toParticipant = await _participantRepository.GetAsync(toParticipantId, cancellationToken);
            var fromParticipant = await _participantRepository.GetAsync(fromParticipantId, cancellationToken);

            await _chatLogService.LogEventAsync(
                new ParticipantHighFived(fromParticipantId, fromParticipant!.Name, roomId)
                    .SetRecipient(toParticipantId, toParticipant!.Name), cancellationToken);

            return _mapper.Map<ParticipantDto>(toParticipant);;
        }

        public async Task<List<ChatRoomDto>> GetChatRoomsAsync(CancellationToken cancellationToken = default)
        {
           var rooms = await _chatRoomRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<List<ChatRoomDto>>(rooms);
        }

        public async Task<ChatRoomDto?> GetChatRoomAsync(int roomId, CancellationToken cancellationToken)
        {
            var chatRoom = (await GetChatRoomsAsync(cancellationToken)).FirstOrDefault(p => p.Id == roomId);

            return chatRoom;
        }

        public async Task<ChatRoomParticipantsDto?> GetChatRoomWithParticipantsAsync(int roomId, CancellationToken cancellationToken)
        {
            var chatRoom = (await _chatRoomRepository.GetAsync(roomId, cancellationToken));

            return _mapper.Map<ChatRoomParticipantsDto?>(chatRoom);
        }

        public async Task<ParticipantDto?> GetParticipantAsync(int participantId, CancellationToken cancellationToken)
        {
            return (await GetParticipantsAsync(cancellationToken)).FirstOrDefault(p => p.Id == participantId);
        }

        public async Task<List<ParticipantDto>> GetParticipantsAsync(CancellationToken cancellationToken = default)
        {
            var participants = await _participantRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ParticipantDto>>(participants);
        }
    }
}