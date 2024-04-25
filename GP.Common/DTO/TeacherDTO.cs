using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class TeacherDTO
    {
        public string? PasswordText { get; set; }
        public string? FullName { get; set; }
        public DateTime? Dob { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Avatar { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? Status { get; set; }

        public string? MajorId { get; set; }

        public string? EducationId { get; set; }
        public int? IsAdmin { get; set; }
        public string UserName { get; set; } = null!;
        public int? IsDelete { get; set; }
        public int? Gender { get; set; }
        public string? TypeFileAvatar { get; set; }
        public string? Address { get; set; }
        public virtual MajorDTO? Major { get; set; }
        public virtual Education? Education { get; set; }



    }
}
