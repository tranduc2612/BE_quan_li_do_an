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
        public string UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? Age { get; set; }
        public DateTime? DOB { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenExpires { get; set; }
    }
}
