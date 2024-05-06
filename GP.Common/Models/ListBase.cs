using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class ListSearchBase
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; }
    }

    public class ProjectListModel : ListSearchBase
    {
        public string NameProject { get; set; }
        public string Datetime { get; set; }
    }

    public class StudentListModel : ListSearchBase
    {
        public string? FullName { get; set; }
        public string? StudentCode { get; set; }
        public string? UserName { get; set; }
        public string? SemesterId { get; set; }
        public string? SchoolYear { get; set; }
        public string? MajorId { get; set; }
        public string? ClassName { get; set; }
        public string? Status { get; set; }
    }

    public class TeacherListModel : ListSearchBase
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Status { get; set; }
        public string? TeacherCode { get; set; }
        public string? MajorId { get; set; }

    }

    public class SemesterListModel : ListSearchBase
    {
        public string? SemesterId { get; set; }

        public string? NameSemester { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

    }

    public class GroupReviewOutlineListModel: ListSearchBase
    {
        public string? GroupReviewOutlineId { get; set; }

        public string? NameGroupReviewOutline { get; set; }
    }
    public class GroupReviewOutlineListSemesterModel
    {
        public string? SemesterId { get; set; }
        public string? GroupReviewOutlineId { get; set; }
        public string? NameGroupReviewOutline { get; set; }
    }

    public class TeachingListModel
    {
        public string SemesterId { get; set; }
        public string? GroupReviewOutlineId { get; set; }
        public string? CouncilId { get; set; }
        public string? UserNameTeacher { get; set; }
        public string? PositionInCouncil { get; set; }
    }

    public class StudentCouncilListModel
    {
        public string SemesterId { get; set; }
        public string? CouncilId { get; set; }
    }

    public class ProjectOutlineListModel
    {
        public string SemesterId { get; set; }
        public string? GroupReviewOutlineId { get; set; }
        public string? UserName { get; set; }
        public string? NameProject { get; set; }

    }
}
