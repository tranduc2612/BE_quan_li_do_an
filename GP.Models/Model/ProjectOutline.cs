using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class ProjectOutline
{
    public string UserName { get; set; } = null!;

    public string? NameProject { get; set; }

    public string? PlantOutline { get; set; }

    public string? TechProject { get; set; }

    public string? ExpectResult { get; set; }

    public string? GroupReviewOutlineId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual GroupReviewOutline? GroupReviewOutline { get; set; }

    public virtual Project UserNameNavigation { get; set; } = null!;
}
