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
    public interface IMajorService
    {
        public List<Major> GetList(MajorDTO req);
        public bool AddMajor(MajorDTO req,out string message);
        public bool UpdateMajor(MajorDTO req, out string message);
    }
}
