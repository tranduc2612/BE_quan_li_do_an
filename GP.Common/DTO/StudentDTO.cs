using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class StudentDTO
    {
        public string UserName { get; set; } = null!;

        public byte[]? Password { get; set; }

        public string? FullName { get; set; }

        public DateTime? Dob { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Avatar { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? Status { get; set; }

        public string? StudentCode { get; set; }

        public string? MajorId { get; set; }

        public string? ClassName { get; set; }

        public string? SchoolYearName { get; set; }

        public int? IsDelete { get; set; }

        public virtual MajorDTO? Major { get; set; }

        public virtual ProjectDTO? Project { get; set; }
    }
}
