using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Extensions;

public static class ChatEventExtensions
{
    public static string ToDescription(this Type type, EventType eventType, int count)
    {
        var method = type.GetMethod("Describe");
        if (method == default)
            return string.Empty;

        var instance = Activator.CreateInstance(type, null);
        if (instance == default)
            return string.Empty;

        return eventType switch
        {
            EventType.ParticipantEntered
                or EventType.ParticipantLeft
                or EventType.PariticipantHighFived
                or EventType.ParticipantCommented => (string)method.Invoke(instance,
                    new object[] { new[] { count.ToString() } })!,
            _ => string.Empty,
        };
    }

}