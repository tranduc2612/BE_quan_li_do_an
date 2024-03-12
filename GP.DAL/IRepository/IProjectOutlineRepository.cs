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
        public ProjectOutline GetById(string username);

    }
}
