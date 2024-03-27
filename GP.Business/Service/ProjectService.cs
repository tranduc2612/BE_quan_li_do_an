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
        private readonly ITeacherRepository _teacherRepository;
        private readonly IStudentRepository _studentRepository;


        private readonly MappingProfile _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectService(IProjectRepository projectRepository, IHttpContextAccessor httpContextAccessor, IProjectOutlineRepository projectOutlineRepository, IStudentRepository studentRepository, ITeacherRepository teacherRepository, MappingProfile mapper)
        {
            _projectRepository = projectRepository;
            _projectOutlineRepository = projectOutlineRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _studentRepository = studentRepository;
            _teacherRepository= teacherRepository;
        }

        public bool AddNewProjectOutline(ProjectOutlineDTO projectOutlineDTO, out string message)
        {
            Project project_find = _projectRepository.GetProjectByUsername(projectOutlineDTO.UserName);
            if(project_find == null)
            {
                message = "Sinh viên không tồn tại";
                return false;
            }

            ProjectOutline projectOutline = _mapper.MapProjectOutlineDTOToProjectOutline(projectOutlineDTO);
            _projectOutlineRepository.Add(projectOutline);
            message = "Thêm đề cương đồ án thành công !";
            return true;
        }

        public bool AssignMentorTeacher(string username_student,string username_teacher, out string message)
        {
            Teacher teacherFind = _teacherRepository.Get(username_teacher);
            if(teacherFind == null)
            {
                message = "Giáo viên không tồn tại !";
                return false;
            }

            Project projectAssign = _projectRepository.AssignMentor(teacherFind, username_student);
            if(projectAssign == null)
            {
                message = "Đồ án không tồn tại !";
                return false;
            }

            message = "Gán giảng viên thành công !";
            return true;
        }

        public List<ProjectDTO> GetListProjectByUsernameMentor(string username, string semesterId)
        {
            List<Project> projectlst = _projectRepository.GetListProjectByUsernameMentor(username, semesterId);
            List<ProjectDTO> map = _mapper.MapProjectsToProjectDTOs(projectlst);

            return map;
        }

        public ProjectOutline GetProjectOutlineByUsername(string username)
        {
            return _projectOutlineRepository.GetById(username);
        }

        public bool UpdateNewProjectOutline(ProjectOutlineDTO projectOutlineDTO, out string message)
        {
            Project project_find = _projectRepository.GetProjectByUsername(projectOutlineDTO.UserName);
            if (project_find == null)
            {
                message = "Sinh viên không tồn tại";
                return false;
            }
            ProjectOutline outline_fine = _projectOutlineRepository.GetById(projectOutlineDTO.UserName);
            if (project_find == null)
            {
                message = "Sinh viên chưa tạo đề cương đồ án";
                return false;
            }
            outline_fine.NameProject = projectOutlineDTO.NameProject;
            outline_fine.ContentProject = projectOutlineDTO.ContentProject;
            outline_fine.TechProject = projectOutlineDTO.TechProject;
            outline_fine.ExpectResult = projectOutlineDTO.ExpectResult;
            outline_fine.PlantOutline = projectOutlineDTO.PlantOutline;

            _projectOutlineRepository.Update(outline_fine);
            message = "Cập nhật cương đồ án thành công !";
            return true;
        }
    }
}
