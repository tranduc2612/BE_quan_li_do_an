﻿using System;
using System.Collections.Generic;

namespace GP.Models.Model;

public partial class Classification
{
    public string ClassifiId { get; set; } = null!;

    public string? TypeCode { get; set; }

    public string? Code { get; set; }

    public string? Value { get; set; }

    public string? Role { get; set; }

    public string? FileName { get; set; }

    public string? Url { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }
}
