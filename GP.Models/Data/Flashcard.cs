using System;
using System.Collections.Generic;

namespace GP.Models.Data;

public partial class Flashcard
{
    public string FlashcardId { get; set; } = null!;

    public string? Question { get; set; }

    public string? Answer { get; set; }

    public string? AnswerLang { get; set; }

    public string? QuestionLang { get; set; }

    public string? Image { get; set; }

    public bool? IsDeleted { get; set; }

    public string? CreditId { get; set; }

    public virtual Credit? Credit { get; set; }

    public virtual ICollection<Learn> Learns { get; set; } = new List<Learn>();
}
