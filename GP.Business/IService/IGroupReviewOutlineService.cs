using GP.Common.DTO;
using GP.Common.Models;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IGroupReviewOutlineService
    {
        public GroupReviewOutline getGroupProjectOutline(string id);
        public PaginatedResultBase<GroupReviewOutline> getListGroupReview(GroupReviewOutlineListModel data);
        public List<GroupReviewOutlineDTO> getListGroupReviewSemester(GroupReviewOutlineListSemesterModel data);
        public List<TeachingDTO> getListTeaching(TeachingListModel data);
        public List<TeachingDTO> getListTeachingByGroupId(string groupId);
        public List<ProjectOutlineDTO> getListProjectOutline(ProjectOutlineListModel data);
        public bool AddGroupReview(GroupReviewOutlineModel data, out string message);
        public bool UpdateGroupReview(GroupReviewOutlineModel data, out string message);
        public bool DeleteGroupReview(string id, out string message);
        public bool AssignTeachingToGroup(AssignTeachingGroupReviewOutlineModel model, out string message);
        public bool AssignProjectToGroup(AssignProjectOutlineGroupReviewOutlineModel model, out string message);
        public bool AutomationSplitGroup(string semesterId, out string message);
    }
}
