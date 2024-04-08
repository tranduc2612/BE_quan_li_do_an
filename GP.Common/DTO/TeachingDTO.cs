using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class TeachingDTO
    {
        public string? PositionInCouncil { get; set; }

        public string? UserNameTeacher { get; set; }

        public string? SemesterId { get; set; }

        public string? GroupReviewOutlineId { get; set; }

        public string? CouncilId { get; set; }

        public virtual Council? Council { get; set; }

        public virtual GroupReviewOutlineDTO? GroupReviewOutline { get; set; }

        public virtual SemesterDTO? Semester { get; set; }

        public virtual TeacherDTO? UserNameTeacherNavigation { get; set; }
    }
}
