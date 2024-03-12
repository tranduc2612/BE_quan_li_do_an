using GP.Common.DTO;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        public SemesterRepository(ManagementGraduationProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Semester req)
        {
            Semester semeterFind = GetById(req.SemesterId);
            if (semeterFind == null)
            {
                _dbContext.Semesters.Add(req);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
            
        }

        public Semester GetById(string id)
        {
            return _dbContext.Semesters.FirstOrDefault(x=>x.SemesterId == id);
        }

        public List<Semester> GetList(SemesterDTO req)
        {
            return _dbContext.Semesters.Where(x =>
                   (String.IsNullOrEmpty(req.NameSemester) || x.NameSemester.Contains(req.NameSemester))).ToList();
        }
    }
}
