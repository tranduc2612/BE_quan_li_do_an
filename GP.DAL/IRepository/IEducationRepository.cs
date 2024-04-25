using GP.Common.DTO;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface IEducationRepository
    {
        public List<Education> GetList(EducationDTO req);
        public Education GetById(string id);
        public string Add(Education req);
        public string Update(Education req);
    }
}
