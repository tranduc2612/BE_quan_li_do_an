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
    public interface IMajorRepository
    {

        public List<Major> GetList(MajorDTO req);
        public Major Get(MajorDTO req);
        public bool Add(Major req);
        public bool Update(Major req);
    }
}
