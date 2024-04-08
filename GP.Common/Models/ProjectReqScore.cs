using GP.Common.DTO;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class ProjectReqScore
    {
        public string? UserName { get; set; } 

        public string? SemesterId { get; set; }
        public string? Role { get; set; }
        public string? Score { get; set; }
        public string? Comment { get; set; }

    }
}
