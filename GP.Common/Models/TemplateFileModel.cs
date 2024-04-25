using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class TemplateFileModel
    {
        public string? Code { get; set; }
        public string? TypeCode { get; set; }
        public IFormFile? file { get; set; }


    }
}
