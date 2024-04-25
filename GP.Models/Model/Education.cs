using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Education
{
    public string EducationId { get; set; } = null!;

    public string? EducationName { get; set; }

    public int? MaxStudentMentor { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}
