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

        public Project GetProjectByUsername(string username)
        {
            return _dbContext.Projects.FirstOrDefault(x=>x.UserName == username);
        }
    }
}
