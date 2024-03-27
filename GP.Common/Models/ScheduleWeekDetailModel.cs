using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class ScheduleWeekDetailModel
    {
        public string? ScheduleWeekId { get; set; }
        public string? UserNameProject { get; set; }
        public string? Comment { get; set; }
        public string? NameFile { get; set; }
        public string? SizeFile { get; set; }
        public string? Function { get; set; }
        public IFormFile? file { get; set; }

    }
}
