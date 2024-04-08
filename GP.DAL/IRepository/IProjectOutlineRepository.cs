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
    public interface IProjectOutlineRepository
    {
        public ProjectOutline Add(ProjectOutline data);
        public ProjectOutline Update(ProjectOutline data);

        public ProjectOutline GetById(string username);
        public List<ProjectOutlineDTO> GetListProjectOutlineInGroup(ProjectOutlineListModel req);
        public List<ProjectOutlineDTO> GetListProjectOutline(ProjectOutlineListModel req);
    }
}
