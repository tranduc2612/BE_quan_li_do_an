using GP.Common.DTO;
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
    public class MajorRepository : IMajorRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        public MajorRepository(ManagementGraduationProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Major req)
        {
            _dbContext.Majors.Add(req);
            _dbContext.SaveChanges();
            return true;
        }
        public bool Update(Major req)
        {
            _dbContext.Majors.Update(req);
            _dbContext.SaveChanges();
            return true;
        }
        public Major Get(MajorDTO req)
        {
            return _dbContext.Majors.FirstOrDefault(x => x.MajorId == req.MajorId);
        }

        public List<Major> GetList(MajorDTO req)
        {
            return _dbContext.Majors.Where(x =>
                    (String.IsNullOrEmpty(req.MajorId) || x.MajorId.Contains(req.MajorId)) &&
                    (String.IsNullOrEmpty(req.MajorName) || x.MajorName.Contains(req.MajorName))).ToList();
        }
    }
}
