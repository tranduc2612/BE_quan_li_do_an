using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class CreditDTO
    {
        public string CreditId { get; set; } = null!;

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public int? CountReport { get; set; }

        public int? CountLearn { get; set; }
    }
}
