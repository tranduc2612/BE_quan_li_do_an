using GP.Business.IService;
using GP.Business.Service;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.Models.Model;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost("assign-mentor")]
        public Response AssignMentor(string username_student,string username_teacher)
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
                response.Success = _projectService.AssignMentorTeacher(username_student, username_teacher, out string message);
                response.Msg = message;
                if(response.Success == false)
                {
                    response.Code = 400;
                }
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpGet("get-project-outline-id")]
        public Response ProjecOutlinetById(string id)
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
                ProjectOutline projectOutline = _projectService.GetProjectOutlineByUsername(id);
                if (projectOutline == null)
                {
                    response.Code = 200;
                    response.Success = false;
                    response.Msg = "Không tìm thấy đề cương đồ án này !";
                }
                else
                {
                    response.Code = 200;
                    response.Success = true;
                    response.ReturnObj = projectOutline;
                }
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("add-project-outline")]
        public Response AddProjectOutline([FromBody] ProjectOutlineDTO req)
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
                response.Code = 201;
                bool check = _projectService.AddNewProjectOutline(req, out string message);
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
