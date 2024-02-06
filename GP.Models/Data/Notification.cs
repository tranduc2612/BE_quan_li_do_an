using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Notification
{
    public string NotiId { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public string? NotiType { get; set; }

    public bool? Seen { get; set; }

    public string? Link { get; set; }

    public string? Username { get; set; }

    public virtual Account? UsernameNavigation { get; set; }
}
