using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class AccountDTO
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PasswordText { get; set; }

    }
}
