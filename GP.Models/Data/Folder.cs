using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Folder
{
    public string FolderId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();
}
