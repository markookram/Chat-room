using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChatRoom.Mvc.Extensions;

public static class ViewModelExtensions
{
    public static IEnumerable<SelectListItem> ToSelectListItems<T>(
        this IEnumerable<T> items,
        Func<T, int> value,
        Func<T, string> text,
        Action<SelectListItem, T> configure = null!)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        return items.Select(x =>
        {
            var item = new SelectListItem
            {
                Text = text(x),
                Value = value(x).ToString()
            };

            configure?.Invoke(item, x);

            return item;
        });
    }

    public static IEnumerable<SelectListItem> InsertEmptyChoice(
        this IEnumerable<SelectListItem> items,
        string text = "",
        int value = 0,
        bool disable = false)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        return Iterate();

        IEnumerable<SelectListItem> Iterate()
        {
            yield return new SelectListItem(text, value == 0 ? "" : value.ToString()) { Disabled = disable };

            foreach (var item in items)
            {
                yield return item;
            }
        }
    }

    public static IEnumerable<SelectListItem> EnumToSelectListItems<T>(this IEnumerable<T> items)
        where T : struct, Enum
    {
        return items.ToSelectListItems(i => Convert.ToInt32(i), i => i.ToDisplayString());
    }

    public static string ToDisplayString(this Enum @enum)
    {
        var stringEnum = @enum.ToString();
        var fi = @enum.GetType().GetField(stringEnum);

        if (fi == null)
            return stringEnum;

        var attrDisplay = fi.GetCustomAttribute<DisplayAttribute>();
        if (attrDisplay != null)
            return attrDisplay.Name!;

        return fi.GetCustomAttribute<DescriptionAttribute>()?.Description ?? stringEnum;
    }

}