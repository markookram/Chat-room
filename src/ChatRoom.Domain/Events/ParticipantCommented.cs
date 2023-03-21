using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantCommented : ChatEvent
{
    public ParticipantCommented()
    {
    }

    public ParticipantCommented(int participantId, string participantName, int chatRoomId)
    :base(EventType.ParticipantCommented,  participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{ParticipantName} comments: {Message}.";
    }

    public static string DescribeItself(params string[] prms)
    {
        if (!prms.Any() || !int.TryParse(prms[0], out int count)) return "comments";
        return $"{count} comments";

    }
}