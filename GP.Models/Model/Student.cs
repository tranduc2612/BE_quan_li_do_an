using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Student
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

    public string? Token { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? TokenCreated { get; set; }

    public DateTime? TokenExpires { get; set; }

    public virtual Major? Major { get; set; }

    public virtual Project? Project { get; set; }
}
