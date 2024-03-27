using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class ScheduleSemesterModel
    {
        public string? ScheduleSemesterId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? TypeSchedule { get; set; }
        public string? SemesterId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Implementer { get; set; }
        public string? Content { get; set; }
        public string? Note { get; set; }
    }
}
