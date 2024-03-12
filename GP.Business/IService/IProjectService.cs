using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;

namespace GP.Business.IService
{
    public interface IProjectService
    {
        public Project GetProjectByUsername(string username);
        public ProjectOutline GetProjectOutlineByUsername(string username);
        public ProjectOutlineDTO AddNewProjectOutline(ProjectOutlineDTO projectOutlineDTO);
        public List<Project> GetListProject(ProjectListModel data);
    }
}
