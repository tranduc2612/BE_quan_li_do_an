using GP.Common.DTO;
using GP.Common.Models;
using GP.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface ICouncilRepository
    {
        Council GetById(string id);
        List<CouncilDTO> GetList(CouncilModel req);
        public Council Add(Council council);
        public Council Update(Council council);
        public void Delete(Council council);
        public List<Teaching> GetListTeachingNotInCouncil(TeachingListModel data);
        public List<Teaching> GetListTeachingInCouncil(TeachingListModel data);


    }
}
