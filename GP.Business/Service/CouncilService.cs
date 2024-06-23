using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Spreadsheet;
using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.DAL.Repository;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class CouncilService : ICouncilService
    {
        private readonly ICouncilRepository _councilRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ITeachingRepository _teachingRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly MappingProfile _mapper;
        public CouncilService(ICouncilRepository councilRepository, ISemesterRepository semesterRepository,IProjectRepository projectRepository ,ITeachingRepository teachingRepository, ITeacherRepository teacherRepository, MappingProfile mapper)
        {
            _councilRepository = councilRepository;
            _semesterRepository= semesterRepository;
            _teachingRepository= teachingRepository;
            _teacherRepository= teacherRepository;
            _projectRepository= projectRepository;
            _mapper = mapper;
        }
        public bool AddCouncil(CouncilModel req, out string message)
        {
            Semester semester = _semesterRepository.GetById(req.SemesterId);
            if (semester == null)
            {
                message = "Học kỳ này không tồn tại";
                return false;
            }
            Council data = _mapper.MapCouncilModelToCouncil(req);
            message = "Thêm thành công !";
            _councilRepository.Add(data);
            return true;
        }

        public List<CouncilDTO> GetList(CouncilModel req)
        {
            return _councilRepository.GetList(req);
        }

        public bool UpdateCouncil(CouncilModel req, out string message)
        {
            Council find = _councilRepository.GetById(req.CouncilId);
            if (find == null)
            {
                message = "Hội đồng này không tồn tại";
                return false;
            }
            Semester semester = _semesterRepository.GetById(req.SemesterId);
            if (semester == null)
            {
                message = "Học kỳ này không tồn tại";
                return false;
            }
            find.CouncilName = req.CouncilName;
            find.CouncilZoom = req.CouncilZoom;
            message = "Cập nhật thành công !";
            _councilRepository.Update(find);
            return true;
        }

        public bool DeleteCouncil(string id, out string message)
        {
            Council council = _councilRepository.GetById(id);
            if (council == null)
            {
                message = "Không tồn tại hội đồng này !";
                return false;
            }
            _councilRepository.Delete(council);

            message = "Xóa thành công !";
            return true;
        }

        public bool AssignTeachingToCouncil(AssignTeachingCouncilModel model, out string message)
        {
            Semester semester_find = _semesterRepository.GetById(model.SemesterTeachingId);
            if (semester_find == null)
            {
                message = "Học kỳ này không tồn tại !";
                return false;
            }
            Council council_find = _councilRepository.GetById(model.CouncilId);
            if (council_find == null)
            {
                message = "Hội đồng này không tồn tại !";
                return false;
            }
            Teacher teacher_find = _teacherRepository.Get(model.UsernameTeaching);
            if (teacher_find == null)
            {
                message = "Giảng viên này không tồn tại !";
                return false;
            }
            Teaching teaching_find = _teachingRepository.GetByUserNameSemester(model.UsernameTeaching, model.SemesterTeachingId);
            if (teaching_find == null)
            {
                message = "Giảng viên này chưa được thêm vào học kỳ này !";
                return false;
            }
            TeachingListModel model_teaching = new TeachingListModel();
            model_teaching.SemesterId = model.SemesterTeachingId;
            model_teaching.PositionInCouncil = model.PositionInCouncil;
            model_teaching.CouncilId = model.CouncilId;
            Teaching checked_have_assign_before = _teachingRepository.GetDetail(model_teaching);
            if(checked_have_assign_before != null)
            {
                checked_have_assign_before.CouncilId = null;
                checked_have_assign_before.PositionInCouncil = null;
                _teachingRepository.Update(checked_have_assign_before);
            }
            teaching_find.PositionInCouncil = model.PositionInCouncil;
            teaching_find.CouncilId = model.CouncilId;
            _teachingRepository.Update(teaching_find);
            message = "Gán giảng viên thành công !";
            return true;
        }

        public bool AssignProjectToCouncil(AssignProjectCouncilModel model, out string message)
        {
            Council council_find = _councilRepository.GetById(model.CouncilId);
            if (council_find == null)
            {
                message = "Hội đồng này không tồn tại !";
                return false;
            }
            List<Project> lstProject = _projectRepository.GetListProjectByCouncilId(model.SemesterId,model.CouncilId);

            foreach (string username in model.UsernameProjects)
            {
                Project project_find = _projectRepository.GetProjectByUsername(username);
                if (project_find == null)
                {
                    message = "Sinh viên Không tồn tại !";
                    return false;
                }
                project_find.CouncilId = model.CouncilId;
                _projectRepository.Update(project_find);
            }

            foreach (Project project_find in lstProject)
            {
                if (!model.UsernameProjects.Any(x => project_find.UserName == x))
                {
                    Project find = _projectRepository.GetProjectByUsername(project_find.UserName);
                    find.CouncilId = null;
                    _projectRepository.Update(find);
                }
            }

            message = "Gán sinh viên vào hội đồng thành công !";
            return true;
        }



        public Council GetCoucnil(string id)
        {
            return _councilRepository.GetById(id);
        }

        public List<TeachingDTO> getListTeachingNotInCouncil(TeachingListModel data)
        {
            return _mapper.MapTeachingsToTeachingDTOs(_councilRepository.GetListTeachingNotInCouncil(data));
        }

        public List<ProjectDTO> getListProjectInCouncil(StudentCouncilListModel data)
        {
            return _mapper.MapProjectsToProjectDTOs(_projectRepository.GetListProjectByCouncilId(data.SemesterId, data.CouncilId));
        }

        public Teaching getTeaching(string username, string semesterId)
        {
            return _teachingRepository.GetByUserNameSemester(username, semesterId);
        }

        public bool AutoAssignTeachingToCouncil(string semesterId, string currentUsername, out string message)
        {
            // Tạo một danh sách chứa các Teaching đã được sắp xếp ngẫu nhiên
            List<Teaching> shuffledTeachings = _teachingRepository
                .GetListTeachingBySemesterId(semesterId)
                .Where(x=>x.UserNameTeacherNavigation.IsDelete == 0 && x.UserNameTeacherNavigation.Status == "AUTH" && x.CouncilId == null)
                .OrderBy(x => Guid.NewGuid()).ToList();
            
            List<Council> councils = _councilRepository.GetListBySemesterId(semesterId).Where(x=>x.IsDelete == 0).ToList();
            if(shuffledTeachings == null)
            {
                message = "Không có giảng viên !";
                return true;
            }
            if (councils == null)
            {
                message = "Không có Hội đồng !";
                return true;
            }

            int currentIndex = 0;
            int councilIndex = 0;

            var availablePositions = new List<string> { "CT", "TK", "UV1", "UV2", "UV3" };

            while (currentIndex < shuffledTeachings.Count)
            {
                if (councils[councilIndex].Teachings.Count >= 5)
                {
                    // chuyển qua hội đồng khác
                    councilIndex++;

                    // nếu không còn hội đồng nào thì tạo
                    if (councilIndex >= councils.Count)
                    {
                        // Nếu không còn council nào có sẵn, tạo thêm một council mới
                        Council newCouncil = new Council();
                        newCouncil.CouncilName = "Hội đồng " + councils.Count + 1;
                        newCouncil.CreatedBy = currentUsername;
                        newCouncil.SemesterId = semesterId;
                        newCouncil.IsDelete = 0;
                        // Thêm council mới vào danh sách councils
                        councils.Add(newCouncil);
                        _councilRepository.Add(newCouncil);
                        councilIndex = councils.Count - 1;
                    }

                    // Reset danh sách các vị trí còn trống cho council mới
                    availablePositions = new List<string> { "CT", "TK", "UV1", "UV2", "UV3" };
                }

                // Lấy teaching hiện tại từ danh sách shuffledTeachings
                Teaching currentTeaching = shuffledTeachings[currentIndex];

                // Lọc ra các vị trí đã được gán trong council hiện tại
                var occupiedPositions = councils[councilIndex].Teachings.Select(t => t.PositionInCouncil);

                // Ưu tiên gán vị trí "CT" và "TK" trước
                string selectedPosition = null;
                if (!occupiedPositions.Contains("CT"))
                {
                    selectedPosition = "CT";
                }
                else if (!occupiedPositions.Contains("TK"))
                {
                    selectedPosition = "TK";
                }
                else
                {
                    // Nếu không có "CT" hoặc "TK" đã được gán, chọn vị trí còn lại theo thứ tự
                    selectedPosition = availablePositions.FirstOrDefault(p => !occupiedPositions.Contains(p));
                }

                // Kiểm tra xem còn vị trí nào còn trống không
                if (selectedPosition == null)
                {
                    // Nếu không còn vị trí trống, tiến hành chuyển sang council tiếp theo
                    continue;
                }


                // Gán vị trí cho teaching hiện tại
                currentTeaching.PositionInCouncil = selectedPosition;
                // Gán council cho teaching hiện tại
                currentTeaching.CouncilId = councils[councilIndex].CouncilId;
                _teachingRepository.Update(currentTeaching);

                // Loại bỏ vị trí đã được gán ra khỏi danh sách các vị trí còn trống
                availablePositions.Remove(selectedPosition);
                // Tăng chỉ số cho teaching tiếp theo
                currentIndex++;
            }
            message = "Phân giảng viên vào hội đồng thành công !";
            return true;

        }

        public bool AutoAssignProjectToCouncil(string semesterId,string currentUsername, out string message)
        {
            // tự động gán giảng viên vào hội đồng
            bool AssignTeachingToCouncil = AutoAssignTeachingToCouncil(semesterId, currentUsername, out string messageTeaching);
            

            if (AssignTeachingToCouncil)
            {
                // gán sinh viên vào hội đồng
                List<Project> projects = _projectRepository
                    .GetListProjectBySemesterId(semesterId)
                    .Where(x=>x.UserNameMentor != null 
                              && 
                              x.UserNameNavigation?.Status == "AUTH"
                              &&
                              x.CouncilId == null
                              &&
                              x.StatusProject == "ACCEPT"
                              //&&
                              //x.ProjectOutline != null
                           )
                    .ToList();
                List<Council> councils = _councilRepository.GetListBySemesterId(semesterId).Where(x=>x.IsDelete == 0 && x.Teachings.Count > 0).ToList();

                int maxTeachingsPerCouncil = projects.Count / councils.Count;

                foreach (Council council in councils)
                {
                    HashSet<string> allCouncilTeachers = new HashSet<string>();
                    foreach (var teaching in council.Teachings)
                    {
                        allCouncilTeachers.Add(teaching.UserNameTeacher);
                    }
                    // lấy ra danh sách các sinh viên không có giảng viên hướng dẫn trong hội đồng
                    List<Project> filteredProjects = projects
                        .Where(project => project.UserNameMentor != null 
                        && !allCouncilTeachers.Contains(project.UserNameMentor) 
                        && project.CouncilId == null)
                        .ToList();
                    int limitZoom = Math.Min(maxTeachingsPerCouncil, filteredProjects.Count);
                    for (int i=0;i<limitZoom;i++)
                    {
                        filteredProjects[i].CouncilId = council.CouncilId;
                        _projectRepository.Update(filteredProjects[i]);
                    }

                    //projects = projects
                    //    .Where(project => project.UserNameMentor != null && allCouncilTeachers.Contains(project.UserNameMentor))
                    //    .ToList();

                }


                // gán toàn bộ sinh viên còn sót lại vào trong hội đồng còn chống
                List<Project> projectLeft = _projectRepository
                    .GetListProjectBySemesterId(semesterId)
                    .Where(x => x.UserNameMentor != null
                              &&
                              x.UserNameNavigation?.Status == "AUTH"
                              &&
                              x.CouncilId == null
                              &&
                              x.StatusProject == "ACCEPT"
                           //&&
                           //x.ProjectOutline != null
                           )
                    .ToList();

                
                foreach(Project project in projectLeft)
                {
                    // Tạo một danh sách các hội đồng không chứa giáo viên hướng dẫn của dự án
                    var eligibleCouncils = councils.Where(council =>
                        !council.Teachings.Any(teaching => teaching.UserNameTeacher == project.UserNameMentor)
                    ).ToList();

                    if (eligibleCouncils.Any())
                    {
                        // Nếu có hội đồng thỏa mãn, chọn một trong số chúng để gán dự án vào
                        Random random = new Random();
                        int index = random.Next(0, eligibleCouncils.Count);
                        Council selectedCouncil = eligibleCouncils[index];

                        // Gán dự án vào hội đồng đã chọn
                        project.CouncilId = selectedCouncil.CouncilId;
                    }
                    else
                    {
                        // Nếu không có hội đồng nào thỏa mãn, chọn một hội đồng ngẫu nhiên từ danh sách các hội đồng
                        Random random = new Random();
                        int index = random.Next(0, councils.Count);
                        Council selectedCouncil = councils[index];

                        // Gán dự án vào hội đồng đã chọn
                        project.CouncilId = selectedCouncil.CouncilId;
                    }


                    _projectRepository.Update(project);
                }



                
            }
            // gán giảng viên phản biện
            AutoAssignTeacherCommentator(semesterId, currentUsername, out string messageCommentator);
            message = "Phân chia giảng viên và sinh viên thành công !";
            return true;
        }

        public bool AutoAssignTeacherCommentator(string semesterId, string currentUsername, out string message)
        {
            List<Council> councils = _councilRepository.GetListBySemesterId(semesterId).Where(x => x.IsDelete == 0).ToList();
            if(councils.Count == 0)
            {
                message = "Hội đồng không hợp lệ !";
                return false;
            }
            // chia giảng viên phản biện cho sinh viên trong hội đồng
            foreach(Council item in councils) {
                List<Teaching> teachings = item.Teachings.ToList();
                List<Project> projectsInCouncil = _projectRepository.GetListProjectByCouncilId(semesterId,item.CouncilId);

                int numberOfProjects = projectsInCouncil.Count;
                int numberOfTeachers = teachings.Count;
                if (numberOfTeachers == 0)
                {
                    continue;
                }

                // Chia đều các giáo viên vào các dự án
                int teacherIndex = 0;
                for (int i = 0; i < numberOfProjects; i++)
                {
                    // Lấy giáo viên tiếp theo
                    string currentTeacher = teachings[teacherIndex].UserNameTeacher;

                    // Gán giáo viên vào dự án
                    projectsInCouncil[i].UserNameCommentator = currentTeacher;

                    _projectRepository.Update(projectsInCouncil[i]);
                    // Chuyển sang giáo viên tiếp theo trong danh sách
                    teacherIndex = (teacherIndex + 1) % numberOfTeachers;
                }

            }


            // chia giảng viên phản biện cho sinh viên bảo lưu
            List<Project> projectPause  = _projectRepository.GetListProjectBySemesterId(semesterId).Where(x=>x.StatusProject == "PAUSE").ToList();
            List<Teacher> teachers = _teacherRepository.GetAllListOnly();
            if (projectPause.Count > 0)
            {
                foreach(Project project in projectPause)
                {
                    Random random = new Random();
                    int index = random.Next(0, councils.Count);
                    Teacher teacherRand = teachers[index];

                    project.UserNameCommentator = teacherRand.UserName;
                    _projectRepository.Update(project);
                }
            }

            message = "Hoàn thiện thuật toán chia đều giảng viên phản biện !";
            return true;
        }
    }
}
