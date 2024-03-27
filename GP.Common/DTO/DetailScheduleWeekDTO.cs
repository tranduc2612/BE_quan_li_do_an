using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class DetailScheduleWeekDTO
    {
        public string? ScheduleWeekId { get; set; }

        public string? UserNameProject { get; set; }

        public string? Status { get; set; }

        public string? Comment { get; set; }

        public string? NameFile { get; set; }

        public string? SizeFile { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual ScheduleWeekDTO? ScheduleWeek { get; set; }

        public virtual ProjectDTO? UserNameProjectNavigation { get; set; }
    }
}
