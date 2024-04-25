using GP.Business.IService;
using GP.Business.Service;
using GP.Common.DTO;
using GP.Common.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : Controller
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }
        [HttpPost("get-list-education")]
        public Response GetListMajor([FromBody] EducationDTO req)
        {
            Response response = new Response();

            try
            {
                response.Msg = "Sucess";
                response.Code = 200;
                response.ReturnObj = _educationService.GetList(req);
            }
            catch (Exception ex)
            {
                response.SetError("Có lỗi xảy ra");
                response.ExceptionInfo = ex.ToString();
            }
            return response;
        }

        [HttpPost("add-education")]
        public Response AddEducation([FromBody] EducationDTO req)
        {
            Response response = new Response();

            try
            {
                response.Code = 201;
                response.Success = _educationService.AddEducation(req, out string message);
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

        [HttpPut("update-education")]
        public Response UpdateEducation([FromBody] EducationDTO req)
        {
            Response response = new Response();

            try
            {
                response.Code = 200;
                response.Success = _educationService.UpdateEducation(req, out string message);
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
