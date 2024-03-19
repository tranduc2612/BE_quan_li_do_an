using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.Models
{
    public class PaginatedResultBase<T>
    {
        public int PageIndex { get; set; }
        public int TotalItem { get; set; }
        public List<T> ListResult { get; set; }

        public PaginatedResultBase()
        {
            ListResult = new List<T>();
        }

        public PaginatedResultBase(int pageIndex, int totalPage, List<T> listResult)
        {
            PageIndex = pageIndex;
            TotalItem = totalPage;
            ListResult = listResult;
        }
    }
}
