using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Student
{
    public string UserName { get; set; } = null!;

    public string? StudentCode { get; set; }

    public string? Class { get; set; }

    public string? Major { get; set; }

    public string? SchoolYear { get; set; }

    public virtual Project? Project { get; set; }

    public virtual Account UserNameNavigation { get; set; } = null!;
}
