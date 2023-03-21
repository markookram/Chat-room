using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Extensions;

public static class ChatEventExtensions
{
    public static string ToDescription(this Type type, EventType eventType, params string[] prms)
    {
        var method = type.GetMethod("DescribeItself");
        if (method == default)
            return string.Empty;

        var instance = Activator.CreateInstance(type, eventType) ??  Activator.CreateInstance(type);
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