using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Project
{
    public string UserName { get; set; } = null!;

    public int? ScoreFinal { get; set; }

    public int? ScoreGvhd { get; set; }

    public int? ScoreGvpb { get; set; }

    public int? ScoreUv1 { get; set; }

    public string? CommentUv1 { get; set; }

    public int? ScoreUv2 { get; set; }

    public string? CommentUv2 { get; set; }

    public int? ScoreUv3 { get; set; }

    public string? CommentUv3 { get; set; }

    public string? SemesterId { get; set; }

    public string? CouncilId { get; set; }

    public string? UserNameCommentator { get; set; }

    public string? UserNameMentor { get; set; }

    public virtual Council? Council { get; set; }

    public virtual ICollection<DetailScheduleWeek> DetailScheduleWeeks { get; set; } = new List<DetailScheduleWeek>();

    public virtual ProjectOutline? ProjectOutline { get; set; }

    public virtual Semester? Semester { get; set; }

    public virtual Teacher? UserNameCommentatorNavigation { get; set; }

    public virtual Teacher? UserNameMentorNavigation { get; set; }

    public virtual Student UserNameNavigation { get; set; } = null!;
}
