using ChatRoom.Domain.Events;
namespace ChatRoom.Domain.Extensions;

public static class ChatEventExtensions
{
    public static string AggregateString(this ChatEvent @event, params string[] prms)
    {
        var type = @event.GetType();
        var method = type.GetMethod("ToAggregateStringFormat");
        if (method == default)
            return string.Empty;

        object? instance;
        instance = Activator.CreateInstance(type);

        if(instance == null)
            return string.Empty;

        return (string) method.Invoke(instance, new object[] {prms.ToArray()})!;
    }
}

