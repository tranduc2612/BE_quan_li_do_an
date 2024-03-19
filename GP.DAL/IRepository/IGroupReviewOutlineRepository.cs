using GP.Common.DTO;
using GP.Common.Models;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface IGroupReviewOutlineRepository
    {
        public PaginatedResultBase<GroupReviewOutline> GetListPage(GroupReviewOutlineListModel data);
        public List<GroupReviewOutlineDTO> GetListPageSemester(GroupReviewOutlineListSemesterModel data);

        public GroupReviewOutline GetById(string id);
        public void Add(GroupReviewOutline data);
        public void Update(GroupReviewOutline data);
        public void Delete(GroupReviewOutline data);
        public void AssignGroupToTeaching(Teaching teaching);
        public void AssignGroupToOutline(ProjectOutline outline, string idGroup);

    }
}
