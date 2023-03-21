namespace ChatRoom.Application.Abstractions.Extensions;

public static class TypeExtensions
{
    public static string ToFormatedString(this Tuple<DateTime, int, int> dt, string format)
    {
        var dtFormat = new DateTime(dt.Item1.Day, dt.Item1.Month, dt.Item1.Day, dt.Item2, dt.Item3, 0);

        return dtFormat.ToString(format);
    }
}