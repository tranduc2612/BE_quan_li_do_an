using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class SemesterDTO
    {
        public SemesterDTO()
        {

        }
        public SemesterDTO(string semesterId, string? nameSemester, DateTime? fromDate, DateTime? toDate, DateTime? createdAt, string? createdBy, int? totalProjectAmount, int? rejectProjectAmount, int? doingProjectAmount, int? acceptProjectAmount, int? pauseProjectAmount, double? avgScoreProject,int? isDelete)
        {
            SemesterId = semesterId;
            NameSemester = nameSemester;
            FromDate = fromDate;
            ToDate = toDate;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            TotalProjectAmount = totalProjectAmount;
            RejectProjectAmount = rejectProjectAmount;
            DoingProjectAmount = doingProjectAmount;
            AcceptProjectAmount = acceptProjectAmount;
            PauseProjectAmount = pauseProjectAmount;
            AvgScoreProject = avgScoreProject;
            IsDelete = isDelete;
        }

        public string SemesterId { get; set; } = null!;

        public string? NameSemester { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string? ScheduleSemesterId { get; set; }
        public string? CreatedBy { get; set; }
        public int? IsDelete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? TotalProjectAmount { get; set; }
        public int? RejectProjectAmount { get; set; }
        public int? DoingProjectAmount { get; set; }
        public int? AcceptProjectAmount { get; set; }
        public int? PauseProjectAmount { get; set; }
        public double? AvgScoreProject { get; set; }
    }
}
