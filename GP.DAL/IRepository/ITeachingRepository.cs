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
    public interface ITeachingRepository
    {
        public Teaching Get(string username,string semesterId);
        public List<Teaching> GetList(TeachingListModel data);
        public List<Teaching> GetListByGroupReviewId(TeachingListModel data);

    }
}
