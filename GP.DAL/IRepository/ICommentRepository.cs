using GP.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface ICommentRepository
    {
        public List<Comment> GetList(string username);
        public Comment GetById(string id);
        public void Add(Comment comment);
        public void Delete(Comment comment);
        public void Update(Comment comment);
        public bool CheckPermission(string usernameOutline, string usernameTeacher, string semesterId);


    }
}
