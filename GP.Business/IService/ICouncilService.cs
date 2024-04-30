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
    public interface ICouncilService
    {
        public List<CouncilDTO> GetList(CouncilModel req);
        public bool AddCouncil(CouncilModel req, out string message);
        public bool DeleteCouncil(string id, out string message);
        public Council GetCoucnil(string id);
        public List<TeachingDTO> getListTeachingNotInCouncil(TeachingListModel data);
        public List<ProjectDTO> getListProjectInCouncil(StudentCouncilListModel data);
        public Teaching getTeaching(string username,string semesterId);
        public bool UpdateCouncil(CouncilModel req, out string message);
        public bool AutoAssignTeachingToCouncil(string semesterId, string currentUsername, out string message);
        public bool AutoAssignTeacherCommentator(string semesterId, string currentUsername, out string message);
        public bool AutoAssignProjectToCouncil(string semesterId,string currentUsername, out string message);
        public bool AssignTeachingToCouncil(AssignTeachingCouncilModel model, out string message);
        public bool AssignProjectToCouncil(AssignProjectCouncilModel model, out string message);


    }
}
