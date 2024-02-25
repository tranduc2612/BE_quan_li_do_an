using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Account
{
    public string UserName { get; set; } = null!;

    public byte[]? Password { get; set; }

    public int? Age { get; set; }

    public DateTime? Dob { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Avatar { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? Status { get; set; }

    public string? Token { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? TokenCreated { get; set; }

    public DateTime? TokenExpires { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
