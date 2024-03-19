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
        public int? ScoreFinal { get; set; }

        public int? ScoreUv1 { get; set; }

        public int? ScoreGvhd { get; set; }

        public int? ScoreGvpb { get; set; }

        public string? CommentUv1 { get; set; }

        public int? ScoreUv2 { get; set; }

        public string? CommentUv2 { get; set; }

        public int? ScoreUv3 { get; set; }

        public string? CommentUv3 { get; set; }
        public string? StatusProject { get; set; }

        public string? CommentGroupReviewOutline { get; set; }
        [Required]
        public string? UserName { get; set; }
        public virtual TeacherDTO? UserNameCommentatorNavigation { get; set; }

        public virtual TeacherDTO? UserNameMentorNavigation { get; set; }
        public virtual ProjectOutline? ProjectOutline { get; set; }
        public virtual SemesterDTO? Semester { get; set; }

    }
}
