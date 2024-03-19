using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface IScheduleSemesterRepository
    {
        public List<ScheduleSemester> GetListScheduleSemester(string semesterId);
        public ScheduleSemester GetById(string id);
        public string Add(ScheduleSemester req);
        public void Update(ScheduleSemester req);
        public void Delete(ScheduleSemester scheduleSemester);

    }
}
