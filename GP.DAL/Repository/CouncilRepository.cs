using GP.Common.DTO;
using GP.Common.Helpers;
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
    public class CouncilRepository : ICouncilRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        private readonly MappingProfile _mapper;

        public CouncilRepository(ManagementGraduationProjectContext dbContext, MappingProfile mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public Council Add(Council council)
        {
            _dbContext.Add(council);
            _dbContext.SaveChanges();
            return council;
        }

        public void Delete(Council council)
        {
            List<Project> pU = _dbContext.Projects.Where(x => x.CouncilId == council.CouncilId).ToList();
            List<Teaching> tcU = _dbContext.Teachings.Where(x => x.CouncilId == council.CouncilId).ToList();
            foreach (var obj in pU)
            {
                obj.CouncilId = null;
            }
            _dbContext.UpdateRange(pU);

            foreach (var obj in tcU)
            {
                obj.CouncilId = null;
            }
            _dbContext.UpdateRange(tcU);

            council.IsDelete = 1;
            _dbContext.Update(council);
            _dbContext.SaveChanges();
        }

        public Council GetById(string id)
        {
            return _dbContext.Councils.Include(x=>x.Teachings).ThenInclude(x=>x.UserNameTeacherNavigation).FirstOrDefault(x => x.CouncilId == id);
        }

        public List<CouncilDTO> GetList(CouncilModel req)
        {
            List<CouncilDTO> lstCouncil = _dbContext.Councils
                .Where(x => x.SemesterId == req.SemesterId && x.IsDelete == 0
                //&&
                //    (string.IsNullOrEmpty(data.GroupReviewOutlineId) || x.GroupReviewOutlineId.Contains(data.GroupReviewOutlineId))
                //&&
                //    (string.IsNullOrEmpty(data.NameGroupReviewOutline) || x.NameGroupReviewOutline.Contains(data.NameGroupReviewOutline))
                )
                .ToList()
                .Select(x =>
                {
                    return new CouncilDTO
                    {
                        CouncilId = x.CouncilId,
                        CouncilName = x.CouncilName,
                        CouncilZoom = x.CouncilZoom,
                        CreatedDate = x.CreatedDate,
                        CreatedBy = x.CreatedBy,
                        SLGV = _dbContext.Teachings.Where(t => t.CouncilId == x.CouncilId).Count(),
                        SLSV = _dbContext.Projects.Where(t => t.CouncilId == x.CouncilId).Count()
                    };
                })
                .ToList();
            return lstCouncil;
        }

        public List<Teaching> GetListTeachingInCouncil(TeachingListModel data)
        {
            List<Teaching> query = _dbContext.Teachings.Include(x => x.UserNameTeacherNavigation)
                .Where(x => (x.SemesterId == data.SemesterId)
                            &&  x.CouncilId == data.CouncilId
                            )
                //.ThenBy(x => x.GroupReviewOutlineId)
                .ToList();

            int totalItem = query.Count();

            return query;
        }

        public List<Teaching> GetListTeachingNotInCouncil(TeachingListModel data)
        {
            List<Teaching> query = _dbContext.Teachings.Include(x => x.UserNameTeacherNavigation)
                .Where(x => (x.SemesterId == data.SemesterId)
                            && x.CouncilId == null
                            )
                //.ThenBy(x => x.GroupReviewOutlineId)
                .ToList();

            int totalItem = query.Count();

            return query;
        }

        public Council Update(Council council)
        {
            _dbContext.Update(council);
            _dbContext.SaveChanges();
            return council;
        }
    }
}
