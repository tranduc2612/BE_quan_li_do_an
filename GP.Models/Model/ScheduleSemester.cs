using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class ScheduleSemester
{
    public string ScheduleSemesterId { get; set; } = null!;

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? TypeSchedule { get; set; }

    public string? SemesterId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Title { get; set; }

    public string? Implementer { get; set; }

    public string? Content { get; set; }

    public string? Note { get; set; }

    public string? StatusSend { get; set; }

    public virtual Teacher? CreatedByNavigation { get; set; }

    public virtual Semester? Semester { get; set; }
}
