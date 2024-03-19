using GP.Common.DTO;
using GP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IScheduleSemesterService
    {
        public List<ScheduleSemesterDTO> GetListScheduleSemester(string semesterId);
        public bool AddScheduleSemester(ScheduleSemesterModel data, out string message);
        public bool UpdateScheduleSemester(ScheduleSemesterModel data, out string message);
        public bool DeleteScheduleSemester(string id, out string message);
    }
}
