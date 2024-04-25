using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class EducationDTO
    {
        public string? EducationId { get; set; }

        public string? EducationName { get; set; }

        public int? MaxStudentMentor { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
