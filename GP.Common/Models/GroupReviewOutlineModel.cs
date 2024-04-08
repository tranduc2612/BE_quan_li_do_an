using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class GroupReviewOutlineModel
    {
        public string? GroupReviewOutlineId { get; set; }

        public string? NameGroupReviewOutline { get; set; }

        public string? CreatedBy { get; set; }

        public int? IsDelete { get; set; }
        public string? SemesterId { get; set; }
    }

    public class AssignTeachingGroupReviewOutlineModel
    {
        public string? GroupReviewOutlineId { get; set; }
        public List<string>? UsernameTeaching { get; set; }
        public string? SemesterTeachingId { get; set; }

    }

    public class AssignProjectOutlineGroupReviewOutlineModel
    {
        public string? SemesterTeachingId { get; set; }
        public string? GroupReviewOutlineId { get; set; }
        public List<string>? UsernameProjectOutline { get; set; }

    }
}
