using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class ProjectDTO
    {
        public string? UserName { get; set; }

        public double? ScoreFinal { get; set; }

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
        public string? HashKeyMentor { get; set; }
        public string? HashKeyCommentator { get; set; }
        public virtual StudentDTO? UserNameNavigation { get; set; }
        public virtual TeacherDTO? UserNameCommentatorNavigation { get; set; }
        public virtual TeacherDTO? UserNameMentorNavigation { get; set; }
        public virtual ProjectOutlineDTO? ProjectOutline { get; set; }
        public virtual SemesterDTO? Semester { get; set; }
        public virtual CouncilDTO? Council { get; set; }


    }
}
