using System.Security.Cryptography.X509Certificates;
using ChatRoom.Application.Abstractions.Infrastructure.Persistence;
using ChatRoom.Persistence.DataStore.SqlLite.Persistence;
using ChatRoom.Persistence.DataStore.SqlLite.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatRoom.Persistence.DataStore.SqlLite.Extensions;

public static class Startup
{
    public static IServiceCollection ConfigureChatRoomSqlLiteDataStore(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<SqlLiteChatRoomDbContext>(op =>
            {
                op.UseSqlite(configuration.GetConnectionString("ChatRoomDatabase"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            })
            .AddDbContext<SqlLiteChatRoomLogDataContext>(op =>
            {
                op.UseSqlite(configuration.GetConnectionString("ChatRoomLogDatabase"));
            })
            .AddScoped<IDataStore, SqlLiteDataStore>()
            .AddScoped<IDataStore, SqlLiteDataStore>()
            .AddScoped<IEventsDataStore, SqlLiteEventsDataStore>();
    }
}