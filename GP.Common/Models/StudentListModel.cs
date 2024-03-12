using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class StudentListModel:ListSearchBase
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
}
