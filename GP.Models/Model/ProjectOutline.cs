using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class ProjectOutline
{
    public string ProjectOutlineId { get; set; } = null!;

    public int? NameProject { get; set; }

    public int? PlantOutline { get; set; }

    public int? TechProject { get; set; }

    public int? ExpectResult { get; set; }

    public string? ProjectId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Project? Project { get; set; }
}
