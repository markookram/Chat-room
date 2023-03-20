using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Persistence.DataStore.InMemory.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.Persistence.DataStore.InMemory.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureChatRoomInMemoryDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IDataStore, InMemoryDataStore>()
            .AddSingleton<IEventsDataStore, InMemoryEventsDataStore>();
    }
}