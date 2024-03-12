using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface IClassificationRepository
    {
        public List<Classification> GetListByTypeCode(string code);
        public Classification GetByCode(string code);
    }
}
