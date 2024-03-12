using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class ScheduleWeek
{
    public string ScheduleWeekId { get; set; } = null!;

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? SemesterId { get; set; }

    public virtual Teacher? CreatedByNavigation { get; set; }

    public virtual ICollection<DetailScheduleWeek> DetailScheduleWeeks { get; set; } = new List<DetailScheduleWeek>();

    public virtual Semester? Semester { get; set; }
}
