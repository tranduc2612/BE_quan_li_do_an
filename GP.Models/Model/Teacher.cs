using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Teacher
{
    public string UserName { get; set; } = null!;

    public string? FullName { get; set; }

    public byte[]? Password { get; set; }

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

    public string? Token { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? TokenCreated { get; set; }

    public DateTime? TokenExpires { get; set; }

    public string? MajorId { get; set; }

    public int? Gender { get; set; }

    public string? Address { get; set; }

    public virtual Major? Major { get; set; }

    public virtual ICollection<Project> ProjectUserNameCommentatorNavigations { get; set; } = new List<Project>();

    public virtual ICollection<Project> ProjectUserNameMentorNavigations { get; set; } = new List<Project>();

    public virtual ICollection<ScheduleSemester> ScheduleSemesters { get; set; } = new List<ScheduleSemester>();

    public virtual ICollection<ScheduleWeek> ScheduleWeeks { get; set; } = new List<ScheduleWeek>();

    public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();

    public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();
}
