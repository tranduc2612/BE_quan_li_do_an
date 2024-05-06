using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface IScheduleWeekRepository
    {
        public List<ScheduleWeek> GetListScheduleWeek(string semesterId,string userNameCreated);
        public ScheduleWeek GetById(string id);
        public string Add(ScheduleWeek req);
        public void Update(ScheduleWeek req);
        public void Delete(ScheduleWeek scheduleWeek);
        public DetailScheduleWeek AddDetailScheduleWeek(DetailScheduleWeek req);
        public void DeleteDetailScheduleWeek(DetailScheduleWeek req);
        public DetailScheduleWeek UpdateDetailScheduleWeek(DetailScheduleWeek req);
        public DetailScheduleWeek GetDetailScheduleWeek(string idUserName,string idScheduleWeek);
        public bool ScanScheduleSemester();


    }
}
