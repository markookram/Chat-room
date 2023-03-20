namespace ChatRoom.Application.Abstractions.Infrastructure.Repositories;

/// <summary>
/// Designed for repositories resolution.
/// </summary>
public interface IChatRoomRepositoryFactory
{
    TRepo Repository<TRepo>() where TRepo : IRepository;
}