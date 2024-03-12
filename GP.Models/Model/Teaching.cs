using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Teaching
{
    public string TeachingId { get; set; } = null!;

    public string? PostionInCouncil { get; set; }

    public string? UserNameTeacher { get; set; }

    public string? SemesterId { get; set; }

    public string? GroupReviewOutlineId { get; set; }

    public string? CouncilId { get; set; }

    public virtual Council? Council { get; set; }

    public virtual GroupReviewOutline? GroupReviewOutline { get; set; }

    public virtual Semester? Semester { get; set; }

    public virtual Teacher? UserNameTeacherNavigation { get; set; }
}
