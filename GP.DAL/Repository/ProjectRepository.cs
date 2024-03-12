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

        public Project Add(Project data)
        {
            _dbContext.Projects.Add(data);
            _dbContext.SaveChanges();
            return data;
        }

        public Project GetByUsername(string username)
        {
            return _dbContext.Projects.FirstOrDefault(x => x.UserName == username);
        }

        public List<Project> GetList(ProjectListModel data)
        {
            return _dbContext.Projects.Where(x=>
                    (String.IsNullOrEmpty(data.NameProject) || x.UserName.Contains(data.NameProject)) 
                    //&&
                    //(String.IsNullOrEmpty(data.Datetime) || x.CommentUv1.Contains(data.Datetime))
                ).Take(data.PageSize).Skip((data.PageIndex - 1) * data.PageSize).ToList();
        }


    }
}
