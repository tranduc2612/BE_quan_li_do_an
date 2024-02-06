using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class AccountJoinClass
{
    public string ClassId { get; set; } = null!;

    public string Username { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Account UsernameNavigation { get; set; } = null!;
}
