using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Category
{
    public string CategoryId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();
}
