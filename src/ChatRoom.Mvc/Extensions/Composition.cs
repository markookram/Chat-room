using Autofac;
using ChatRoom.Composition;

namespace ChatRoom.Mvc.Extensions;

/// <summary>
/// Encapsulates Composition root responsibility.
/// </summary>
public class Composition
{
    public static bool WithSqlDb = false;

    public IConfiguration Configuration { get; }

    public Composition(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.ConfigureCustomContainer();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureChatRoomMvc(Configuration);
        services.ConfigureChatRoomApp(Configuration, WithSqlDb);

    }


}