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

        [HttpPost("get-list-group-review-outline")]
        public Response GetListGroupReviewPage(GroupReviewOutlineListModel model)
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
                response.ReturnObj = _groupReviewOutlineService.getListGroupReview(model);
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


        // 
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
                bool check = _groupReviewOutlineService.AssignProjectToGroup(model.UsernameProjectOutline, model.GroupReviewOutlineId, out string message);
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
