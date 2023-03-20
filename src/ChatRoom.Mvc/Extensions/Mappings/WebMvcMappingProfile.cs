using AutoMapper;
using ChatRoom.Application.Abstractions.Model;
using ChatRoom.Mvc.Models.ChatRoom;

namespace ChatRoom.Mvc.Extensions.Mappings;

public class WebMvcMappingProfile : Profile
{
    public WebMvcMappingProfile()
    {
        CreateMap<ChatRoomParticipantsDto, ChatRoomVm>();

        CreateMap<ParticipantDto, ParticipantVm>();
    }
}