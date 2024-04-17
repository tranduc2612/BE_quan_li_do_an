using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using GP.Business.IService;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.Models.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class ScheduleWeekController : ControllerBase
        {
            IScheduleWeekService _scheduleWeekService;
            public ScheduleWeekController(IScheduleWeekService scheduleWeekService)
            {
                _scheduleWeekService = scheduleWeekService;
            }

            [HttpGet("get-list-schedule-week")]
            public Response GetScheduleWeek(string idSemester, string idUserCreated)
            {
                Response response = new Response();

                try
                {
                    response.Code = 200;
                    response.Success = true;
                    response.ReturnObj = _scheduleWeekService.GetListScheduleWeek(idSemester, idUserCreated);

                }
                catch (Exception ex)
                {
                    response.SetError("Có lỗi xảy ra");
                    response.ExceptionInfo = ex.ToString();
                }
                return response;
            }


            [HttpGet("get-schedule-week")]
            public Response GetScheduleWeek(string id)
            {
                Response response = new Response();

                try
                {

                    response.Code = 200;
                    response.Success = true;
                    response.ReturnObj = _scheduleWeekService.GetById(id);

                }
                catch (Exception ex)
                {
                    response.SetError("Có lỗi xảy ra");
                    response.ExceptionInfo = ex.ToString();
                }
                return response;
            }

            [HttpPost("add-schedule-week")]
            public Response AddScheduleWeek(ScheduleWeekModel data)
            {
                Response response = new Response();

                try
                {

                    response.Code = 201;
                    response.ReturnObj = _scheduleWeekService.AddScheduleWeek(data, out string message,out bool check);
                    if (!check)
                    {
                        response.Success = false;
                        response.Code = 400;
                        response.Msg = message;
                    }
                    else
                    {
                        response.Msg = "Thêm mới thành công !";
                    }

                }
                catch (Exception ex)
                {
                    response.SetError("Có lỗi xảy ra");
                    response.ExceptionInfo = ex.ToString();
                }
                return response;
            }

            [HttpPut("update-schedule-week")]
            public Response UpdateScheduleWeek(ScheduleWeekModel data)
        {
            Response response = new Response();

            try
            {

                response.Code = 201;
                bool checkReq = _scheduleWeekService.UpdateScheduleWeek(data, out string message);
                if (!checkReq)
                {
                    response.Success = false;
                    response.Code = 400;
                    response.Msg = message;
                }
                else
                {
                    response.ReturnObj = message;
                    response.Msg = "Cập nhật thành công !";
                }

            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

            [HttpDelete("delete-schedule-week")]
            public Response DeleteScheduleWeek(string id)
            {
                Response response = new Response();

                try
                {

                    response.Code = 201;
                    bool checkReq = _scheduleWeekService.DeleteScheduleWeek(id, out string message);
                    if (!checkReq)
                    {
                        response.Success = false;
                        response.Code = 400;
                        response.Msg = message;
                    }
                    else
                    {
                        response.ReturnObj = message;
                        response.Msg = "Cập nhật thành công !";
                    }

                }
                catch (Exception ex)
                {
                    response.SetError("Có lỗi xảy ra");
                    response.ExceptionInfo = ex.ToString();
                }
                return response;
            }


            [HttpPost("handle-detail-schedule-week")]
            public Response HandleDetailScheduleWeek([FromForm] ScheduleWeekDetailModel data)
            {
                Response response = new Response();

                try
                {
                    response.Code = 201;
                    response.ReturnObj = _scheduleWeekService.HandleScheduleWeekDetail(data, out string message, out bool check);
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

            [HttpGet("get-detail-schedule-week")]
            public Response GetDetailScheduleWeek(string userName,string idSchedule)
            {
                Response response = new Response();

                try
                {
                    response.Code = 200;
                    response.ReturnObj = _scheduleWeekService.GetScheduleWeekDetail(userName, idSchedule);
                    response.Msg = "Lấy thành công !";
                }
                catch (Exception ex)
                {
                    response.SetError("Có lỗi xảy ra");
                    response.ExceptionInfo = ex.ToString();
                }
                return response;
            }

            [HttpPut("update-comment-schedule-week")]
            public Response UpdateCommentDetailScheduleWeek(string ScheduleWeekId,string UserNameProject,string Comment)
            {
                Response response = new Response();

                try
                {
                    ScheduleWeekDetailModel data = new ScheduleWeekDetailModel();
                    data.ScheduleWeekId = ScheduleWeekId;
                    data.UserNameProject = UserNameProject;
                    data.Comment = Comment;

                response.Code = 200;
                    response.ReturnObj = _scheduleWeekService.UpdateComment(data, out string msg);
                    if(response.ReturnObj == null)
                    {
                        response.Success= false;
                    }
                    response.Msg = msg;
                }
                catch (Exception ex)
                {
                    response.SetError("Có lỗi xảy ra");
                    response.ExceptionInfo = ex.ToString();
                }
                return response;
            }

            [HttpGet("dowload-file-schedule-week")]
            public IActionResult DownloadFileScheduleWeek(string ScheduleWeekId, string UserNameProject)
            {

                try
                {
                    DetailScheduleWeek find = _scheduleWeekService.GetScheduleWeekDetail(UserNameProject, ScheduleWeekId );
                if(find != null)
                {
                    string path = Path.Combine("file/file_week", find.NameFile);
                    byte[] fileData = System.IO.File.ReadAllBytes(path);
                    // Lấy tên file từ đường dẫn
                    string fileName = Path.GetFileName(path);

                    // Trả về file dưới dạng phản hồi HTTP để tải xuống
                    return File(fileData, "application/octet-stream",  fileName);
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

