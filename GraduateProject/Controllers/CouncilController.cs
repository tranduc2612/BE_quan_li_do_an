using GP.Business.IService;
using GP.Business.Service;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouncilController : ControllerBase
    {
        private readonly ICouncilService _councilService;

        public CouncilController(ICouncilService councilService)
        {
            _councilService = councilService;
        }

        [HttpGet("get-council")]
        public Response GetCouncil(string id)
        {
            Response response = new Response();

            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _councilService.GetCoucnil(id);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpGet("get-teaching")]
        public Response GetTeaching(string username, string semesterId)
        {
            Response response = new Response();

            // Validate 
            if (!ModelState.IsValid)
            {
                response.SetError(StatusCodes.Status422UnprocessableEntity, "Lỗi tham số đầu vào");
                return response;
            }

            try
            {
                response.ReturnObj = _councilService.getTeaching(username, semesterId); ;
                response.Msg = "Success";
                response.Code = 200;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }


        [HttpPost("get-list-council-semester")]
        public Response GetListCouncilSemester([FromBody] CouncilModel req)
        {
            Response response = new Response();

            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _councilService.GetList(req);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("get-list-project-council")]
        public Response GetListProject([FromBody] StudentCouncilListModel req)
        {
            Response response = new Response();

            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _councilService.getListProjectInCouncil(req);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("get-list-teaching-in-council")]
        public Response GetListTeaching([FromBody] TeachingListModel req)
        {
            Response response = new Response();

            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _councilService.getListTeachingNotInCouncil(req);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("add-council")]
        public Response AddCouncil([FromBody] CouncilModel req)
        {
            Response response = new Response();
            try
            {
                response.Code = 201;
                response.Success = _councilService.AddCouncil(req, out string message);
                response.Msg = message;
                response.ReturnObj = null;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }
        [HttpPut("update-council")]
        public Response UpdateCouncil([FromBody] CouncilModel req)
        {
            Response response = new Response();
            try
            {
                response.Code = 201;
                response.Success = _councilService.UpdateCouncil(req, out string message);
                response.Msg = message;
                response.ReturnObj = null;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpDelete("delete-council")]
        public Response DeleteCouncil(string id)
        {
            Response response = new Response();
            try
            {
                response.Code = 201;
                response.Success = _councilService.DeleteCouncil(id, out string message);
                response.Msg = message;
                response.ReturnObj = null;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("assign-council-to-teaching")]
        public Response AssignCouncilToTeaching([FromBody] AssignTeachingCouncilModel model)
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
                bool check = _councilService.AssignTeachingToCouncil(model, out string message);
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
        [HttpPost("assign-council-to-project")]
        public Response AssignProjectToProject([FromBody] AssignProjectCouncilModel model)
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
                bool check = _councilService.AssignProjectToCouncil(model, out string message);
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
