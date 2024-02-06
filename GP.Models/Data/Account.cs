using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Account
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Avatar { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public int? HasWarning { get; set; }

    public byte[]? Password { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? TokenCreated { get; set; }

    public DateTime? TokenExpires { get; set; }

    public virtual ICollection<AccountJoinClass> AccountJoinClasses { get; set; } = new List<AccountJoinClass>();

    public virtual ICollection<AccountLearnCredit> AccountLearnCredits { get; set; } = new List<AccountLearnCredit>();

    public virtual ICollection<Learn> Learns { get; set; } = new List<Learn>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Streak> Streaks { get; set; } = new List<Streak>();
}
