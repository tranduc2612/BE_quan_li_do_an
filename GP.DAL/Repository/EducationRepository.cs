using GP.Common.DTO;
using GP.DAL.IRepository;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class EducationRepository : IEducationRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        public EducationRepository(ManagementGraduationProjectContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string Add(Education req)
        {
            _dbContext.Add(req);
            _dbContext.SaveChanges();
            return req.EducationId;
        }

        public Education GetById(string id)
        {
            return _dbContext.Educations.FirstOrDefault(x => x.EducationId == id);
        }

        public List<Education> GetList(EducationDTO req)
        {
            return _dbContext.Educations.ToList();
        }

        public string Update(Education req)
        {
            _dbContext.Update(req);
            _dbContext.SaveChanges();
            return req.EducationId;
        }
    }
}
