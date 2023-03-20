using Autofac;
using ChatRoom.Application.Infrastructure.Extensions;
using ChatRoom.Domain.Extensions;
using ChatRoom.Persistence.DataStore.InMemory.Extensions;
using ChatRoom.Persistence.DataStore.SqlLite;
using ChatRoom.Persistence.DataStore.SqlLite.Extensions;
using ChatRoom.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.Composition;

/// <summary>
/// Chat-room app Composition Root.
/// 'Wires up' all app projects.
/// </summary>
public static class Startup
{
    public static void ConfigureCustomContainer(this ContainerBuilder builder)
    {
        builder.ConfigureChatRoomAppWithBuilder();
    }

    public static IServiceCollection ConfigureChatRoomApp(this IServiceCollection services, IConfiguration configuration, bool withSqlDb = false)
    {
        services
            .ConfigureChatRoomApplication(configuration)
            .ConfigureChatRoomDomain(configuration)
            .ConfigureChatRoomPersistence(configuration);

        if (withSqlDb)
        {
            services.AddOptions()
                .Configure<ConnectionStringsOptions>(configuration.GetSection("ConnectionStrings"));;
            services.ConfigureChatRoomSqlLiteDataStore(configuration);
        }
        else
            services.ConfigureChatRoomInMemoryDataStore(configuration);

        return services;
    }



}