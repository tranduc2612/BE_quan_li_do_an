using GP.Common.DTO;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class ProjectReq
    {
        public int? ScoreFinal { get; set; }

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

        public string? CommentGroupReviewOutline { get; set; }
        [Required]
        public string? UserName { get; set; }
        public virtual Teacher? UserNameCommentatorNavigation { get; set; }

        public virtual Teacher? UserNameMentorNavigation { get; set; }
    }
}
