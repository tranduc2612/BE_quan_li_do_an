using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;

namespace GP.Business.IService
{
    public interface IProjectService
    {
        public ProjectOutline GetProjectOutlineByUsername(string username);
        public List<ProjectDTO> GetListProjectByUsernameMentor(string username,string semesterId);
        public bool UpdateNewProjectOutline(ProjectOutlineDTO projectOutlineDTO, out string message);
        public bool AddNewProjectOutline(ProjectOutlineDTO projectOutlineDTO, out string message);
        public bool AssignMentorTeacherToProject(string username_student,string username_teacher,out string message);
        public bool AssignUserNameCommentatorToProject(string username_student, string username_teacher, out string message);
        public bool UpdateScoreToProject(string username , string role, string score, string comment, out string message);


    }
}
