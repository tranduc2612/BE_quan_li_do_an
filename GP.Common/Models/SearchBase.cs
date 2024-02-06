using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class SearchBase
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; }
        public string SearchText { get; set; }
    }
}
