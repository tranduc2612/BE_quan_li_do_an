using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class StudentModel
    {
        public string? UserName { get; set; }
        public string? PasswordText { get; set; }
        public string? FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Avatar { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
        public string? StudentCode { get; set; }
        public string? ClassName { get; set; }
        public string? MajorId { get; set; }
        public string? SchoolYearName { get; set; }
        public string? SemesterId { get; set; }
        public int? Gender { get; set; }
        public string? Address { get; set; }
        public string? MsgError { get; set; }
        public string? DOBOriginal { get; set; }

        public double? Gpa { get; set; }
        public string? StatusProject { get; set; }

    }
    
    public class ListStudentModel
    {
        public string? SemesterId { get; set; }
        public IFormFile? file { get; set; }

    }
}
