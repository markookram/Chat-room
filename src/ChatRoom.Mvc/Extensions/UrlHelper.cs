using ChatRoom.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Mvc.Extensions;

public static class UrlHelper
{
    public static string ChatRoomLog(this IUrlHelper url)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return url.Action(nameof(ChatRoomLogController.CheckLogs),
            ControllerName<ChatRoomLogController>());
#pragma warning restore CS8603 // Possible null reference return.
    }

    public static string SendMessage(this IUrlHelper url)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return url.Action(nameof(ChatRoomController.SendMessage),
            ControllerName<ChatRoomController>());
#pragma warning restore CS8603 // Possible null reference return.
    }

    public static string ControllerName<TController>() => typeof(TController).Name.ControllerName();
}

