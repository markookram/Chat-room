using ChatRoom.Application.Abstractions.Events.Enum;

namespace ChatRoom.Application.Abstractions.Queries.Params;

public class DefaultQueryParams : IQueryParams
{
    public virtual GranularityType GranularityType => GranularityType.All;

    public int RoomId { get; set; }

    public int ParticipantId { get; set; }

    public DefaultQueryParams AddRoomId(int roomId)
    {
        RoomId = roomId;

        return this;
    }

    public DefaultQueryParams AddParticipantId(int participantId)
    {
        ParticipantId = participantId;

        return this;
    }

}