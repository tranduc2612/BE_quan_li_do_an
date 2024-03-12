using GP.Common.DTO;
using GP.Common.Models;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface ITeacherRepository
    {
        public Teacher Get(string username);
        public PaginatedResultBase<TeacherDTO> GetList(TeacherListModel data);
        public Teacher Create(Teacher teacher);
        public Teacher Update(Teacher teacher);
        public bool Delete(string username);
    }
}
