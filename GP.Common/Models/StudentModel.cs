﻿using GP.Models.Model;
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
        public string SemesterId { get; set; }


    }
}