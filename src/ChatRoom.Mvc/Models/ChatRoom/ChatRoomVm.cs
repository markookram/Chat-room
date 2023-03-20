namespace ChatRoom.Mvc.Models.ChatRoom;

public class ChatRoomVm
{
    public int Id { get; set; }

#pragma warning disable CS8618
    public string Name { get; set; }
#pragma warning restore CS8618

    public int ParticipantId { get; set; }

#pragma warning disable CS8618
    public string ParticipantName { get; set; }
#pragma warning restore CS8618

    public List<ParticipantVm>? Participants { get; set; }
}