namespace ChatRoom.Domain.Model;

/// <summary>
/// Chat room
/// </summary>
public class ChatRoom : Entity, IChatRoomEntity
{
    public ChatRoom(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }

    private readonly List<Participant> _participants = new();
    public IReadOnlyCollection<Participant> Participants => _participants;

    public void AddParticipant(int participantId, string participantName)
    {
        if (_participants.All(p => p.Id != participantId))
            _participants.Add(new Participant(participantName)
                                              .AddIdentity(participantId)
                                              .AddToTheRoom(Id));
    }

    public void AddParticipant(Participant participant)
    {
        if (_participants.All(p => p.Id != participant.Id))
            _participants.Add(participant.AddToTheRoom(Id));
    }

    public void AddRangeParticipants(IList<Participant> participants)
    {
        foreach (var participant in participants)
        {
            AddParticipant(participant.Id, participant.Name);
        }
    }

    public void RemoveParticipant(int participantId)
    {
        var participant = _participants.SingleOrDefault(p => p.Id == participantId);
        if (participant == null)
            return;
        _participants.Remove(participant.RemoveFromTheRoom());
    }

    public override ChatRoom AddIdentity(int id)
    {
        Id = id;
        return this;
    }
}