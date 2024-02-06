using GP.Common.Models;
using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface ICreditRepository
    {
        public Credit CreateCredit();

        public PaginatedResultBase<Credit> GetListCreditByFilter(SearchBase searchBase, string username = "");
    }
}
