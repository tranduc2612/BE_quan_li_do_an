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
    public class MajorController : ControllerBase
    {
        private readonly IMajorService _majorService;

        public MajorController(IMajorService majorService)
        {
            _majorService = majorService;
        }

        // 
        [HttpPost("get-list-major")]
        public Response GetListMajor([FromBody] MajorDTO req)
        {
            Response response = new Response();

            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _majorService.GetList(req);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        // 
        [HttpPost("get-major")]
        public Response GetMajor([FromBody] MajorDTO req)
        {
            Response response = new Response();

            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _majorService.GetList(req);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("add-major")]
        public Response AddMajor([FromBody] MajorDTO req)
        {
            Response response = new Response();

            try
            {
                response.Code = 201;
                response.Success = _majorService.AddMajor(req, out string message);
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

        [HttpPut("update-major")]
        public Response UpdateMajor([FromBody] MajorDTO req)
        {
            Response response = new Response();

            try
            {
                response.Code = 200;
                response.Success = _majorService.UpdateMajor(req, out string message);
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

    }
}
