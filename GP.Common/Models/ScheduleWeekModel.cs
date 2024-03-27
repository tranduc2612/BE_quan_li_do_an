using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class ScheduleWeekModel
    {
        public string? ScheduleWeekId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? SemesterId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
