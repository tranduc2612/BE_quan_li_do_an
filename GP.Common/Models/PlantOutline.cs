using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class PlantOutline
    {
        public string? id { get; set; }
        public string? stt { get; set; }
        public string? content { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public string? note { get; set; }
        public string? isNew { get; set; }


    }
}
