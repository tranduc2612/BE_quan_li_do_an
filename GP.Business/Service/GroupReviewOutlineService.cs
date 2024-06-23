using DocumentFormat.OpenXml.Drawing;
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
            group.GroupReviewOutlineId = Guid.NewGuid().ToString();
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

        public GroupReviewOutline getGroupProjectOutline(string id)
        {
            return _groupReviewOutlineRepository.GetById(id);
        }

        public bool AutomationSplitGroup(string semesterId, out string message)
        {
            if(semesterId == null)
            {
                message = "Học kỳ không hợp lệ !";
                return false;
            }
           List<ProjectOutline> projectOutlines = _projectOutlineRepository.GetListProjectOutlineBySemester(semesterId)
                .Where(x => x.GroupReviewOutlineId == null 
                && x.UserNameNavigation.UserNameMentor != null 
                && x.UserNameNavigation.StatusProject != "INTERN"
                && x.UserNameNavigation.StatusProject != "REJECT"
            ).ToList();
           List<GroupReviewOutline> groups = _groupReviewOutlineRepository.GetListGroupBySemesterId(semesterId);
            List<Teaching> teachings = _teachingRepository.GetListTeachingBySemesterId(semesterId).Where(x=>x.GroupReviewOutlineId == null && x.UserNameTeacherNavigation?.Status == "AUTH" 
                && x.UserNameTeacherNavigation?.IsDelete == 0).ToList();
            if(groups.Count == 0)
            {
                message = "Nhóm xét duyệt phải lớn hơn 0 !";
                return false;
            }
            if (projectOutlines.Count == 0)
            {
                message = "Tổng số sinh viên tham gia làm đồ án phải lớn hơn 0 !";
                return false;
            }

            int maxTeachingsPerGroup = teachings.Count / groups.Count;

            // Nếu số lượng đề cương không chia hết cho số lượng nhóm, làm tròn lên
            //if (teachings.Count % groups.Count != 0)
            //{
            //    maxTeachingsPerGroup++;
            //}


            // Duyệt qua từng giảng dạy để chia vào nhóm
            int currentGroupIndex = 0;
            foreach (Teaching teaching in teachings)
            {

                // nếu đề cương đã có nhóm xét duyệt thì sẽ bỏ qua
                if (teaching.GroupReviewOutlineId != null)
                {
                    continue;
                }

                // Kiểm tra xem nhóm hiện tại đã đạt đến giới hạn số lượng đề cương chưa
                if (groups[currentGroupIndex].Teachings.Count < maxTeachingsPerGroup)
                {
                    //teaching.GroupReviewOutlineId = groups[currentGroupIndex].GroupReviewOutlineId;
                    groups[currentGroupIndex].Teachings.Add(teaching);
                    _teachingRepository.Update(teaching);
                }
                else
                {
                    // Chuyển sang nhóm tiếp theo nếu nhóm hiện tại đã đầy
                    currentGroupIndex++;
                    // Kiểm tra nếu nhóm hiện tại đã vượt quá số lượng nhóm, quay trở lại nhóm đầu tiên
                    if (currentGroupIndex >= groups.Count)
                    {
                        currentGroupIndex = 0;
                    }

                    //teaching.GroupReviewOutlineId = groups[currentGroupIndex].GroupReviewOutlineId;
                    groups[currentGroupIndex].Teachings.Add(teaching);
                    _teachingRepository.Update(teaching);
                }
            }

            List<GroupReviewOutline> groupsAvailable = _groupReviewOutlineRepository.GetListGroupBySemesterId(semesterId).Where(x=>x.Teachings.Count > 0).ToList();

            // Tính toán số lượng tối đa đề cương mỗi nhóm có thể chứa
            int maxProjectsPerGroup = projectOutlines.Count / groupsAvailable.Count;

            // Nếu số lượng đề cương không chia hết cho số lượng nhóm, làm tròn lên
            //if (projectOutlines.Count % groups.Count != 0)
            //{
            //    maxProjectsPerGroup++;
            //}


            currentGroupIndex = 0;
            foreach (GroupReviewOutline group in groupsAvailable)
            {
                HashSet<string> allCouncilTeachers = new HashSet<string>();
                foreach (var teaching in group.Teachings)
                {
                    allCouncilTeachers.Add(teaching.UserNameTeacher);
                }

                List<ProjectOutline> filteredProjects = projectOutlines
                    .Where(projectOutline => projectOutline.UserNameNavigation.UserNameMentor != null
                    && !allCouncilTeachers.Contains(projectOutline.UserNameNavigation.UserNameMentor)
                    && projectOutline.GroupReviewOutlineId == null)
                    .ToList();
                int limitZoom = Math.Min(maxProjectsPerGroup, filteredProjects.Count);
                for (int i = 0; i < limitZoom; i++)
                {
                    filteredProjects[i].GroupReviewOutlineId = group.GroupReviewOutlineId;
                    _projectOutlineRepository.Update(filteredProjects[i]);
                }
            }

            // gán toàn bộ sinh viên còn sót lại vào trong hội đồng còn chống
            List<ProjectOutline> projectLeft = _projectOutlineRepository.GetListProjectOutlineBySemester(semesterId)
                .Where(x => x.GroupReviewOutlineId == null
                && x.UserNameNavigation.UserNameMentor != null
                && x.UserNameNavigation.StatusProject != "DOING"
            ).ToList();


            foreach (ProjectOutline projectOutline in projectLeft)
            {
                // Tạo một danh sách các hội đồng không chứa giáo viên hướng dẫn của dự án
                var eligibleGroups = groups.Where(group =>
                    !group.Teachings.Any(teaching => teaching.UserNameTeacher == projectOutline.UserNameNavigation.UserNameMentor)
                ).ToList();

                if (eligibleGroups.Any())
                {
                    // Nếu có hội đồng thỏa mãn, chọn một trong số chúng để gán dự án vào
                    Random random = new Random();
                    int index = random.Next(0, eligibleGroups.Count);
                    GroupReviewOutline selectedCouncil = eligibleGroups[index];

                    // Gán dự án vào hội đồng đã chọn
                    projectOutline.GroupReviewOutlineId = selectedCouncil.GroupReviewOutlineId;
                }
                else
                {
                    // Nếu không có hội đồng nào thỏa mãn, chọn một hội đồng ngẫu nhiên từ danh sách các hội đồng
                    Random random = new Random();
                    int index = random.Next(0, groups.Count);
                    GroupReviewOutline selectedGriup = groups[index];

                    // Gán dự án vào hội đồng đã chọn
                    projectOutline.GroupReviewOutlineId = selectedGriup.GroupReviewOutlineId;
                }

                _projectOutlineRepository.Update(projectOutline);
            }

            message = "Okeee";
            return true;
        }


        public List<TeachingDTO> getListTeachingByGroupId(string groupId)
        {
            return _mapper.MapTeachingsToTeachingDTOs(_teachingRepository.GetListTeachingByGroupReview(groupId));
        }
    }
}
