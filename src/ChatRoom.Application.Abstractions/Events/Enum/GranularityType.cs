using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Application.Abstractions.Events.Enum;

/// <summary>
/// Possible types of the events log queries
/// </summary>
public enum GranularityType
{
    [Display(Name = "all")]
    All,

    [Display(Name = "by hour")]
    Hourly,

    [Display(Name = "by minute")]
    Minute,

    [Display(Name = "agg. by hour")]
    AggregatedByHour,

    [Display(Name = "agg. by minute")]
    AggregatedByMinute,

}