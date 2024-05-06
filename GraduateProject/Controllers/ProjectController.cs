using GP.Business.IService;
using GP.Business.Service;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.Models.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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

        [HttpGet("get-list-project-by-mentor")]
        public Response GetListProjectByMentor(string username_teacher, string semesterId)
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
                response.ReturnObj = _projectService.GetListProjectByUsernameMentor(username_teacher, semesterId);
                response.Msg = "Lấy danh sach thành công !";
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpGet("get-project-by-username")]
        public Response GetProjectByUsername(string username)
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
                response.ReturnObj = _projectService.GetProjectByUserName(username);
                response.Msg = "thành công !";
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
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
                response.Success = _projectService.AssignMentorTeacherToProject(username_student, username_teacher, out string message);
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

        [HttpPost("auto-assign-mentor")]
        public Response AutoAssignMentor([FromQuery]string semesterId)
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
                //response.Code = 200;
                //response.Success = _projectService.AutomationAssignMentorTeacherToProject(semesterId, out string message);
                //response.Msg = message;
                //if (response.Success == false)
                //{
                //    response.Code = 400;
                //}

                response.Code = 200;
                response.ReturnObj = _projectService.AutomationAssignMentorTeacherToProject(semesterId, out string message);
                response.Msg = message;
                if (response.Success == false)
                {
                    response.Code = 400;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number  == 2812)
                {
                    response.SetError("Thủ tục không tồn tại trong cơ sở dữ liệu.");
                    response.ExceptionInfo = ex.ToString();
                }
                else
                {
                    response.SetError("Lỗi khi thực thi thủ tục: " + ex.Message);
                    response.ExceptionInfo = ex.ToString();
                }
                
            }
            return response;
        }

        [HttpPost("assign-commentator")]
        public Response AssignCommentator(string username_student, string username_teacher)
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
                response.Success = _projectService.AssignUserNameCommentatorToProject(username_student, username_teacher, out string message);
                response.Msg = message;
                if (response.Success == false)
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



        [HttpPost("get-list-project-by-group-review")]
        public Response GetListProjectByGroupReview(ProjectOutlineListModel req)
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
                response.ReturnObj = _projectService.GetListProjectByGroupId(req);
                response.Msg = "Lấy danh sach thành công !";
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

        [HttpPut("update-project-outline")]
        public Response UpdateProjectOutline([FromBody] ProjectOutlineDTO req)
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
                bool check = _projectService.UpdateNewProjectOutline(req, out string message);
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

        [HttpPut("update-project-score")]
        public Response UpdateScoreProject([FromBody] ProjectReqScore req)
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
                bool check = _projectService.UpdateScoreToProject(req.UserName,req.Role,req.Score,req.Comment, out string message);
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

        [HttpPost("get-info-review-by-hash-key")]
        public Response GetInfoReview([FromBody] ProjectReviewKey req)
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
                response.ReturnObj = null;
                if(req.Role == "MENTOR")
                {
                    response.ReturnObj = _projectService.GetProjectByHashKeyMentor(req.Key);
                }
                if(req.Role == "COMMENTATOR")
                {
                    response.ReturnObj = _projectService.GetProjectByHashKeyCommentator(req.Key);
                }
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("handle-upload-file-final-project")]
        public Response UploadFileFinalProject([FromForm] ProjectFinalFile req)
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
                response.ReturnObj = _projectService.HandleUploadFinalFile(req, out string message, out bool check);
                response.Msg = message;
                if (!check)
                {
                    response.Success = false;
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

        [HttpGet("dowload-file-project-final")]
        public IActionResult DownloadFileFinal(string Username)
        {

            try
            {
                ProjectDTO find = _projectService.GetProjectByUserName(Username);
                if (find != null)
                {
                    string path = Path.Combine("file", "final", find.UserName, find.NameFileFinal);
                    byte[] fileData = System.IO.File.ReadAllBytes(path);
                    // Lấy tên file từ đường dẫn
                    string fileName = Path.GetFileName(path);

                    // Trả về file dưới dạng phản hồi HTTP để tải xuống
                    return File(fileData, "application/octet-stream", fileName);
                }
                else
                {
                    return NotFound();
                }



            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


    }
}
