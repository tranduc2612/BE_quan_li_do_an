using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class AccountDTO
    {
        [Required]
        //public string UserName { get; set; } = null!;
        public string? FullName { get; set; }
        public string? PasswordText { get; set; }
        public DateTime? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
        public string? StudentCode { get; set; }
        public string? Class { get; set; }
        public string? Major { get; set; }
        public string? SchoolYear { get; set; }
        public string? Token { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenCreated { get; set; }
        public DateTime? TokenExpires { get; set; }
        [Required]
        public string Role { get; set; }
        public string? TeacherCode { get; set; }
        public string? Education { get; set; }
        [Required]
        public string SemesterId { get; set; } = null!;
    }
}
