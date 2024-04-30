using GP.Business.IService;
using GP.Common.Helpers;
using GP.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupReviewOutlineController : ControllerBase
    {
        private readonly IGroupReviewOutlineService _groupReviewOutlineService;

        public GroupReviewOutlineController(IGroupReviewOutlineService groupReviewOutlineService)
        {
            _groupReviewOutlineService= groupReviewOutlineService;
        }

        [HttpGet("get-group-review-outline-by-id")]
        public Response GetGroupReviewId(string id)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _groupReviewOutlineService.getProjectOutline(id);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("get-list-group-review-outline-semester")]
        public Response GetListGroupReviewSemester(GroupReviewOutlineListSemesterModel model)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                response.Msg = "Sucess";
                response.Code = 201;
                response.ReturnObj = _groupReviewOutlineService.getListGroupReviewSemester(model);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }
        [HttpPost("get-list-teaching")]
        public Response GetListTeaching(TeachingListModel model)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _groupReviewOutlineService.getListTeaching(model);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        //sẽ chỉ lấy danh sách đề cương có trong group đó
        [HttpPost("get-list-project-outline-by-groupid")]
        public Response GetListProjectOutline(ProjectOutlineListModel model)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _groupReviewOutlineService.getListProjectOutline(model);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("add-group-review-outline")]
        public Response AddGroupReviewOutline([FromBody] GroupReviewOutlineModel model)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                response.Msg = "Sucess";
                response.Code = 201;
                bool check = _groupReviewOutlineService.AddGroupReview(model, out string message);
                if (!check)
                {
                    response.SetError(400,message);
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
        [HttpPost("assign-group-review-outline-to-teaching")]
        public Response AssignGroupReviewOutlineToTeaching([FromBody]AssignTeachingGroupReviewOutlineModel model)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                response.Msg = "Sucess";
                response.Code = 201;
                bool check = _groupReviewOutlineService.AssignTeachingToGroup(model, out string message);
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
        [HttpPost("assign-group-review-outline-to-project-outline")]
        public Response AssignGroupReviewOutlineToProjectOutline([FromBody] AssignProjectOutlineGroupReviewOutlineModel model)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                response.Msg = "Sucess";
                response.Code = 201;
                bool check = _groupReviewOutlineService.AssignProjectToGroup(model, out string message);
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

        [HttpPost("automation-split-group")]
        public Response AutomationSplitGroup([FromQuery] string semesterId)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                response.Msg = "Sucess";
                response.Code = 201;
                bool check = _groupReviewOutlineService.AutomationSplitGroup(semesterId, out string message);
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

        [HttpPut("update-group-review-outline")]
        public Response UpdateGroupReviewOutline(GroupReviewOutlineModel model)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                response.Msg = "Sucess";
                response.Code = 201;
                bool check = _groupReviewOutlineService.UpdateGroupReview(model, out string message);
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
        [HttpDelete("delete-group-review-outline")]
        public Response UpdateGroupReviewOutline(string id)
        {
            Response response = new Response();
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }
            try
            {
                response.Msg = "Sucess";
                response.Code = 201;
                bool check = _groupReviewOutlineService.DeleteGroupReview(id, out string message);
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
