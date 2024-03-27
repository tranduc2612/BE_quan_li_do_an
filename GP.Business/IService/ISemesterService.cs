using GP.Common.DTO;
using GP.Common.Models;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface ISemesterService
    {
        public Semester getSemester(string id);
        public List<Semester> GetListSemester(SemesterDTO req);
        public bool Add(SemesterDTO req, out string message);
        public PaginatedResultBase<SemesterDTO> GetListSemesterPage(SemesterListModel data);
    }
}
