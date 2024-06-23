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
    public class ScheduleSemesterRepository : IScheduleSemesterRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        public ScheduleSemesterRepository(ManagementGraduationProjectContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string Add(ScheduleSemester req)
        {
            req.ScheduleSemesterId = Guid.NewGuid().ToString();
            _dbContext.ScheduleSemesters.Add(req);
            _dbContext.SaveChanges();
            return req.ScheduleSemesterId.ToString();
        }

        public void Delete(ScheduleSemester scheduleSemester)
        {
            _dbContext.ScheduleSemesters.Remove(scheduleSemester);
            _dbContext.SaveChanges();
        }

        public ScheduleSemester GetById(string id)
        {
            return _dbContext.ScheduleSemesters.Include(x=>x.CreatedByNavigation).FirstOrDefault(x => x.ScheduleSemesterId == id);
        }

        public List<ScheduleSemester> GetListScheduleSemester(string semesterId)
        {
            return _dbContext.ScheduleSemesters.Include(x => x.CreatedByNavigation).Include(x => x.Semester).Where(x=>x.SemesterId==semesterId).OrderBy(x=>x.CreatedDate).ToList();
        }

        public void Update(ScheduleSemester req)
        {
            _dbContext.ScheduleSemesters.Update(req);
            _dbContext.SaveChanges();
        }
    }
}
