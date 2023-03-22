using ChatRoom.Domain.Events.Enum;

namespace ChatRoom.Domain.Events;

public class ParticipantCommented : ChatEvent, IChatEventWithMessage
{
    public ParticipantCommented()
    {
    }

    public ParticipantCommented(int participantId, string participantName, int chatRoomId)
        :base(EventType.ParticipantCommented,  participantId, participantName, chatRoomId)
    {
        CreatedOn = DateTime.Now;
    }

    public override string ToEventString()
    {
        return $"{ParticipantName} comments {Message}";
    }

    public override string ToAggregateString(params string[] prms)
    {
        return $"{prms[0]} comments";
    }

    public override ParticipantCommented AddMessage(string? message)
    {
        if (!string.IsNullOrEmpty(message) && Type != EventType.ParticipantCommented)
            throw new InvalidOperationException(
                $"Only for {EventType.ParticipantCommented} is allowed to add the comment.");

        Message = message;

        return this;
    }
}