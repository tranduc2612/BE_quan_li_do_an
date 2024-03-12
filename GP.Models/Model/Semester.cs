using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Semester
{
    public string SemesterId { get; set; } = null!;

    public string? NameSemester { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? ScheduleSemesterId { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ScheduleSemester? ScheduleSemester { get; set; }

    public virtual ICollection<ScheduleWeek> ScheduleWeeks { get; set; } = new List<ScheduleWeek>();

    public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();
}
