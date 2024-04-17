using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            project_find.StatusProject = "DOING";
            ProjectOutline projectOutline = _mapper.MapProjectOutlineDTOToProjectOutline(projectOutlineDTO);
            _projectOutlineRepository.Add(projectOutline);
            _projectRepository.Update(project_find);
            message = "Thêm đề cương đồ án thành công !";
            return true;
        }

        public bool AssignMentorTeacherToProject(string username_student,string username_teacher, out string message)
        {
            Teacher teacherFind = _teacherRepository.Get(username_teacher);
            if(teacherFind == null)
            {
                message = "Giáo viên không tồn tại !";
                return false;
            }

            Project projectAssign = _projectRepository.GetProjectByUsername(username_student);
            if(projectAssign == null)
            {
                message = "Đồ án không tồn tại !";
                return false;
            }
            projectAssign.UserNameMentor = username_teacher;
            _projectRepository.Update(projectAssign);
            message = "Gán giảng viên thành công !";
            return true;
        }

        public bool AssignUserNameCommentatorToProject(string username_student, string username_teacher, out string message)
        {
            Project find_project = _projectRepository.GetProjectByUsername(username_student);
            if (find_project == null)
            {
                message = "Sinh viên này không tồn tại !";
                return false;
            }
            Teacher find_teacher = _teacherRepository.Get(username_teacher);
            if (find_teacher == null)
            {
                message = "Giảng viên này không tồn tại !";
                return false;
            }
            find_project.UserNameCommentator = find_teacher.UserName;
            _projectRepository.Update(find_project);
            message = "Gán giảng viên phản biện thành công !";
            return true;

        }

        public List<ProjectDTO> GetListProjectByUsernameMentor(string username, string semesterId)
        {
            List<Project> projectlst = _projectRepository.GetListProjectByUsernameMentor(username, semesterId);
            List<ProjectDTO> map = _mapper.MapProjectsToProjectDTOs(projectlst);

            return map;
        }

        public ProjectDTO GetProjectByHashKeyCommentator(string key)
        {
            return _mapper.MapProjectToProjectDTO(_projectRepository.GetProjectByHashKeyMentor(key));
        }

        public ProjectDTO GetProjectByHashKeyMentor(string key)
        {
            return _mapper.MapProjectToProjectDTO(_projectRepository.GetProjectByHashKeyCommentator(key));
        }

        public ProjectDTO GetProjectByUserName(string username)
        {
            return _mapper.MapProjectToProjectDTO(_projectRepository.GetProjectByUsernameData(username));
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

        public bool UpdateScoreToProject(string username, string role, string score, string comment, out string message)
        {
            Project project = _projectRepository.GetProjectByUsername(username);
            if(project == null)
            {
                message = "Đồ án không tồn tại !";
                return false;
            }
            bool successParse = double.TryParse(score, out double resultScore);
            if(!successParse)
            {
                message = "Điểm không hợp lệ !";
                return false;
            }
            if (role == "CT")
            {
                project.ScoreCt = resultScore;
                project.CommentCt = comment;
                double totalHD = ((project.ScoreCt ?? 0) + (project.ScoreTk ?? 0) + (project.ScoreUv1 ?? 0) + (project.ScoreUv2 ?? 0) + (project.ScoreUv3 ?? 0)) / 5;
                double totalQT = ((project.ScoreMentor ?? 0) + (project.ScoreCommentator ?? 0)) / 2;
                double total = (totalHD + totalQT) / 2;
                project.ScoreFinal = Math.Round(total,2);
                _projectRepository.Update(project);
                message = "Cập nhật đồ án thành công !";
                return true;
            }
            if (role == "TK")
            {
                project.ScoreTk = resultScore;
                project.CommentTk = comment;
                double totalHD = ((project.ScoreCt ?? 0) + (project.ScoreTk ?? 0) + (project.ScoreUv1 ?? 0) + (project.ScoreUv2 ?? 0) + (project.ScoreUv3 ?? 0)) / 5;
                double totalQT = ((project.ScoreMentor ?? 0) + (project.ScoreCommentator ?? 0)) / 2;
                double total = (totalHD + totalQT) / 2;
                project.ScoreFinal = Math.Round(total,2);
                _projectRepository.Update(project);
                message = "Cập nhật đồ án thành công !";
                return true;
            }
            if (role == "UV1")
            {
                project.ScoreUv1 = resultScore;
                project.CommentUv1 = comment;
                double totalHD = ((project.ScoreCt ?? 0) + (project.ScoreTk ?? 0) + (project.ScoreUv1 ?? 0) + (project.ScoreUv2 ?? 0) + (project.ScoreUv3 ?? 0)) / 5;
                double totalQT = ((project.ScoreMentor ?? 0) + (project.ScoreCommentator ?? 0)) / 2;
                double total = (totalHD + totalQT) / 2;
                project.ScoreFinal = Math.Round(total,2);
                _projectRepository.Update(project);
                message = "Cập nhật đồ án thành công !";
                return true;
            }
            if (role == "UV2")
            {
                project.ScoreUv2 = resultScore;
                project.CommentUv2 = comment;
                double totalHD = ((project.ScoreCt ?? 0) + (project.ScoreTk ?? 0) + (project.ScoreUv1 ?? 0) + (project.ScoreUv2 ?? 0) + (project.ScoreUv3 ?? 0)) / 5;
                double totalQT = ((project.ScoreMentor ?? 0) + (project.ScoreCommentator ?? 0)) / 2;
                double total = (totalHD + totalQT) / 2;
                project.ScoreFinal = Math.Round(total,2);
                _projectRepository.Update(project);
                message = "Cập nhật đồ án thành công !";
                return true;
            }
            if (role == "UV3")
            {
                project.ScoreUv3 = resultScore;
                project.CommentUv3 = comment;
                double totalHD = ((project.ScoreCt ?? 0) + (project.ScoreTk ?? 0) + (project.ScoreUv1 ?? 0) + (project.ScoreUv2 ?? 0) + (project.ScoreUv3 ?? 0)) / 5;
                double totalQT = ((project.ScoreMentor ?? 0) + (project.ScoreCommentator ?? 0)) / 2;
                double total = (totalHD + totalQT) / 2;
                project.ScoreFinal = Math.Round(total,2);
                _projectRepository.Update(project);
                message = "Cập nhật đồ án thành công !";
                return true;
            }
            if (role == "MENTOR")
            {
                project.ScoreMentor = resultScore;
                project.CommentMentor = comment;
                double totalHD = ((project.ScoreCt ?? 0) + (project.ScoreTk ?? 0) + (project.ScoreUv1 ?? 0) + (project.ScoreUv2 ?? 0) + (project.ScoreUv3 ?? 0)) / 5;
                double totalQT = ((project.ScoreMentor ?? 0) + (project.ScoreCommentator ?? 0)) / 2;
                double total = (totalHD + totalQT) / 2;
                project.ScoreFinal = Math.Round(total, 2);
                _projectRepository.Update(project);
                message = "Cập nhật đồ án thành công !";
                return true;
            }
            if (role == "COMMENTATOR")
            {
                project.ScoreCommentator = resultScore;
                project.CommentCommentator = comment;
                double totalHD = ((project.ScoreCt ?? 0) + (project.ScoreTk ?? 0) + (project.ScoreUv1 ?? 0) + (project.ScoreUv2 ?? 0) + (project.ScoreUv3 ?? 0)) / 5;
                double totalQT = ((project.ScoreMentor ?? 0) + (project.ScoreCommentator ?? 0)) / 2;
                double total = (totalHD + totalQT) / 2;
                project.ScoreFinal = Math.Round(total, 2);
                _projectRepository.Update(project);
                message = "Cập nhật đồ án thành công !";
                return true;
            }
            message = "Vai trò không hợp lệ !";
            return false;
        }
    }
}
