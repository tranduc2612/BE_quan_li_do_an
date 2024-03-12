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
    public interface ISemesterRepository
    {
        public Semester GetById(string id);

        public List<Semester> GetList(SemesterDTO req);

        public bool Add(Semester req);
    }
}
