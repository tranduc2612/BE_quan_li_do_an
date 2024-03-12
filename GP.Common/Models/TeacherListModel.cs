using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class TeacherListModel: ListSearchBase
    {
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Status { get; set; }
        public string? TeacherCode { get; set; }
        public string? MajorId { get; set; }

    }
}
