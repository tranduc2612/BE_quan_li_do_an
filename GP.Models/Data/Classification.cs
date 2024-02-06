using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Classification
{
    public string ClassifyId { get; set; } = null!;

    public string? Type { get; set; }

    public string? Code { get; set; }

    public string? Value { get; set; }

    public string? Info { get; set; }
}
