using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Project
{
    public string UserName { get; set; } = null!;

    public int? ScoreFinal { get; set; }

    public string? SemesterId { get; set; }

    public string? CouncilId { get; set; }

    public string? UserNameCommentator { get; set; }

    public string? UserNameMentor { get; set; }

    public string? StatusProject { get; set; }

    public int? ScoreMentor { get; set; }

    public string? CommentMentor { get; set; }

    public string? CommentCommentator { get; set; }

    public int? ScoreCommentator { get; set; }

    public int? Score1 { get; set; }

    public string? Comment1 { get; set; }

    public int? Score2 { get; set; }

    public string? Comment2 { get; set; }

    public int? Score3 { get; set; }

    public string? Comment3 { get; set; }

    public virtual Council? Council { get; set; }

    public virtual ICollection<DetailScheduleWeek> DetailScheduleWeeks { get; set; } = new List<DetailScheduleWeek>();

    public virtual ProjectOutline? ProjectOutline { get; set; }

    public virtual Semester? Semester { get; set; }

    public virtual Teacher? UserNameCommentatorNavigation { get; set; }

    public virtual Teacher? UserNameMentorNavigation { get; set; }

    public virtual Student UserNameNavigation { get; set; } = null!;
}
