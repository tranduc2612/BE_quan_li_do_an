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
            req.ScheduleWeekId = Guid.NewGuid().ToString();
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

        public bool ScanScheduleSemester()
        {
            var scheduleSemesters = _dbContext.ScheduleSemesters.ToList();

            if (scheduleSemesters == null || !scheduleSemesters.Any())
            {
                return false;
            }

            foreach (var schedule in scheduleSemesters)
            {
                DateTime currentDate = DateTime.Now;
                if (currentDate < schedule.FromDate)
                {
                    continue;
                }

                List<Project> projectInSemester = null;

                if ((schedule.TypeSchedule == "SCHEDULE_FOR_MENTOR" || schedule.TypeSchedule == "SCHEDULE_FOR_COMMENTATOR") && schedule.StatusSend == "W")
                {
                    schedule.StatusSend = "S";
                    _dbContext.Update(schedule);
                    _dbContext.SaveChanges();
                    projectInSemester = _dbContext.Projects
                                                    .Where(x => x.SemesterId == schedule.SemesterId)
                                                    .ToList();

                    foreach (var project in projectInSemester)
                    {
                        if (schedule.TypeSchedule == "SCHEDULE_FOR_MENTOR")
                        {
                            project.HashKeyMentor = Guid.NewGuid().ToString();
                        }
                        else if (schedule.TypeSchedule == "SCHEDULE_FOR_COMMENTATOR")
                        {
                            project.HashKeyCommentator = Guid.NewGuid().ToString();
                        }
                    }

                    _dbContext.UpdateRange(projectInSemester);
                    _dbContext.SaveChanges();
                }
            }

            return true;
        }
    }
}
