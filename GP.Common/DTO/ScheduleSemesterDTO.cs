using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class ScheduleSemesterDTO
    {
        public string ScheduleSemesterId { get; set; } = null!;

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string? TypeSchedule { get; set; }

        public string? SemesterId { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string? Implementer { get; set; }
        public string? Content { get; set; }
        public string? Note { get; set; }

        public virtual TeacherDTO? CreatedByNavigation { get; set; }

        public virtual SemesterDTO? Semester { get; set; }
    }
}
