using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class FlashcardDTO
    {
        public string FlashcardId { get; set; } = null!;

        public string? Question { get; set; }

        public string? Answer { get; set; }

        public string? AnswerLang { get; set; }

        public string? QuestionLang { get; set; }

        public string? Image { get; set; }

        public bool? IsDeleted { get; set; }

        public string? CreditId { get; set; }
    }
}
