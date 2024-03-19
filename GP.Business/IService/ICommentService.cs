using GP.Common.DTO;
using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface ICommentService
    {
        public List<Comment> GetList(string username);
        public Comment AddComment(CommentDTO comment);
        public bool DeleteComment(string id, out string message);
        public Comment UpdateComment(CommentDTO comment);
        public bool CheckPermissionComment(string usernameOutline,string usernameTeacher, string semesterId);


    }
}
