using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class AccountLogin
    {
        public AccountLogin()
        {
        }

        public AccountLogin(string userName, string password, string role)
        {
            UserName = userName;
            Password = password;
            Role = role;
        }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }

    public class ChangePassword
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? PasswordOld { get; set; }
        [Required]
        public string? PasswordNew { get; set; }
        [Required]
        public string? Role { get; set; }
    }

    public class CheckUserName
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Role { get; set; }
    }

    public class ChangeAvatar
    {
        public string? Role { get; set; }
        public string? UserName { get; set; }
        public IFormFile? file { get; set; }
    }
}
