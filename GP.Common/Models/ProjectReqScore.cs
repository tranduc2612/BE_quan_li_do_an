using GP.Common.DTO;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
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

    public class ProjectReviewKey
    {
        public string? Role { get; set; }
        public string? Key { get; set; }
    }

    public class ProjectFinalFile
    {
        public string? Function { get; set; }

        public string? UserName { get; set; }
        public string? NameFileFinal { get; set; }

        public string? SizeFileFinal { get; set; }

        public string? TypeFileFinal { get; set; }
        public IFormFile? file { get; set; }

    }
}
