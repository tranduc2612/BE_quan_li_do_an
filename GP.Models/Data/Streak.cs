using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Streak
{
    public string StreakId { get; set; } = null!;

    public DateTime? LearnDate { get; set; }

    public string? Username { get; set; }

    public virtual Account? UsernameNavigation { get; set; }
}
