﻿using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class GroupReviewOutline
{
    public string GroupReviewOutlineId { get; set; } = null!;

    public string? NameGroupReviewOutline { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<ProjectOutline> ProjectOutlines { get; set; } = new List<ProjectOutline>();

    public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();
}