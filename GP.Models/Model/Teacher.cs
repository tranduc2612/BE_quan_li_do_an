using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Teacher
{
    public string UserName { get; set; } = null!;

    public string? TeacherCode { get; set; }

    public string? Major { get; set; }

    public string? Education { get; set; }

    public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();

    public virtual Account UserNameNavigation { get; set; } = null!;
}
