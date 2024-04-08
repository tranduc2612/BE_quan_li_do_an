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
    }
}
