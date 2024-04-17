using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class LoginResponseDTO
    {
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public DateTime? DOB { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public int? IsDelete { get; set; }
        public string? Status { get; set; }
        public string? StudentCode { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenExpires { get; set; }
        public DateTime? TokenCreated { get; set; }
        public int? IsAdmin { get; set; }
        public int? Gender { get; set; }
        public string? Address { get; set; }
        public double? Gpa { get; set; }
        public string? ClassName { get; set; }
        public string? SchoolYearName { get; set; }
        public string? Education { get; set; }
        public virtual MajorDTO? Major { get; set; }
        public virtual ProjectDTO? Project { get; set; }
        public virtual ProjectOutlineDTO? ProjectOutline { get; set; }


    }
}
