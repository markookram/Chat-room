using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Mvc.Models.ChatRoom;

public class ChatRoomLobyVm
{
    [Display(Name = "Select a room you want to get in")]
    [Required(ErrorMessage = "Where do you think to get in?")]
    public int ChatRoomId { get; set; }

    [Display(Name = "Select who you want to be")]
    [Required(ErrorMessage = "You can't get in without name.")]
    public int ParticipantId { get; set; }
}