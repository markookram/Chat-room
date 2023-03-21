using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantEntered : ChatEvent
{
    public ParticipantEntered()
    {
    }

    public ParticipantEntered(EventType type)
        :base(type){}

    public ParticipantEntered(int participantId, string participantName, int chatRoomId)
    :base(EventType.ParticipantEntered, participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{ParticipantName} enters the room.";
    }

    public static string DescribeItself(params string[] prms)
    {
        if (!prms.Any() || !int.TryParse(prms[0], out int count)) return "person entered";
        var subj = count == 1 ? "person" : "people";
        return $"{count} {subj} entered";

    }
}