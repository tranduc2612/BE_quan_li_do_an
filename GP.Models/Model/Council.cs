using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Council
{
    public string CouncilId { get; set; } = null!;

    public string? CouncilName { get; set; }

    public string? CouncilZoom { get; set; }

    public string? CreatedBy { get; set; }

    public string? SemesterId { get; set; }

    public int? IsDelete { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual Semester? Semester { get; set; }

    public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();
}
