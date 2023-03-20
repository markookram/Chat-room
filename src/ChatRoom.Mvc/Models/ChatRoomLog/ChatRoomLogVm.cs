using System.ComponentModel.DataAnnotations;
#pragma warning disable CS8618

namespace ChatRoom.Mvc.Models.ChatRoomLog;

public class ChatRoomLogVm
{
    [Display(Name = "Granularity")]
    public int GranularityId { get; set; }

    public int Id { get; set; }

    public int ParticipantId { get; set; }

    public string Name { get; set; }

    public string Log { get; set; }
}