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
            return _dbContext.ProjectOutlines.Include(x=>x.GroupReviewOutline).Include(x=>x.UserNameNavigation).ThenInclude(x=>x.UserNameMentorNavigation).ThenInclude(x => x.Major).Include(x=>x.GroupReviewOutline).FirstOrDefault(x => x.UserName == username);
        }

        public List<ProjectOutline> GetListProjectOutlineByGroupId(ProjectOutlineListModel req)
        {
            List<ProjectOutline> query = _dbContext.ProjectOutlines.Include(x=>x.GroupReviewOutline).Include(x => x.UserNameNavigation).ThenInclude(x=>x.UserNameMentorNavigation)
                .Where(x => (x.UserNameNavigation.SemesterId == req.SemesterId)
                            && (string.IsNullOrEmpty(req.UserName) || x.UserName.Contains(req.UserName))
                            && (string.IsNullOrEmpty(req.NameProject) || x.NameProject.Contains(req.NameProject))
                            && (string.IsNullOrEmpty(req.GroupReviewOutlineId) || x.GroupReviewOutlineId == req.GroupReviewOutlineId)
                            )
                .OrderBy(x => x.GroupReviewOutlineId == req.GroupReviewOutlineId ? 0 : 1)
                .ToList();

            return query;
        }
        public List<ProjectOutline> GetListProjectOutline(ProjectOutlineListModel req)
        {
            List<ProjectOutline> query = _dbContext.ProjectOutlines.Include(x => x.GroupReviewOutline)
                .Include(x => x.UserNameNavigation).ThenInclude(x => x.UserNameMentorNavigation)
                .Where(x => (x.UserNameNavigation.SemesterId == req.SemesterId)
                            && (string.IsNullOrEmpty(req.UserName) || x.UserName.Contains(req.UserName))
                            && (string.IsNullOrEmpty(req.NameProject) || x.NameProject.Contains(req.NameProject))
                            )
                .OrderBy(x => x.GroupReviewOutlineId == req.GroupReviewOutlineId ? 0 : 1)
                .ToList();

            return query;
        }

        public ProjectOutline Update(ProjectOutline data)
        {
            _dbContext.Update(data);
            _dbContext.SaveChanges();
            return data;
        }
    }
}
