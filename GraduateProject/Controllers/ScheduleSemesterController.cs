using DocumentFormat.OpenXml.Drawing.Charts;
using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleSemesterController : ControllerBase
    {
        IScheduleSemesterService _scheduleSemesterService;
        public ScheduleSemesterController(IScheduleSemesterService scheduleSemesterService)
        {
            _scheduleSemesterService= scheduleSemesterService;
        }

        [HttpGet("get-schedule-semester")]
        public Response GetScheduleSemester(string id)
        {
            Response response = new Response();

            try
            {

                response.Code = 200;
                response.Success = true;
                response.ReturnObj = _scheduleSemesterService.GetById(id);

            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("add-schedule-semester")]
        public Response AddScheduleSemester(ScheduleSemesterModel data)
        {
            Response response = new Response();

            try
            {

                response.Code = 201;
                bool checkReq = _scheduleSemesterService.AddScheduleSemester(data, out string message);
                if (!checkReq)
                {
                    response.Success = false;
                    response.Code = 400;
                    response.Msg = message;
                }
                else
                {
                    response.ReturnObj = message;
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

        [HttpPut("update-schedule-semester")]
        public Response UpdateScheduleSemester(ScheduleSemesterModel data)
        {
            Response response = new Response();

            try
            {

                response.Code = 200;
                bool checkReq = _scheduleSemesterService.UpdateScheduleSemester(data, out string message);
                if (!checkReq)
                {
                    response.Success = false;
                    response.Code = 400;
                }
                else
                {
                    response.Success = checkReq;
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
        [HttpDelete("delete-schedule-semester")]
        public Response DeleteScheduleSemester(string id)
        {
            Response response = new Response();

            try
            {

                response.Code = 200;
                bool checkReq = _scheduleSemesterService.DeleteScheduleSemester(id, out string message);
                if (!checkReq)
                {
                    response.Success = false;
                    response.Code = 400;
                }
                else
                {
                    response.Success = checkReq;
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

        [HttpGet("get-list-schedule-semester")]
        public Response GetListScheduleSemester(string semesterId)
        {
            Response response = new Response();

            try
            {

                response.Code = 200;
                response.ReturnObj = _scheduleSemesterService.GetListScheduleSemester(semesterId);
                response.Msg = "Lấy danh sách thành công !";

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
