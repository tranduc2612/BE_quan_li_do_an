﻿using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Teaching
{
    public string TeachingId { get; set; } = null!;

    public string? PostionInCouncil { get; set; }

    public string? UserName { get; set; }

    public string? SemesterId { get; set; }

    public virtual Semester? Semester { get; set; }

    public virtual Teacher? UserNameNavigation { get; set; }
}