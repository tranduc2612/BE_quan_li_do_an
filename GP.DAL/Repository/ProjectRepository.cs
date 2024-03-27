using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        public ProjectRepository(ManagementGraduationProjectContext dbContext) {
            _dbContext = dbContext;
        }

        public Project AssignMentor(Teacher teacher, string username)
        {   
            Project project= _dbContext.Projects.FirstOrDefault(x=>x.UserName == username);
            if(project != null) { 
                project.UserNameMentor = teacher.UserName;
                _dbContext.SaveChanges();
                return project;
            }
            return null;
        }

        public List<Project> GetListProjectByUsernameMentor(string username_mentor,string semesterId)
        {
            //var query = from project in _dbContext.Projects
            //join student in _dbContext.Students on project.UserName equals student.UserName
            //where project.SemesterId == semesterId && project.UserNameMentor == username_mentor
            //select project;
            //return query.ToList();
            return _dbContext.Projects.Include(x => x.UserNameNavigation).Include(x=>x.ProjectOutline).Where(x => x.SemesterId == semesterId && x.UserNameMentor == username_mentor).ToList();
        }

        public Project GetProjectByUsername(string username)
        {
            return _dbContext.Projects.FirstOrDefault(x=>x.UserName == username);
        }
    }
}
