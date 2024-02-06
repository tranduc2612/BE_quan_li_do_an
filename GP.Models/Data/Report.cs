using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Report
{
    public string ReportId { get; set; } = null!;

    public string? Username { get; set; }

    public string? ObjReportedId { get; set; }

    public string? TypeObj { get; set; }

    public string? Reason { get; set; }

    public string? Message { get; set; }

    public virtual Account? UsernameNavigation { get; set; }
}
