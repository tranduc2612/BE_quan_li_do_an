using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class SemesterDTO
    {
        public string SemesterId { get; set; } = null!;

        public string? NameSemester { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string? ScheduleSemesterId { get; set; }
    }
}
