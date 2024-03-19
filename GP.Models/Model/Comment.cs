using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Comment
{
    public string CommentId { get; set; } = null!;

    public string? ContentComment { get; set; }

    public string? CreatedBy { get; set; }

    public string? UserName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ProjectOutline? UserNameNavigation { get; set; }
}
