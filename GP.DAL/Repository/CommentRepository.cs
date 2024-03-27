using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ManagementGraduationProjectContext _dbContext;
        public CommentRepository(ManagementGraduationProjectContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Comment comment)
        {
            _dbContext.Add(comment);
            _dbContext.SaveChanges();
        }

        public bool CheckPermission(string usernameOutline, string usernameTeacher, string semesterId)
        {
            Teaching find_teaching = _dbContext.Teachings.FirstOrDefault(x => x.UserNameTeacher == usernameTeacher && x.SemesterId == semesterId);
            if(find_teaching == null)
            {
                return false;
            }
            ProjectOutline projectOutline = _dbContext.ProjectOutlines.FirstOrDefault(x => x.UserName == usernameOutline);
            if(projectOutline == null)
            {
                return false;
            }
            Project project = _dbContext.Projects.FirstOrDefault(x => x.UserName == usernameOutline);
            if (find_teaching.GroupReviewOutlineId == projectOutline.GroupReviewOutlineId &&
               project.SemesterId == find_teaching.SemesterId) {
                return true;
            }

            return false;
        }

        public void Delete(Comment comment)
        {

            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
        }

        public Comment GetById(string id)
        {
            return _dbContext.Comments.FirstOrDefault(x => x.CommentId == id);
        }

        public List<Comment> GetList(string username)
        {
            List<Comment> comments = _dbContext.Comments
                .Include(x=>x.UserNameNavigation)
                .Include(x=>x.CreatedByNavigation)
                .Where(x=>x.UserName == username).ToList();
            return comments;
        }

        public void Update(Comment comment)
        {
            _dbContext.Update(comment);
            _dbContext.SaveChanges();
        }
    }
}
