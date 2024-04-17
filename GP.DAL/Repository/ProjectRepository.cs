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

        public List<Project> GetListProjectByCouncilId(string semesterId, string councilId)
        {
            return _dbContext.Projects.Include(x=>x.ProjectOutline).Include(x=>x.UserNameNavigation).Include(x=>x.Council)
                .Where(x => x.SemesterId == semesterId 
                      && 
                      (String.IsNullOrEmpty(councilId) || councilId == x.CouncilId)
                      &&
                      x.ProjectOutline != null
                      ).ToList();
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

        public Project GetProjectByHashKeyCommentator(string key)
        {
            return _dbContext.Projects.Include(x=>x.UserNameMentorNavigation).ThenInclude(x=>x.Major).Include(x=>x.ProjectOutline).Include(x=>x.UserNameNavigation).FirstOrDefault(x => x.HashKeyMentor == key);
        }

        public Project GetProjectByHashKeyMentor(string key)
        {
            return _dbContext.Projects.Include(x => x.UserNameCommentatorNavigation).ThenInclude(x => x.Major).Include(x => x.ProjectOutline).Include(x => x.UserNameNavigation).FirstOrDefault(x => x.HashKeyCommentator == key);
        }

        public Project GetProjectByUsername(string username)
        {
            return _dbContext.Projects.FirstOrDefault(x=>x.UserName == username);
        }

        public Project GetProjectByUsernameData(string username)
        {
            return _dbContext.Projects.Include(x => x.ProjectOutline).Include(x => x.UserNameMentorNavigation).ThenInclude(x=>x.Major).Include(x => x.UserNameNavigation).FirstOrDefault(x => x.UserName == username);
        }

        public Project Update(Project project)
        {
            _dbContext.Update(project);
            _dbContext.SaveChanges();
           return project;
        }
    }
}
