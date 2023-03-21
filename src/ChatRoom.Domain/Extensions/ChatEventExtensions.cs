using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Extensions;

public static class ChatEventExtensions
{
    public static string ToAggregateString(this Type type, EventType eventType, params string[] prms)
    {
        var method = type.GetMethod("AggregateString");
        if (method == default)
            return string.Empty;

        object? instance;
        try
        {
            instance = Activator.CreateInstance(type, eventType) ??  Activator.CreateInstance(type);
        }
#pragma warning disable CS0168
        catch (Exception e)
#pragma warning restore CS0168
        {
            instance = Activator.CreateInstance(type);
        }

        if(instance == null)
            return string.Empty;

        return eventType switch
        {
            EventType.ParticipantEntered
                or EventType.ParticipantLeft
                or EventType.PariticipantHighFived
                or EventType.ParticipantCommented => (string)method.Invoke(instance,
                    new object[] { prms.ToArray() })!,
            _ => string.Empty,
        };
    }

}