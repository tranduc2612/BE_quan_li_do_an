using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Teaching
{
    public string UserNameTeacher { get; set; } = null!;

    public string SemesterId { get; set; } = null!;

    public string? GroupReviewOutlineId { get; set; }

    public string? CouncilId { get; set; }

    public string? PositionInCouncil { get; set; }

    public virtual Council? Council { get; set; }

    public virtual GroupReviewOutline? GroupReviewOutline { get; set; }

    public virtual Semester Semester { get; set; } = null!;

    public virtual Teacher UserNameTeacherNavigation { get; set; } = null!;
}
