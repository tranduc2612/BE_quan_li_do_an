using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class CouncilDTO
    {
        public string? CouncilId { get; set; }

        public string? CouncilName { get; set; }

        public string? CouncilZoom { get; set; }

        public string? CreatedBy { get; set; }

        public int? IsDelete { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? SemesterId { get; set; }
        public int? SLGV { get; set; }
        public int? SLSV { get; set; }

        public virtual ICollection<ProjectDTO> Projects { get; set; } = new List<ProjectDTO>();

        public virtual SemesterDTO? Semester { get; set; }

        public virtual ICollection<TeachingDTO> Teachings { get; set; } = new List<TeachingDTO>();
    }
}
