namespace ChatRoom.Application.Abstractions.Model;

public class ChatRoomParticipantsDto
{

    public int Id { get; set; }

#pragma warning disable CS8618
    public string Name { get; set; }
#pragma warning restore CS8618

    public List<ParticipantDto>? Participants { get; set; }
}