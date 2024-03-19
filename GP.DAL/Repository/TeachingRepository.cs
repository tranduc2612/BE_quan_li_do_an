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
    public class TeachingRepository : ITeachingRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        private readonly MappingProfile _mapper;

        public TeachingRepository(ManagementGraduationProjectContext dbContext, MappingProfile mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Teaching Get(string username, string semesterId)
        {
            return _dbContext.Teachings.FirstOrDefault(x => x.UserNameTeacher == username && x.SemesterId == semesterId);
        }

        public List<Teaching> GetList(TeachingListModel data)
        {
            List<Teaching> query = _dbContext.Teachings.Include(x => x.UserNameTeacherNavigation).Include(x=>x.GroupReviewOutline)
                .Where(x => (string.IsNullOrEmpty(data.SemesterId) || x.SemesterId == data.SemesterId) 
                            && (string.IsNullOrEmpty(data.UserNameTeacher) || x.UserNameTeacher.Contains(data.SemesterId))
                            )
                .OrderBy(x => x.GroupReviewOutlineId == data.GroupReviewOutlineId ? 0 : 1)
                //.ThenBy(x => x.GroupReviewOutlineId)
                .ToList();

            int totalItem = query.Count();

            return query;
        }

        public List<Teaching> GetListByGroupReviewId(TeachingListModel data)
        {
            List<Teaching> query = _dbContext.Teachings.Include(x => x.UserNameTeacherNavigation).Include(x => x.GroupReviewOutline)
                .Where(x => (string.IsNullOrEmpty(data.SemesterId) || x.SemesterId == data.SemesterId)
                            && (string.IsNullOrEmpty(data.UserNameTeacher) || x.UserNameTeacher.Contains(data.SemesterId))
                            && (string.IsNullOrEmpty(data.GroupReviewOutlineId) || x.GroupReviewOutlineId == data.GroupReviewOutlineId)
                            )
                //.ThenBy(x => x.GroupReviewOutlineId)
                .ToList();

            int totalItem = query.Count();

            return query;
        }
    }
}
