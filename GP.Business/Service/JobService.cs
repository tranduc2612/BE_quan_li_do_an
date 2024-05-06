using GP.Business.IService;
using GP.Common.Helpers;
using GP.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class JobService : IJobService
    {
        IScheduleWeekRepository _scheduleWeekRepository;
        ISemesterRepository _semesterRepository;
        private readonly MappingProfile _mapper;
        public JobService(IScheduleWeekRepository scheduleWeekRepository, ISemesterRepository semesterRepository, MappingProfile mapper)
        {
            _scheduleWeekRepository = scheduleWeekRepository;
            _semesterRepository = semesterRepository;
            _mapper = mapper;
        }
        public void JobExcuteScheduleSemester()
        {
            _scheduleWeekRepository.ScanScheduleSemester();
        }
    }
}
