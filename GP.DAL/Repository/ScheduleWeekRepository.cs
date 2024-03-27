using GP.Common.Helpers;
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
    public class ScheduleWeekRepository : IScheduleWeekRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        private readonly MappingProfile _mapper;

        public ScheduleWeekRepository(ManagementGraduationProjectContext dbContext, MappingProfile mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public string Add(ScheduleWeek req)
        {
            _dbContext.ScheduleWeeks.Add(req);
            _dbContext.SaveChanges();
            return req.ScheduleWeekId;
        }

        public DetailScheduleWeek AddDetailScheduleWeek(DetailScheduleWeek req)
        {
            _dbContext.DetailScheduleWeeks.Add(req);
            _dbContext.SaveChanges();
            return req;
        }

        public void Delete(ScheduleWeek scheduleSemester)
        {
            List<DetailScheduleWeek> find = _dbContext.DetailScheduleWeeks.Where(x => x.ScheduleWeekId == scheduleSemester.ScheduleWeekId).ToList();
            _dbContext.DetailScheduleWeeks.RemoveRange(find);
            _dbContext.ScheduleWeeks.Remove(scheduleSemester);
            _dbContext.SaveChanges();
        }

        public void DeleteDetailScheduleWeek(DetailScheduleWeek req)
        {
            _dbContext.DetailScheduleWeeks.Remove(req);
            _dbContext.SaveChanges();
        }

        public ScheduleWeek GetById(string id)
        {
            return _dbContext.ScheduleWeeks.Include(x=>x.DetailScheduleWeeks).FirstOrDefault(x => x.ScheduleWeekId == id);
        }

        public DetailScheduleWeek GetDetailScheduleWeek(string idUserName, string idScheduleWeek)
        {
            return _dbContext.DetailScheduleWeeks.FirstOrDefault(x => x.ScheduleWeekId == idScheduleWeek && x.UserNameProject == idUserName);
        }

        public List<ScheduleWeek> GetListScheduleWeek(string semesterId, string userNameCreated)
        {
            return _dbContext.ScheduleWeeks.Where(x => x.SemesterId == semesterId && x.CreatedBy == userNameCreated).ToList();
        }

        public void Update(ScheduleWeek req)
        {
            _dbContext.Update(req);
            _dbContext.SaveChanges();
        }

        public DetailScheduleWeek UpdateDetailScheduleWeek(DetailScheduleWeek req)
        {
            _dbContext.DetailScheduleWeeks.Update(req);
            _dbContext.SaveChanges();
            return req;
        }
    }
}
