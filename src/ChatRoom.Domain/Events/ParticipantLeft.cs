using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantLeft : ChatEvent
{
    public ParticipantLeft()
    { }

    public ParticipantLeft(EventType type)
        :base(type){}

    public ParticipantLeft(int participantId, string participantName, int chatRoomId)
        :base(EventType.ParticipantLeft, participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{ParticipantName} leaves.";
    }

    public static string DescribeItself(params string[] prms)
    {
        if (!prms.Any() || !int.TryParse(prms[0], out int count)) return "left";
        return $"{count} left";

    }
}