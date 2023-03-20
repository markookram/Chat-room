using ChatRoom.Mvc.Services.ChatRoom;
using ChatRoom.Mvc.Services.ChatRoomLog;

namespace ChatRoom.Mvc.Extensions;

public static class StartupExtensions
{
    public static IServiceCollection ConfigureChatRoomMvc(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddAutoMapper(typeof(StartupExtensions).Assembly)
            .AddScoped<IChatRoomService, ChatRoomService>()
            .AddScoped<IChatRoomLogService, ChatRoomLogService>();

    }


}