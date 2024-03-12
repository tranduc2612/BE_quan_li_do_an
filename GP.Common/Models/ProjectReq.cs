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

        public int? ScoreUv1 { get; set; }

        public int? ScoreGvhd { get; set; }

        public int? ScoreGvpb { get; set; }

        public string? CommentUv1 { get; set; }

        public int? ScoreUv2 { get; set; }

        public string? CommentUv2 { get; set; }

        public int? ScoreUv3 { get; set; }

        public string? CommentUv3 { get; set; }

        public string? CommentGroupReviewOutline { get; set; }
        [Required]
        public string? UserName { get; set; }
        public virtual Teacher? UserNameCommentatorNavigation { get; set; }

        public virtual Teacher? UserNameMentorNavigation { get; set; }
    }
}
