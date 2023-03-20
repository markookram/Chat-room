using ChatRoom.Domain;

namespace ChatRoom.Persistence.SqlLite.Extensions;

public static class MigrationExtension
{
    public static T CompleteAuditing<T>(this T entity)
        where T : Entity
    {
        entity.TweakDateOfBirth(DateTime.Now);

        return entity;
    }
}