using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.Models.Model;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpPost("get-list-project")]
        public Response GetListProject(ProjectListModel data)
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
                List<Project> projects = _projectService.GetListProject(data);
                response.SetData(200, projects);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpGet("get-project-by-id")]
        public Response ProjectById(string username)
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
                Project projectFind = _projectService.GetProjectByUsername(username);
                if (projectFind == null)
                {
                    response.Code = 200;
                    response.Success = false;
                    response.Msg = "Không tìm thấy đồ án này !";
                }
                else
                {
                    response.Code = 200;
                    response.Success = true;
                    response.ReturnObj = projectFind;
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
                response.Success = true;
                ProjectOutlineDTO project_outline = _projectService.AddNewProjectOutline(req);
                if (project_outline == null)
                {
                    response.Code = 400;
                    response.SetError("Bạn chưa khởi tạo đồ án !");
                }
                else
                {
                    response.SetData(201, project_outline);
                }
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
