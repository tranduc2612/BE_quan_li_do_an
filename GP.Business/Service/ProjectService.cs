using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.DAL.Repository;
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
        private readonly IMajorRepository _majorRepository;
        private readonly IEducationRepository _educationRepository;


        private readonly MappingProfile _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectService(IProjectRepository projectRepository, IHttpContextAccessor httpContextAccessor, IProjectOutlineRepository projectOutlineRepository, IStudentRepository studentRepository, ITeacherRepository teacherRepository,IMajorRepository majorRepository,IEducationRepository educationRepository, MappingProfile mapper)
        {
            _projectRepository = projectRepository;
            _projectOutlineRepository = projectOutlineRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _studentRepository = studentRepository;
            _teacherRepository= teacherRepository;
            _educationRepository= educationRepository;
            _majorRepository= majorRepository;
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

        public bool AutomationAssignMentorTeacherToProject(string semesterId, out string message)
        {
            List<Project> projects = _projectRepository.GetListProjectBySemesterId(semesterId).Where(
                x => x.UserNameNavigation.Status == "AUTH" 
                && x.UserNameNavigation.IsDelete == 0 
                && x.UserNameNavigation.MajorId != null
                && x.UserNameMentor == null
                ).OrderByDescending(x=>x.UserNameNavigation.Gpa).ToList();

            List<Teacher> teachers = _teacherRepository.GetAllListOnly().Where(x=>x.IsDelete == 0 
            && x.Status == "AUTH" 
            && x.MajorId != null 
            && x.EducationId != null
            ).ToList();

            if(teachers.Count <= 0)
            {
                message = "Không có giảng viên đủ điều kiện để phân";
                return false;
            }
            if (projects.Count <= 0)
            {
                message = "Không có sinh viên đủ điều kiện để phân";
                return false;
            }


            // chúng ta sẽ lặp từ sinh viên có điểm GPA cao nhất đến sinh viên cuối cùng
            foreach (Project project in projects)
            {
                Student student = project.UserNameNavigation;

                var studentRegisterTeacher = student?.UserNameMentorRegister;
                //kiêm tra xem sinh viên đó đăng kí giảng viên nào
                if (studentRegisterTeacher != null)
                {
                    Teacher findTeacherRegister = teachers.Find(x => x.UserName == studentRegisterTeacher);
                    // kiểm tra xem giảng viên đó còn có thể hướng dẫn sinh viên nữa không
                    var studentCanMentor = findTeacherRegister?.Education?.MaxStudentMentor;
                    if (studentCanMentor != null)
                    {
                        int? remainingSlots = studentCanMentor - findTeacherRegister.ProjectUserNameMentorNavigations.Count;
                        if (remainingSlots > 0)
                        {
                            //findTeacherRegister.ProjectUserNameMentorNavigations.Add(project);
                            project.UserNameMentor = findTeacherRegister.UserName;
                            _projectRepository.Update(project);
                            continue;
                        }
                    }
                }

                if(student?.MajorId != null)
                {
                    // Lấy danh sách giảng viên thuộc chuyên ngành của sinh viên
                    // Ưu tiên phân phối sinh viên cho giảng viên trong chuyên ngành
                    List<Teacher> teachersInMajor = teachers.Where(t => t.MajorId == project.UserNameNavigation.MajorId).ToList();
                    int randomCount = 0;
                    if(teachersInMajor.Count > 0)
                    {
                        while (randomCount <= teachersInMajor.Count)
                        {
                            Random random = new Random();
                            int index = random.Next(0, teachersInMajor.Count);
                            Teacher teacherRand = teachersInMajor[index];

                            if (teacherRand != null)
                            {
                                int? remainingSlots = teacherRand.Education?.MaxStudentMentor - teacherRand.ProjectUserNameMentorNavigations.Count;
                                if (remainingSlots > 0)
                                {
                                    //teacher.ProjectUserNameMentorNavigations.Add(project);
                                    project.UserNameMentor = teacherRand.UserName;
                                    _projectRepository.Update(project);
                                    break;
                                }
                            }
                        }
                    }
                    
                    // nếu đã hết giảng viên thuộc chuyên ngành đó có thể hướng dẫn thì sinh viên đó sẽ phải đẩy sang đợt 2
                    // không thể chia tiếp ở đây vì nếu chia tiếp thì:
                    // có thể sinh viên này sẽ được xếp vào giảng viên chuyên ngành khác
                    // sẽ bị có thể bị mất cơ hội của bạn đăng ký đúng chuyên ngành
                }
            }

            // đợt 2 sẽ là đợt kiểm tra xem còn những sinh viên nào chưa có giảng viên hướng dẫn
            // đây sẽ là trường hợp 
            // + Giảng viên sinh viên đó đăng ký đã hết suất hướng dẫn
            // + Phải chia sinh viên này vào giảng viên sao cho phù hợp
            List<Project> projectsType2 = projects.Where(x=>x.UserNameMentor == null).OrderByDescending(x => x.UserNameNavigation.Gpa).ToList();
            List<Teacher> teacherType2 = teachers.Where(x => x.Education.MaxStudentMentor - x.ProjectUserNameMentorNavigations.Count > 0).ToList();

            if (projectsType2.Count > 0 && teacherType2.Count >0)
            {
                int currentTeacherIndex = 0;
                Teacher currentTeacher = teacherType2[currentTeacherIndex];
                foreach (Project project in projectsType2)
                {
                    if (currentTeacher.ProjectUserNameMentorNavigations.Count < currentTeacher.Education.MaxStudentMentor)
                    {
                        //currentTeacher.ProjectUserNameMentorNavigations.Add(project);
                        project.UserNameMentor = currentTeacher.UserName;
                        _projectRepository.Update(project);
                    }
                    else
                    {
                        currentTeacherIndex++;
                        if(teacherType2.Count == currentTeacherIndex)
                        {
                            break;
                        }
                        currentTeacher = teacherType2[currentTeacherIndex];
                        //currentTeacher.ProjectUserNameMentorNavigations.Add(project);
                        project.UserNameMentor = currentTeacher.UserName;
                        _projectRepository.Update(project);
                    }
                }
            }

            // đợt 3 sẽ là sinh viên cuối khi mà tất cả giảng viên đều đã đủ hết số lượng sinh viên
            // sẽ random trong tất cả các giảng viên để thêm vào
            List<Project> projectsType3 = projects.Where(x => x.UserNameMentor == null).ToList();
            
            if(projectsType3.Count > 0)
            {
                int count = 0;
                Random random = new Random();
                foreach (Project project in projectsType3)
                {

                    // Chọn một giảng viên ngẫu nhiên từ danh sách
                    int randomIndex = random.Next(teachers.Count);
                    Teacher randomTeacher = teachers[randomIndex];
                    // Gán giảng viên cho dự án
                    project.UserNameMentor = randomTeacher.UserName;
                    //randomTeacher.ProjectUserNameMentorNavigations.Add(project);
                    _projectRepository.Update(project);
                }
            }

            message = "Hoàn thiện thuật toán";
            return true;
        }

        public bool CallProcAutomationAssignMentorTeacherToProject(string semesterId, out string message)
        {
            _projectRepository.CallProcAutoMationAssignMentor(semesterId);
            message = "oke";
            return true;
        }

        public List<ProjectDTO> GetListProjectByGroupId(ProjectOutlineListModel req)
        {
            //return _mapper.MapProjectsToProjectDTOs(_projectRepository.GetListProjectByGroupId(req.SemesterId, req.GroupReviewOutlineId));
            return _projectRepository.GetListProjectByGroupId(req.SemesterId, req.GroupReviewOutlineId);

        }

        public List<ProjectDTO> GetListProjectByUsernameMentor(string username, string semesterId)
        {
            List<Project> projectlst = _projectRepository.GetListProjectByUsernameMentor(username, semesterId);
            List<ProjectDTO> map = _mapper.MapProjectsToProjectDTOs(projectlst);

            return map;
        }

        public ProjectDTO GetProjectByHashKeyCommentator(string key)
        {
            return _mapper.MapProjectToProjectDTO(_projectRepository.GetProjectByHashKeyCommentator(key));
        }

        public ProjectDTO GetProjectByHashKeyMentor(string key)
        {
            return _mapper.MapProjectToProjectDTO(_projectRepository.GetProjectByHashKeyMentor(key));
        }

        public ProjectDTO GetProjectByUserName(string username)
        {
            return _mapper.MapProjectToProjectDTO(_projectRepository.GetProjectByUsernameData(username));
        }

        public ProjectOutline GetProjectOutlineByUsername(string username)
        {
            return _projectOutlineRepository.GetById(username);
        }

        public Project HandleUploadFinalFile(ProjectFinalFile data, out string message, out bool check)
        {
            Project find = _projectRepository.GetProjectByUsername(data.UserName);
            if(find == null)
            {
                check = false;
                message = "Sinh viên không tồn tại";
                return null;
            }
            if (find != null && data.Function == "D")
            {
                if (File.Exists(Path.Combine("file", "final", data.UserName, find.NameFileFinal ?? "no_name")))
                {
                    File.Delete(Path.Combine("file", "final", data.UserName, find.NameFileFinal ?? "no_name"));
                }
                find.NameFileFinal = null;
                find.TypeFileFinal = null;
                find.SizeFileFinal = null;
                
                _projectRepository.Update(find);
                check = true;
                message = "Xóa thành công";
                return null;
            }
            if (data.file == null || data.file.Length == 0)
            {
                check = false;
                message = "file không hợp lệ !";
                return null;
            }
            var randomName = $"{Path.GetFileNameWithoutExtension(data.file.FileName)}_{DateTime.Now.ToString("yyyyMMddHHmmss")}_{new Random().Next(1000, 9999)}_{Path.GetExtension(data.file.FileName)}";
            if (!Directory.Exists(Path.Combine("file", "final", data.UserName)))
            {
                // Tạo thư mục cha nếu nó chưa tồn tại
                Directory.CreateDirectory(Path.Combine("file", "final", data.UserName));
            }
            var filePath = Path.Combine("file", "final", data.UserName, randomName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                data.SizeFileFinal = data.file.Length.ToString();
                data.NameFileFinal = randomName;
                data.TypeFileFinal = data.file.ContentType.ToString();
                if(data.Function == "U" || data.Function == "C")
                {
                    if (data.Function == "U")
                    {
                        if (File.Exists(Path.Combine("file", "final", data.UserName, find.NameFileFinal ?? "no_name")))
                        {
                            File.Delete(Path.Combine("file", "final", data.UserName, find.NameFileFinal ?? "no_name"));
                        }
                    }
                    find.NameFileFinal = data.NameFileFinal;
                    find.SizeFileFinal = data.SizeFileFinal;
                    find.TypeFileFinal = data.TypeFileFinal;
                    check = true;
                    message = "Cập nhật thành công";
                    Project new_data = _projectRepository.Update(find);
                    data.file.CopyTo(stream);
                    return new_data;
                }
                check = false;
                message = "Chức năng không hợp lệ !";
                return null;
            }
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
            if (project == null)
            {
                message = "Đồ án không tồn tại!";
                return false;
            }

            bool successParse = double.TryParse(score, out double resultScore);
            if (!successParse)
            {
                message = "Điểm không hợp lệ!";
                return false;
            }

            switch (role)
            {
                case "CT":
                    project.ScoreCt = resultScore;
                    project.CommentCt = comment;
                    break;
                case "TK":
                    project.ScoreTk = resultScore;
                    project.CommentTk = comment;
                    break;
                case "UV1":
                    project.ScoreUv1 = resultScore;
                    project.CommentUv1 = comment;
                    break;
                case "UV2":
                    project.ScoreUv2 = resultScore;
                    project.CommentUv2 = comment;
                    break;
                case "UV3":
                    project.ScoreUv3 = resultScore;
                    project.CommentUv3 = comment;
                    break;
                case "MENTOR":
                    project.ScoreMentor = resultScore;
                    project.CommentMentor = comment;
                    break;
                case "COMMENTATOR":
                    project.ScoreCommentator = resultScore;
                    project.CommentCommentator = comment;
                    break;
                default:
                    message = "Vai trò không hợp lệ!";
                    return false;
            }

            double totalHD = ((project.ScoreCt ?? 0) + (project.ScoreTk ?? 0) + (project.ScoreUv1 ?? 0) + (project.ScoreUv2 ?? 0) + (project.ScoreUv3 ?? 0)) / 5;
            double totalQT = ((project.ScoreMentor ?? 0) + (project.ScoreCommentator ?? 0)) / 2;
            double total = (totalHD * 0.7 + totalQT * 0.3);
            project.ScoreFinal = Math.Round(total, 2);
            _projectRepository.Update(project);

            message = "Cập nhật đồ án thành công!";
            return true;
        }
    }
}
