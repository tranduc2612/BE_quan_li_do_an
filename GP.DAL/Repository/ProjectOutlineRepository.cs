using GP.DAL.IRepository;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class ProjectOutlineRepository : IProjectOutlineRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        public ProjectOutlineRepository(ManagementGraduationProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProjectOutline Add(ProjectOutline data)
        {
            _dbContext.Add(data);
            _dbContext.SaveChanges();
            return data;
        }

        public ProjectOutline GetById(string username)
        {
            return _dbContext.ProjectOutlines.FirstOrDefault(x => x.UserName == username);
        }
    }
}
