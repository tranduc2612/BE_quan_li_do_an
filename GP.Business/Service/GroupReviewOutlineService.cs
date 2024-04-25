using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.DAL.Repository;
using GP.Models.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class GroupReviewOutlineService : IGroupReviewOutlineService
    {
        IGroupReviewOutlineRepository _groupReviewOutlineRepository;
        ITeacherRepository _teacherRepository;
        ITeachingRepository _teachingRepository;
        ISemesterRepository _semesterRepository;
        IProjectOutlineRepository _projectOutlineRepository;
        private readonly MappingProfile _mapper;
        public GroupReviewOutlineService(IGroupReviewOutlineRepository groupReviewOutlineRepository, ITeacherRepository teacherRepository, ITeachingRepository teachingRepository, ISemesterRepository semesterRepository, IProjectOutlineRepository projectOutlineRepository, MappingProfile mapper)
        {
            _groupReviewOutlineRepository = groupReviewOutlineRepository;
            _teacherRepository = teacherRepository;
            _teachingRepository = teachingRepository;
            _semesterRepository = semesterRepository;
            _projectOutlineRepository = projectOutlineRepository;
            _mapper = mapper;
        }
        public bool AddGroupReview(GroupReviewOutlineModel data, out string message)
        {
            GroupReviewOutline group = new GroupReviewOutline();
            group.NameGroupReviewOutline = data.NameGroupReviewOutline;
            group.CreatedBy= data.CreatedBy;
            group.SemesterId = data.SemesterId;
            _groupReviewOutlineRepository.Add(group);

            message = "Thêm thành công !";
            return true;

        }

        public bool DeleteGroupReview(string id, out string message)
        {
            GroupReviewOutline group = _groupReviewOutlineRepository.GetById(id);
            if(group== null)
            {
                message = "Không tồn tại nhóm này !";
                return false;
            }
            _groupReviewOutlineRepository.Delete(group);

            message = "Xóa thành công !";
            return true;
        }

        public bool UpdateGroupReview(GroupReviewOutlineModel data, out string message)
        {
            GroupReviewOutline group = _groupReviewOutlineRepository.GetById(data.GroupReviewOutlineId);
            if (group == null)
            {
                message = "Không tồn tại nhóm này !";
                return false;
            }

            group.GroupReviewOutlineId = data.GroupReviewOutlineId;
            group.NameGroupReviewOutline = data.NameGroupReviewOutline;
            group.CreatedBy = data.CreatedBy;
            message = "Cập nhật thành công !";

            _groupReviewOutlineRepository.Update(group);
            return true;

        }

        public bool AssignTeachingToGroup(AssignTeachingGroupReviewOutlineModel model, out string message)
        {
            Semester semester_find = _semesterRepository.GetById(model.SemesterTeachingId);
            if (semester_find == null)
            {
                message = "Học kỳ này không tồn tại !";
                return false;
            }
            GroupReviewOutline group_find = _groupReviewOutlineRepository.GetById(model.GroupReviewOutlineId);
            if (group_find == null)
            {
                message = "Nhóm xét duyệt này không tồn tại !";
                return false;
            }
            TeachingListModel lstReq = new TeachingListModel();
            lstReq.SemesterId = model.SemesterTeachingId;
            lstReq.GroupReviewOutlineId = model.GroupReviewOutlineId;
            List<Teaching> lstTeaching = _teachingRepository.GetListWithTeachingCondition(lstReq);
            

            foreach (string username in model.UsernameTeaching)
            {
                Teacher teacher_find = _teacherRepository.Get(username);
                if (teacher_find == null)
                {
                    message = "Giảng viên này không tồn tại !";
                    return false;
                }

                Teaching teaching_find = _teachingRepository.GetByUserNameSemester(username, model.SemesterTeachingId);
                if (teaching_find == null)
                {
                    message = "Giảng viên này chưa được thêm vào học kỳ này !";
                    return false;
                }
                teaching_find.GroupReviewOutlineId = model.GroupReviewOutlineId;
                _groupReviewOutlineRepository.AssignGroupToTeaching(teaching_find);
            }

            foreach (Teaching teaching_find in lstTeaching)
            {
                if (!model.UsernameTeaching.Any(x => teaching_find.UserNameTeacher == x))
                {
                    teaching_find.GroupReviewOutlineId = null;
                    _groupReviewOutlineRepository.AssignGroupToTeaching(teaching_find);
                }
            }
            message = "Gán giảng viên thành công !";
            return true;
        }

        public bool AssignProjectToGroup(AssignProjectOutlineGroupReviewOutlineModel model, out string message)
        {
            GroupReviewOutline group_find = _groupReviewOutlineRepository.GetById(model.GroupReviewOutlineId);
            if (group_find == null)
            {
                message = "Nhóm xét duyệt này không tồn tại !";
                return false;
            }
            ProjectOutlineListModel reqList = new ProjectOutlineListModel();
            reqList.GroupReviewOutlineId = model.GroupReviewOutlineId;
            reqList.SemesterId = model.SemesterTeachingId;
            List<ProjectOutlineDTO> lstProjectOutline = _projectOutlineRepository.GetListProjectOutlineInGroup(reqList);

            foreach (string username in model.UsernameProjectOutline)
            {
                ProjectOutline projectOutline = _projectOutlineRepository.GetById(username);
                if (projectOutline == null)
                {
                    message = "Sinh viên chưa đăng ký đề cương đồ án !";
                    return false;
                }
                projectOutline.GroupReviewOutlineId = model.GroupReviewOutlineId;
                _groupReviewOutlineRepository.AssignGroupToOutline(projectOutline);
            }
            foreach (ProjectOutlineDTO outline_find in lstProjectOutline)
            {
                if (!model.UsernameProjectOutline.Any(x => outline_find.UserName == x))
                {
                    ProjectOutline find = _projectOutlineRepository.GetById(outline_find.UserName);
                    find.GroupReviewOutlineId = null;
                    _groupReviewOutlineRepository.AssignGroupToOutline(find);
                }
            }

            message = "Gán đề cương vào nhóm xét duyệt thành công !";
            return true;

        }

        public PaginatedResultBase<GroupReviewOutline> getListGroupReview(GroupReviewOutlineListModel data)
        {
            return _groupReviewOutlineRepository.GetListPage(data);
        }

        public List<GroupReviewOutlineDTO> getListGroupReviewSemester(GroupReviewOutlineListSemesterModel data)
        {
            return _groupReviewOutlineRepository.GetListPageSemester(data);
        }

        public List<TeachingDTO> getListTeaching(TeachingListModel data)
        {
            return _mapper.MapTeachingsToTeachingDTOs(_teachingRepository.GetListTeaching(data));
        }

        public List<ProjectOutlineDTO> getListProjectOutline(ProjectOutlineListModel data)
        {
            return _projectOutlineRepository.GetListProjectOutline(data);
        }

        public GroupReviewOutline getProjectOutline(string id)
        {
            return _groupReviewOutlineRepository.GetById(id);
        }

        public bool AutoAssignTeacherAndStudentInGroup(string semesterId, out string message)
        {
            throw new NotImplementedException();
        }
    }
}
