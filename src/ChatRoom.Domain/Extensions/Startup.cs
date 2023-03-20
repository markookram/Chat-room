using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.Domain.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureChatRoomDomain(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}