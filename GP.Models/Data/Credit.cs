using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Credit
{
    public string CreditId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public bool? IsDeleted { get; set; }

    public int? CountReport { get; set; }

    public int? CountLearn { get; set; }

    public virtual ICollection<AccountLearnCredit> AccountLearnCredits { get; set; } = new List<AccountLearnCredit>();

    public virtual ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual ICollection<Folder> Folders { get; set; } = new List<Folder>();
}
