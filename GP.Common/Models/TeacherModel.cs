using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class TeacherModel
    {
        public string? PasswordText { get; set; }

        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public DateTime? Dob { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Avatar { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? Status { get; set; }

        public int? IsAdmin { get; set; }

        public int? IsDelete { get; set; }

        public string? TeacherCode { get; set; }

        public string? Education { get; set; }

        public string? MajorId { get; set; }
        public int? Gender { get; set; }
        public string? Address { get; set; }
    }
}
