using Autofac;
using ChatRoom.Application.Abstractions.Events.Enum;
using ChatRoom.Application.Abstractions.Queries;
using ChatRoom.Application.Abstractions.Services.ChatRoom;
using ChatRoom.Application.Abstractions.Services.ChatRoomLog;
using ChatRoom.Application.Services.ChatRoom;
using ChatRoom.Application.Services.ChatRoomLog;
using ChatRoom.Application.Services.ChatRoomLog.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.Application.Infrastructure.Extensions;

public static class Startup
{
    public static void ConfigureChatRoomAppWithBuilder(this ContainerBuilder builder)
    {
        builder.RegisterType<QueryAll>()
            .As<IQuery<IQueryParams, Task<StringQueryResult>>>().Keyed<IQuery<IQueryParams, Task<StringQueryResult>>>(GranularityType.All);

        builder.RegisterType<QueryByHour>()
            .As<IQuery<IQueryParams, Task<StringQueryResult>>>().Keyed<IQuery<IQueryParams, Task<StringQueryResult>>>(GranularityType.Hourly);

        builder.RegisterType<QueryByMinute>()
            .As<IQuery<IQueryParams, Task<StringQueryResult>>>().Keyed<IQuery<IQueryParams, Task<StringQueryResult>>>(GranularityType.Minute);

        builder.RegisterType<QueryAggregateByHour>()
            .As<IQuery<IQueryParams, Task<StringQueryResult>>>().Keyed<IQuery<IQueryParams, Task<StringQueryResult>>>(GranularityType.AggregatedByHour);

        builder.RegisterType<QueryAggregateByMinute>()
            .As<IQuery<IQueryParams, Task<StringQueryResult>>>().Keyed<IQuery<IQueryParams, Task<StringQueryResult>>>(GranularityType.AggregatedByMinute);

        builder.Register<Func<GranularityType, IQuery<IQueryParams, Task<StringQueryResult>>>>(c =>
        {
            var componentContext = c.Resolve<IComponentContext>();
            return granularityType =>
            {
                var query = componentContext.ResolveKeyed<IQuery<IQueryParams, Task<StringQueryResult>>>(granularityType);

                return query;
            };
        });
    }

    public static IServiceCollection ConfigureChatRoomApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddChatRoomServices()
            .AddChatRoomAutoMapper();
    }

    private static IServiceCollection AddChatRoomAutoMapper(this IServiceCollection services)
    {
        return services
            .AddAutoMapper(typeof(Startup).Assembly);
    }

    private static IServiceCollection AddChatRoomServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IChatRoomService, ChatRoomService>()
            .AddScoped<IChatRoomLogService, ChatRoomLogService>();
    }

}