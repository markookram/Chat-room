using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantEntered : ChatEvent
{
    public ParticipantEntered()
    {
    }

    public ParticipantEntered(int participantId, string participantName, int chatRoomId)
    :base(EventType.ParticipantEntered, participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public override string ToEventString()
    {
        return $"{ParticipantName} enters the room";
    }

    public override string ToAggregateString(params string[] prms)
    {
        return $"{prms[0]} {prms[1]} entered";
    }
}