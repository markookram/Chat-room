namespace ChatRoom.Application.Abstractions.Model;

public class ParticipantDto
{
    public int Id { get; set; }

    public int? ChatRoomId { get; set; }

#pragma warning disable CS8618
    public string Name { get; set; }
#pragma warning restore CS8618
}