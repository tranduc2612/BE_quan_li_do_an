using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectOutlineRepository _projectOutlineRepository;
        private readonly IStudentRepository _studentRepository;


        private readonly MappingProfile _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectService(IProjectRepository projectRepository, IHttpContextAccessor httpContextAccessor, IProjectOutlineRepository projectOutlineRepository, IStudentRepository studentRepository, MappingProfile mapper)
        {
            _projectRepository = projectRepository;
            _projectOutlineRepository = projectOutlineRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _studentRepository = studentRepository;
        }

        public ProjectOutlineDTO AddNewProjectOutline(ProjectOutlineDTO projectOutlineDTO)
        {
            Project project_find = _projectRepository.GetByUsername(projectOutlineDTO.UserName);
            if(project_find == null)
            {
                return null;
            }
            ProjectOutline new_project_outline = _mapper.MapProjectOutlineDTOToProjectOutline(projectOutlineDTO);
            return _mapper.MapProjectOutlineToProjectOutlineDTO(_projectOutlineRepository.Add(new_project_outline));
        }

        public List<Project> GetListProject(ProjectListModel data)
        {
            return _projectRepository.GetList(data);
        }

        public Project GetProjectByUsername(string username)
        {
            return _projectRepository.GetByUsername(username);
        }

        public ProjectOutline GetProjectOutlineByUsername(string username)
        {
            return _projectOutlineRepository.GetById(username);
        }
    }
}
