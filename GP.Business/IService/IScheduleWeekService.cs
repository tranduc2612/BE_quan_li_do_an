using GP.Common.DTO;
using GP.Common.Models;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IScheduleWeekService
    {
        public ScheduleWeekDTO GetById(string id);
        public List<ScheduleWeekDTO> GetListScheduleWeek(string semesterId, string usernameCreated);
        public string AddScheduleWeek(ScheduleWeekModel data, out string message, out bool check);
        public bool UpdateScheduleWeek(ScheduleWeekModel data, out string message);
        public bool DeleteScheduleWeek(string id, out string message);
        public DetailScheduleWeek HandleScheduleWeekDetail(ScheduleWeekDetailModel file, out string message, out bool check);
        public DetailScheduleWeek GetScheduleWeekDetail(string userName,string idScheduleWeek);
        public DetailScheduleWeek UpdateComment(ScheduleWeekDetailModel data, out string message);
    }
}
