using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Learn
{
    public string Username { get; set; } = null!;

    public string FlashcardId { get; set; } = null!;

    public string? Process { get; set; }

    public DateTime? LastLearnedDate { get; set; }

    public bool? RecentWrongExam { get; set; }

    public bool? RecentWrongLearn { get; set; }

    public virtual Flashcard Flashcard { get; set; } = null!;

    public virtual Account UsernameNavigation { get; set; } = null!;
}
