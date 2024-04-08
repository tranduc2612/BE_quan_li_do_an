using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Project
{
    public string UserName { get; set; } = null!;

    public string? SemesterId { get; set; }

    public string? CouncilId { get; set; }

    public string? UserNameCommentator { get; set; }

    public string? UserNameMentor { get; set; }

    public string? StatusProject { get; set; }

    public double? ScoreMentor { get; set; }

    public string? CommentMentor { get; set; }

    public string? CommentCommentator { get; set; }

    public double? ScoreCommentator { get; set; }

    public double? ScoreUv1 { get; set; }

    public string? CommentUv1 { get; set; }

    public double? ScoreUv2 { get; set; }

    public string? CommentUv2 { get; set; }

    public double? ScoreUv3 { get; set; }

    public string? CommentUv3 { get; set; }

    public double? ScoreTk { get; set; }

    public string? CommentTk { get; set; }

    public double? ScoreCt { get; set; }

    public string? CommentCt { get; set; }

    public double? ScoreFinal { get; set; }

    public virtual Council? Council { get; set; }

    public virtual ICollection<DetailScheduleWeek> DetailScheduleWeeks { get; set; } = new List<DetailScheduleWeek>();

    public virtual ProjectOutline? ProjectOutline { get; set; }

    public virtual Semester? Semester { get; set; }

    public virtual Teacher? UserNameCommentatorNavigation { get; set; }

    public virtual Teacher? UserNameMentorNavigation { get; set; }

    public virtual Student UserNameNavigation { get; set; } = null!;
}
