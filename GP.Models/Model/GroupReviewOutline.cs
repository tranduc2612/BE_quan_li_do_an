using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class GroupReviewOutline
{
    public string GroupReviewOutlineId { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public int? IsDelete { get; set; }

    public string? NameGroupReviewOutline { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? SemesterId { get; set; }

    public virtual ICollection<ProjectOutline> ProjectOutlines { get; set; } = new List<ProjectOutline>();

    public virtual Semester? Semester { get; set; }

    public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();
}
