using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Class
{
    public string ClassId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public bool? AcceptEdit { get; set; }

    public bool? IsDeleted { get; set; }

    public string? Status { get; set; }

    public int? CountReport { get; set; }

    public int? CountJoin { get; set; }

    public virtual ICollection<AccountJoinClass> AccountJoinClasses { get; set; } = new List<AccountJoinClass>();

    public virtual ICollection<Credit> Credits { get; set; } = new List<Credit>();

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
}
