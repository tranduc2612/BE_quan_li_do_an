using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Major
{
    public string MajorId { get; set; } = null!;

    public string? MajorName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
