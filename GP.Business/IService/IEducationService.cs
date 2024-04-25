using GP.Common.DTO;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IEducationService
    {
        public List<Education> GetList(EducationDTO req);
        public bool AddEducation(EducationDTO req, out string message);
        public bool UpdateEducation(EducationDTO req, out string message);

    }
}
