using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Comment
{
    public string CommentId { get; set; } = null!;

    public string? ContentComment { get; set; }

    public string? CreatedBy { get; set; }

    public string? CreatedDate { get; set; }

    public string? ProjectOutlineId { get; set; }

    public virtual ProjectOutline? ProjectOutline { get; set; }
}
