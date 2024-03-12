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
    public interface IStudentRepository
    {
        public Student Get(string username);
        public PaginatedResultBase<StudentDTO> GetList(StudentListModel data);
        public Student Create(StudentModel student);
        public Student Update(Student student);
        public bool Delete(string username);
    }
}
