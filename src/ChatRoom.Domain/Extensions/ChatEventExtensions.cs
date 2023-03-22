using ChatRoom.Domain.Events;
using ChatRoom.Domain.Events.Enum;
using static System.Int32;

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

        return (string) method.Invoke(instance, new object[] {prms.ToArray()})!;
    }

    public static string ToEventString(this EventType type, params object?[] prms)
    {
        return type switch
        {
            EventType.ParticipantEntered => string.Format(ParticipantEntered.StringFormat, prms),
            EventType.ParticipantLeft => string.Format(ParticipantLeft.StringFormat, prms),
            EventType.ParticipantCommented => string.Format(ParticipantCommented.StringFormat, prms),
            EventType.PariticipantHighFived => string.Format(ParticipantHighFived.StringFormat, prms),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static string ToAggegateEventString(this EventType type, params string?[] prms)
    {
        TryParse(prms[0], out int count);

        return type switch
        {
            EventType.ParticipantEntered => string.Format(ParticipantEntered.AggregateStringFormat, prms[0],
                count == 1 ? "person" : "people"),
            EventType.ParticipantLeft => string.Format(ParticipantLeft.AggregateStringFormat, prms[0]),
            EventType.ParticipantCommented => string.Format(ParticipantCommented.AggregateStringFormat, prms[0]),
            EventType.PariticipantHighFived => string.Format(ParticipantHighFived.AggregateStringFormat, prms[0],
                count == 1 ? "person" : "people"),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

}

