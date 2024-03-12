using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class DetailScheduleWeek
{
    public string ScheduleWeekId { get; set; } = null!;

    public string UserNameProject { get; set; } = null!;

    public string? Status { get; set; }

    public string? NameFile { get; set; }

    public string? Comment { get; set; }

    public virtual ScheduleWeek ScheduleWeek { get; set; } = null!;

    public virtual Project UserNameProjectNavigation { get; set; } = null!;
}
