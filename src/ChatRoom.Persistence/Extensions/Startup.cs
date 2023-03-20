using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using ChatRoom.Domain.Events;
using ChatRoom.Domain.Model;
using ChatRoom.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.Persistence.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureChatRoomPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IChatRoomRepositoryFactory, ChatRoomRepositoryFactory>()
            .AddScoped(typeof(IChatRoomLogRepository<ChatEvent>), typeof(ChatRoomLogRepository))
            .AddScoped(typeof(IRepository<Domain.Model.ChatRoom>), typeof(ChatRoomRepository))
            .AddScoped(typeof(IAggregateRootRepository<Participant>), typeof(ParticipantRepository));
    }
}