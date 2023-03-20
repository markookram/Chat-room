using AutoMapper;
using ChatRoom.Application.Abstractions.Model;

namespace ChatRoom.Application.Infrastructure.Mappings;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Domain.Model.ChatRoom, ChatRoomDto>();

        CreateMap<Domain.Model.Participant, ParticipantDto>();

        CreateMap<Domain.Model.ChatRoom, ChatRoomParticipantsDto>();
    }
}