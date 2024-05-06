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

        public Teaching GetByUserNameSemester(string username, string semesterId)
        {
            return _dbContext.Teachings.Include(x=>x.Council).Include(x => x.GroupReviewOutline).FirstOrDefault(x => x.UserNameTeacher == username && x.SemesterId == semesterId);
        }

        public Teaching GetDetail(TeachingListModel data)
        {
            return _dbContext.Teachings.FirstOrDefault(x => (string.IsNullOrEmpty(data.SemesterId) || x.SemesterId == data.SemesterId)
                            && (string.IsNullOrEmpty(data.UserNameTeacher) || x.UserNameTeacher.Contains(data.SemesterId))
                            && (string.IsNullOrEmpty(data.CouncilId) || x.CouncilId == data.CouncilId)
                            && (string.IsNullOrEmpty(data.PositionInCouncil) || x.PositionInCouncil == data.PositionInCouncil)
                            );
        }

        public List<Teaching> GetListTeaching(TeachingListModel data)
        {
            List<Teaching> query = _dbContext.Teachings.Include(x => x.UserNameTeacherNavigation).ThenInclude(x=>x.Education).Include(x=>x.GroupReviewOutline)
                .Where(x => (string.IsNullOrEmpty(data.SemesterId) || x.SemesterId == data.SemesterId) 
                            && (string.IsNullOrEmpty(data.UserNameTeacher) || x.UserNameTeacher.Contains(data.SemesterId))
                            && (string.IsNullOrEmpty(data.PositionInCouncil) || x.PositionInCouncil == data.PositionInCouncil)
                            )
                //.OrderBy(x => x.GroupReviewOutlineId == data.GroupReviewOutlineId ? 0 : 1)
                //.ThenBy(x => x.GroupReviewOutlineId)
                .ToList();

            int totalItem = query.Count();

            return query;
        }

        public List<Teaching> GetListTeachingByCouncilId(string councilId)
        {
            return _dbContext.Teachings.Where(x => x.CouncilId == councilId).ToList();
        }

        public List<Teaching> GetListTeachingByGroupReview(string groupId)
        {
            return _dbContext.Teachings.Include(x => x.UserNameTeacherNavigation).Include(x => x.GroupReviewOutline).Where(x => x.GroupReviewOutlineId == groupId).ToList();
        }

        public List<Teaching> GetListTeachingBySemesterId(string semesterId)
        {
            return _dbContext.Teachings.Include(x=>x.UserNameTeacherNavigation).Include(x=>x.GroupReviewOutline).Where(x => x.SemesterId == semesterId).ToList();
        }

        public List<Teaching> GetListWithTeachingCondition(TeachingListModel data)
        {
            List<Teaching> query = _dbContext.Teachings.Include(x=>x.UserNameTeacherNavigation)
                .Where(x => (x.SemesterId == data.SemesterId)
                            && (string.IsNullOrEmpty(data.UserNameTeacher) || x.UserNameTeacher.Contains(data.SemesterId))
                            && (string.IsNullOrEmpty(data.GroupReviewOutlineId) || x.GroupReviewOutlineId == data.GroupReviewOutlineId)
                            && (string.IsNullOrEmpty(data.CouncilId) || x.CouncilId == data.CouncilId)
                            )
                //.ThenBy(x => x.GroupReviewOutlineId)
                .ToList();

            int totalItem = query.Count();

            return query;
        }

        public Teaching Update(Teaching teaching)
        {
            _dbContext.Teachings.Update(teaching);
            _dbContext.SaveChanges();
            return teaching;
        }
    }
}
