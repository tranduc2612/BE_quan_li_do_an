using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.DAL.IRepository;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IProjectOutlineRepository _outlineRepository;


        private readonly MappingProfile _mapper;


        public CommentService(ICommentRepository commentRepository, IProjectOutlineRepository outlineRepository, MappingProfile mapper)
        {
            _commentRepository= commentRepository;
            _outlineRepository = outlineRepository;
            _mapper = mapper;
        }

        public Comment AddComment(CommentDTO comment)
        {
            ProjectOutline find = _outlineRepository.GetById(comment.UserName);
            if(find == null) {
                return null;
            }
            if(find.UserNameNavigation == null)
            {
                return null;
            }
            if (CheckPermissionComment(find.UserName,comment.CreatedBy,find.UserNameNavigation.SemesterId))
            {
                Comment add = _mapper.MapCommentDTOToComment(comment);
                _commentRepository.Add(add);
                return add;
            }

            return null;
        }

        public bool CheckPermissionComment(string usernameOutline, string usernameTeacher, string semesterId)
        {
            return _commentRepository.CheckPermission(usernameOutline, usernameTeacher, semesterId);
        }

        public bool DeleteComment(string id, out string message)
        {
            Comment comment = _commentRepository.GetById(id);
            if (comment == null)
            {
                message = "Đánh giá không tồn tại !";
                return false;
            }
            _commentRepository.Delete(comment);
            message = "Xóa thành công !";
            return true;
        }

        public List<Comment> GetList(string username)
        {
            throw new NotImplementedException();
        }

        public Comment UpdateComment(CommentDTO comment)
        {
            ProjectOutline find_outline = _outlineRepository.GetById(comment.UserName);
            if (find_outline == null)
            {
                return null;
            }
            Comment find_comment = _commentRepository.GetById(comment.CommentId);
            if(find_comment == null) {
                return null;
            }
            if (CheckPermissionComment(find_outline.UserName, comment.CreatedBy, find_outline.UserNameNavigation.SemesterId))
            {
                Comment update = _mapper.MapCommentDTOToComment(comment);
                find_comment.ContentComment = comment.ContentComment;
                find_comment.CreatedDate = DateTime.UtcNow;
                _commentRepository.Update(find_comment);
                return find_comment;
            }
            return null;
        }
    }
}
