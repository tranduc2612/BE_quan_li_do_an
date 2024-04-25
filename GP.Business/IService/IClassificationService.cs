using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IClassificationService
    {
        public List<Classification> GetListClassification(string code);
        public Classification GetClassification(string type_code,string code);
        public Classification Update(Classification model);
    }
}
