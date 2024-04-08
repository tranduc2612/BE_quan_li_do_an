using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        private readonly MappingProfile _mapper;

        public ProjectOutlineRepository(ManagementGraduationProjectContext dbContext, MappingProfile mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

        public List<ProjectOutlineDTO> GetListProjectOutlineInGroup(ProjectOutlineListModel req)
        {
            //List<ProjectOutline> query = _dbContext.ProjectOutlines

            //                .Where(x => ( 
            //                   (string.IsNullOrEmpty(req.UserName) || x.UserName.Contains(req.UserName))
            //                && (string.IsNullOrEmpty(req.NameProject) || x.NameProject.Contains(req.NameProject))
            //                && (x.GroupReviewOutlineId == req.GroupReviewOutlineId)
            //                ))
            //    .ToList();
            List<ProjectOutlineDTO> query = (from p in _dbContext.Projects join 
                        po in _dbContext.ProjectOutlines on p.UserName equals po.UserName
                        where (
                            p.SemesterId == req.SemesterId &&
                            (string.IsNullOrEmpty(req.GroupReviewOutlineId) || po.GroupReviewOutlineId == req.GroupReviewOutlineId)
                            &&
                            (string.IsNullOrEmpty(req.UserName) || p.UserName.Contains(req.UserName))
                            && (string.IsNullOrEmpty(req.NameProject) || po.NameProject.Contains(req.NameProject))
                        )
                        select new ProjectOutlineDTO
                        {
                            UserName = po.UserName,
                            NameProject = po.NameProject,
                            UserNameNavigation = _mapper.MapProjectToProjectDTO(p)
                        }).ToList();
                    
            return query;
        }
        public List<ProjectOutlineDTO> GetListProjectOutline(ProjectOutlineListModel req)
        {
            List<ProjectOutlineDTO> query = (from p in _dbContext.Projects
                                             join
                                             po in _dbContext.ProjectOutlines on p.UserName equals po.UserName
                                             join 
                                             gr in _dbContext.GroupReviewOutlines on po.GroupReviewOutlineId equals gr.GroupReviewOutlineId
                                             into poGroup
                                             from gr in poGroup.DefaultIfEmpty()
                                             where (
                                                 p.SemesterId == req.SemesterId
                                                 && (String.IsNullOrEmpty(req.GroupReviewOutlineId) || po.GroupReviewOutlineId == req.GroupReviewOutlineId)
                                             )
                                             //orderby (po.GroupReviewOutlineId == req.GroupReviewOutlineId ? 0 : 1)
                                             select new ProjectOutlineDTO
                                             {
                                                 UserName = po.UserName,
                                                 NameProject = po.NameProject,
                                                 GroupReviewOutlineId = po.GroupReviewOutlineId,
                                                 GroupReviewOutline = _mapper.MapGroupReviewToGroupReviewDTO(gr),
                                                 UserNameNavigation = _mapper.MapProjectToProjectDTO(p)
                                             }).ToList();
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
