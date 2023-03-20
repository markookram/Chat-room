namespace ChatRoom.Mvc.Extensions;

public static class MvcExtensions
{
    private const string ControllerPostfix = "Controller";

    public static string ControllerName(this string controllerTypeName)
    {
        return controllerTypeName.EndsWith(ControllerPostfix)
            ? controllerTypeName.Substring(0, controllerTypeName.Length - ControllerPostfix.Length)
            : controllerTypeName;
    }
}