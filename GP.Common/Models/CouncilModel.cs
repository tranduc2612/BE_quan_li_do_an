using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class CouncilModel
    {
        public string? CouncilId { get; set; }

        public string? CouncilName { get; set; }

        public string? CouncilZoom { get; set; }

        public string? CreatedBy { get; set; }

        public int? IsDelete { get; set; }
        [Required]
        public string? SemesterId { get; set; }

    }

    public class AssignTeachingCouncilModel
    {
        public string? CouncilId { get; set; }
        public string? UsernameTeaching { get; set; }
        public string? PositionInCouncil { get; set; }
        public string? SemesterTeachingId { get; set; }

    }

    public class AssignProjectCouncilModel
    {
        public string? SemesterId { get; set; }
        public string? CouncilId { get; set; }
        public List<string>? UsernameProjects { get; set; }

    }

    public class AssignUserNameCommentatorToProjectModel
    {
        public string? UserNameCommentator { get; set; }
        public string? UserNameProject { get; set; }

    }
}
