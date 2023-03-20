using System.Collections;
using ChatRoom.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Persistence.DataStore.SqlLite.Extensions;

public static class Efxtensions
{
    public static void SetNavigationsState<T>(this EntityEntry<T> entry, DbContext context, EntityState state) where T : class, IAggregateRoot
    {
        foreach (var navigation in entry.Navigations)
        {
            if (navigation.CurrentValue is not IEnumerable nav)
                continue;
            foreach (var val in nav)
            {
                context.Entry(val).State = state;
            }
        }
    }

    public static void SetState<T>(this EntityEntry<T> entry, DbContext context, EntityState state) where T : class, IAggregateRoot
    {
        entry.State = state;

        entry.SetNavigationsState(context, state);
    }
}