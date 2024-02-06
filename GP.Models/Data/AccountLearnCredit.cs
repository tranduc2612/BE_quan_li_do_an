using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class AccountLearnCredit
{
    public string CreditId { get; set; } = null!;

    public string Username { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Credit Credit { get; set; } = null!;

    public virtual Account UsernameNavigation { get; set; } = null!;
}
