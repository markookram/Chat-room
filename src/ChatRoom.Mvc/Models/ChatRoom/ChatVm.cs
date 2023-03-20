namespace ChatRoom.Mvc.Models.ChatRoom;

public class ChatVm
{
    public int RoomId { get; set; }

    public int ParticipantId { get; set; }

    public int ToParticipantId { get; set; }

#pragma warning disable CS8618
    public string Message { get; set; }
#pragma warning restore CS8618
}