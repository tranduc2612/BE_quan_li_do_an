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
        public DateTime? DOB { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
        public int? IsDelete { get; set; }
        public string? Status { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenExpires { get; set; }
        public DateTime? TokenCreated { get; set; }
        public int? IsAdmin { get; set; }
    }
}
