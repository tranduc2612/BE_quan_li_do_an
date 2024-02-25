using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Project
{
    public string ProjectId { get; set; } = null!;

    public int? ScoreFinal { get; set; }

    public int? ScoreUv1 { get; set; }

    public int? ScoreGvhd { get; set; }

    public int? ScoreGvpb { get; set; }

    public string? CommentUv1 { get; set; }

    public int? ScoreUv2 { get; set; }

    public string? CommentUv2 { get; set; }

    public int? ScoreUv3 { get; set; }

    public string? CommentUv3 { get; set; }

    public string? CommentGroupReviewOutline { get; set; }

    public string? ProjectOutlineId { get; set; }

    public string? UserName { get; set; }

    public virtual ICollection<ProjectOutline> ProjectOutlines { get; set; } = new List<ProjectOutline>();

    public virtual Student? UserNameNavigation { get; set; }
}
