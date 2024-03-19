using GP.Business.IService;
using GP.Business.Service;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("get-list-comment")]
        public Response GetComment(string username)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }

            try
            {
                response.Code = 200;
                response.Success = true;
                List<Comment> cmt = _commentService.GetList(username);
                response.ReturnObj = cmt;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("add-comment")]
        public Response AddComment(CommentDTO comment)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }

            try
            {
                response.Code = 201;
                response.Success = true;
                Comment cmt = _commentService.AddComment(comment);

                if (cmt == null)
                {
                    response.SetError(400, "Thêm thất bại !");
                }
                else
                {
                    response.ReturnObj = cmt;
                }
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPut("update-comment")]
        public Response UpdateComment(CommentDTO comment)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }

            try
            {
                response.Code = 201;
                response.Success = true;
                Comment cmt = _commentService.UpdateComment(comment);

                if (cmt == null)
                {
                    response.SetError(400, "Cập nhật thất bại !");
                }
                else
                {
                    response.ReturnObj = cmt;
                }
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpDelete("delete-comment")]
        public Response DeleteComment(string id_comment)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Validate Error");
                return response;
            }

            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                bool check = _commentService.DeleteComment(id_comment, out string message);

                if (!check)
                {
                    response.SetError(400, message);
                }
                response.Msg = message;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }
    }
}
