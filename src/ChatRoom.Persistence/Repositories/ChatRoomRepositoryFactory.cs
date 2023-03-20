using ChatRoom.Application.Abstractions.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace ChatRoom.Persistence.Repositories;

public class ChatRoomRepositoryFactory : IChatRoomRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ChatRoomRepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TRepo Repository<TRepo>() where TRepo : IRepository
    {
        TRepo service = _serviceProvider.GetRequiredService<TRepo>();
        return service;
    }
}